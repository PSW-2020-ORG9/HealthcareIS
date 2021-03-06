﻿using System;
using System.Collections.Generic;
using System.Linq;
using General;
using General.Repository;
using Schedule.API.DTOs;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Procedures;
using Schedule.API.Model.Recommendations;
using Schedule.API.Model.Shifts;
using Schedule.API.Model.Utilities;
using Schedule.API.Services.Procedures.Interface;

namespace Schedule.API.Services.Procedures
{
    public class RecommendationService : IRecommendationService
    {
        private readonly RepositoryWrapper<IExaminationRepository> _examinationWrapper;
        private readonly RepositoryWrapper<IShiftRepository> _shiftWrapper;
        private readonly IConnection _doctorConnection;
        private const int MaximumAppointmentsInShift = 16;

        public RecommendationService(
            IExaminationRepository examinationRepository,
            IShiftRepository shiftRepository,
            IConnection doctorConnection)
        {
            _shiftWrapper = new RepositoryWrapper<IShiftRepository>(shiftRepository);
            _examinationWrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            _doctorConnection = doctorConnection;
        }
        
        private const int RecommendationBatchSize = 5;
        private int GetRemainingSlots(IEnumerable<RecommendationDto> r) => RecommendationBatchSize - r.Count();
        
        public IEnumerable<RecommendationDto> Recommend(RecommendationRequestDto dto)
        {
            if (dto.TimeInterval == null) return null;
            if (dto.TimeInterval.Start < DateTime.Now) return null;
            
            return dto.Preference == RecommendationPreference.Time ? RecommendWithTime(dto) : RecommendWithDoctor(dto);
        }

        public IEnumerable<RecommendationDto> RecommendEmergency(RecommendationRequestDto dto)
        {
            if (dto.TimeInterval == null) return null;
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            recommendations.AddRange(RecommendEmergencyInTimeInterval(dto, GetRemainingSlots(recommendations)));
            return recommendations;
        }

        private List<RecommendationDto> RecommendWithTime(RecommendationRequestDto dto)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            recommendations.AddRange(RecommendForDoctorInTimeInterval(dto, GetRemainingSlots(recommendations)));
            recommendations.AddRange(RecommendAnyDoctorInTimeInterval(dto, GetRemainingSlots(recommendations)));
            
            return recommendations;
        }

        private List<RecommendationDto> RecommendWithDoctor(RecommendationRequestDto dto)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            recommendations.AddRange(RecommendForDoctorInTimeInterval(dto, GetRemainingSlots(recommendations)));
            recommendations.AddRange(RecommendForDoctorAnyTimeInterval(dto, GetRemainingSlots(recommendations)));
            recommendations.AddRange(RecommendAnyDoctorInTimeInterval(dto, GetRemainingSlots(recommendations)));

            return recommendations;
        }

        private List<RecommendationDto> RecommendAnyDoctorInTimeInterval(RecommendationRequestDto dto, int remainingSlots)
        {
            if (remainingSlots == 0) return new List<RecommendationDto>();

            IEnumerable<int> doctorIds = 
                _doctorConnection.Get<IEnumerable<int>>($"specialty/ids/{dto.SpecialtyId}");

            IEnumerable<Shift> allShifts = _shiftWrapper.Repository
                .GetMatching(shift =>
                    doctorIds.Contains(shift.DoctorId)
                    && shift.TimeInterval.Start.Date >= dto.TimeInterval.Start.Date
                    && shift.TimeInterval.Start.Date <= dto.TimeInterval.End.Date
                );

            return FindRecommendationsInShifts(allShifts, remainingSlots);
        }

        private List<RecommendationDto> RecommendForDoctorInTimeInterval(RecommendationRequestDto dto, int remainingSlots)
        {
            if (remainingSlots == 0) return new List<RecommendationDto>();
            
            var selectedDoctorShifts = 
                _shiftWrapper.Repository.GetByDoctorIdAndTimeInterval(dto.DoctorId, dto.TimeInterval).ToList();
            return FindRecommendationsInShifts(selectedDoctorShifts, remainingSlots);
        }

        private List<RecommendationDto> RecommendForDoctorAnyTimeInterval(RecommendationRequestDto dto, int remainingSlots)
        {
            if (remainingSlots == 0) return new List<RecommendationDto>();
            
            var selectedDoctorShifts = _shiftWrapper.Repository.GetByDoctorIdAndTimeInterval(
                dto.DoctorId,
                new TimeInterval
                {
                    Start = (dto.TimeInterval.Start.AddMonths(-1) < DateTime.Now ? DateTime.Now : dto.TimeInterval.Start.AddMonths(-1)),
                    End = dto.TimeInterval.End.AddMonths(1)
                }
            );
            return FindRecommendationsInShifts(selectedDoctorShifts, remainingSlots);
        }

        private List<RecommendationDto> RecommendEmergencyInTimeInterval(RecommendationRequestDto dto, int remainingSlots)
        {
            if (remainingSlots == 0) return new List<RecommendationDto>();
            IEnumerable<int> doctorIds =
                _doctorConnection.Get<IEnumerable<int>>($"specialty/ids/{dto.SpecialtyId}");
            IEnumerable<Shift> allShifts = _shiftWrapper.Repository
                .GetMatching(shift =>
                    doctorIds.Contains(shift.DoctorId)
                    && shift.TimeInterval.Start.Date >= dto.TimeInterval.Start.Date
                    && shift.TimeInterval.Start.Date <= dto.TimeInterval.End.Date
                    && shift.TimeInterval.Start.Hour >= dto.TimeInterval.Start.Hour
                    && shift.TimeInterval.Start.Hour <= dto.TimeInterval.End.Hour
                );

            return FindRecommendationsInShifts(allShifts, remainingSlots);
        }

        /// <summary>
        /// Returns a list of <see cref="RecommendationDto"/> objects which can take place within the
        /// given Shift array.
        /// Max number of returned objects is defined by <see cref="maxTimeSlots"/> argument.
        /// </summary>
        /// <param name="shifts"></param>
        /// <param name="maxTimeSlots"></param>
        /// <returns></returns>
        private List<RecommendationDto> FindRecommendationsInShifts(IEnumerable<Shift> shifts, int maxTimeSlots)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            foreach (var shift in shifts)
            {
                if (maxTimeSlots > 0)
                {
                    List<RecommendationDto> singleShiftRecommendations = RecommendForShift(shift, maxTimeSlots);
                    recommendations.AddRange(singleShiftRecommendations);
                    maxTimeSlots -= singleShiftRecommendations.Count;
                }
                else
                    break;
            }
            return recommendations;
        }

        /// <summary>
        /// Returns a list of <see cref="RecommendationDto"/> objects which can take place within the given Shift.
        /// Max number of returned Recommendations is defined by <see cref="maxSlots"/> argument.
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="maxSlots"></param>
        /// <returns></returns>
        private List<RecommendationDto> RecommendForShift(Shift shift, int maxSlots)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            var timeSlots = GetAvailableTimeSlots(shift.DoctorId, shift.TimeInterval, maxSlots);
            var doctor = 
                _doctorConnection.Get<IEnumerable<Doctor>>(shift.DoctorId.ToString())
                    .FirstOrDefault(doc => doc.Id == shift.DoctorId);
            
            if (doctor == default) return recommendations;
            
            foreach (var timeSlot in timeSlots)
            {
                recommendations.Add(new RecommendationDto()
                {
                    Doctor = doctor,
                    TimeInterval = timeSlot,
                    RoomId = shift.AssignedExamRoomId
                });
            }

            return recommendations;
        }
        
        /// <summary>
        /// Returns a list of TimeIntervals in which the Doctor with the given ID is available for the given
        /// <see cref="TimeInterval"/> argument.
        /// Max number of returned free intervals is defined by the <see cref="maxSlots"/> argument.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="timeInterval"></param>
        /// <param name="maxSlots"></param>
        /// <returns></returns>
        private List<TimeInterval> GetAvailableTimeSlots(int doctorId, TimeInterval timeInterval, int maxSlots)
        {
            List<TimeInterval> intervals = new List<TimeInterval>();
            
            var start = timeInterval.Start;
            while (start < timeInterval.End  &&  maxSlots > 0)
            {
                IEnumerable<Examination> examinations
                    = _examinationWrapper.Repository.GetByDoctorAndExaminationStart(doctorId, start).Where(e => !e.IsCanceled);
               
                if (!examinations.Any())
                {
                    intervals.Add(new TimeInterval(start, start.Add(Examination.TimeFrameSize)));
                    maxSlots--;
                }
                start = start.Add(Examination.TimeFrameSize);
            }
            
            return intervals;
        }

        public IEnumerable<EquipmentRelocationDto> RecommendEquipmentRelocation(SchedulingDto dto)
        {
            int dateInterval = dto.TimeInterval.End.Day - dto.TimeInterval.Start.Day;
            IEnumerable<Shift> shiftsForDestinationRoom = _shiftWrapper.Repository.GetByTimeInterval
                (dto.TimeInterval).Where(shift => shift.AssignedExamRoomId == dto.DestinationRoomId);

            IEnumerable<Shift> shiftsForSourceRoom = _shiftWrapper.Repository.GetByTimeInterval
                (dto.TimeInterval).Where(shift => shift.AssignedExamRoomId == dto.SourceRoomId);

            IEnumerable<RecommendationDto> recommendationsSourceRoom = FindRecommendationsInShifts(shiftsForSourceRoom, 20 * dateInterval);
            IEnumerable<RecommendationDto> recommendationsDestinationRoom = FindRecommendationsInShifts(shiftsForDestinationRoom, 20 * dateInterval);

            return FindCommonEquipmentRelocationAppointments(dto, recommendationsSourceRoom, recommendationsDestinationRoom);

        }

        private IEnumerable<EquipmentRelocationDto> FindCommonEquipmentRelocationAppointments(SchedulingDto dto, IEnumerable<RecommendationDto> recsDestinationRoom, IEnumerable<RecommendationDto> recsSourceRoom)
        {
            List<EquipmentRelocationDto> equipmentRelocationDtos = new List<EquipmentRelocationDto>();

            foreach (RecommendationDto recDestination in recsDestinationRoom)
            {
                foreach (RecommendationDto recSource in recsSourceRoom)
                {
                    if (recDestination.TimeInterval.Start == recSource.TimeInterval.Start && recDestination.TimeInterval.End == recSource.TimeInterval.End)
                    {
                        EquipmentRelocationDto eqDto = new EquipmentRelocationDto()
                        {
                            SourceRoomId = dto.SourceRoomId,
                            DestinationRoomId = dto.DestinationRoomId,
                            Amount = 0,
                            TimeInterval = recDestination.TimeInterval
                        };
                        equipmentRelocationDtos.Add(eqDto);
                    }
                    if (equipmentRelocationDtos.Count == 5) return equipmentRelocationDtos;
                }
            }
            return equipmentRelocationDtos;
        }

        public IEnumerable<RoomRenovationDto> RecommendRenovationAppointments(SchedulingDto dto)
        {
            if(dto.DestinationRoomId == -1)
                return BasicRenovationSearch(dto);
            else
                return ComplexRenovationSearch(dto);            
        }

        private IEnumerable<RoomRenovationDto> BasicRenovationSearch(SchedulingDto dto)
        {
            List<RoomRenovationDto> appointments = new List<RoomRenovationDto>();
            List<Shift> availableShiftsSource = new List<Shift>();
            IEnumerable<Shift> shiftsForSourceRoom = _shiftWrapper.Repository.GetShiftsByRoomID(dto.SourceRoomId);
            foreach (Shift shift in shiftsForSourceRoom)
            {
                List<Shift> shiftList = new List<Shift>();
                shiftList.Add(shift);
                IEnumerable<RecommendationDto> recommendationsSourceRoom = FindRecommendationsInShifts(shiftList, MaximumAppointmentsInShift);
                if (recommendationsSourceRoom.Count() == MaximumAppointmentsInShift)
                {
                    availableShiftsSource.Add(shift);
                }
            }
            
            foreach (Shift shift in availableShiftsSource)
            {
                RoomRenovationDto roomRenovationDto = new RoomRenovationDto()
                {
                    SourceRoomId = dto.SourceRoomId,
                    DestinationRoomId = dto.DestinationRoomId,
                    TimeInterval = shift.TimeInterval
                };
                appointments.Add(roomRenovationDto);
            }
            return appointments;
        }

        private IEnumerable<RoomRenovationDto> ComplexRenovationSearch(SchedulingDto dto)
        {
            List<RoomRenovationDto> appointments = new List<RoomRenovationDto>();
            List<Shift> availableShiftsSource = new List<Shift>();
            List<Shift> availableShiftsDestination = new List<Shift>();
            IEnumerable<Shift> shiftsForDestinationRoom = _shiftWrapper.Repository.GetShiftsByRoomID(dto.DestinationRoomId);
            IEnumerable<Shift> shiftsForSourceRoom = _shiftWrapper.Repository.GetShiftsByRoomID(dto.SourceRoomId);
            foreach (Shift shift in shiftsForSourceRoom)
            {
                List<Shift> shiftList = new List<Shift>();
                shiftList.Add(shift);
                IEnumerable<RecommendationDto> recommendationsSourceRoom = FindRecommendationsInShifts(shiftList, MaximumAppointmentsInShift);
                if (recommendationsSourceRoom.Count() == MaximumAppointmentsInShift)
                {
                    availableShiftsSource.Add(shift);
                }
            }
            foreach (Shift shift in shiftsForDestinationRoom)
            {
                List<Shift> shiftList = new List<Shift>();
                shiftList.Add(shift);
                IEnumerable<RecommendationDto> recommendationsDestinationRoom = FindRecommendationsInShifts(shiftList, MaximumAppointmentsInShift);
                if (recommendationsDestinationRoom.Count() == MaximumAppointmentsInShift)
                {
                    availableShiftsDestination.Add(shift);
                }
            }

            List<TimeInterval> availableTimeIntervals = new List<TimeInterval>();
            foreach (Shift shiftSource in availableShiftsSource)
            {
                foreach (Shift shiftDest in availableShiftsDestination)
                {
                    if (shiftDest.TimeInterval.Start.Date == shiftSource.TimeInterval.Start.Date && shiftDest.TimeInterval.End.Date == shiftSource.TimeInterval.End.Date)
                    {
                        availableTimeIntervals.Add(shiftDest.TimeInterval);
                    }
                }
            }
            foreach (TimeInterval timeInterval in availableTimeIntervals)
            {
                RoomRenovationDto roomRenovationDto = new RoomRenovationDto()
                {
                    SourceRoomId = dto.SourceRoomId,
                    DestinationRoomId = dto.DestinationRoomId,
                    TimeInterval = timeInterval
                };
                appointments.Add(roomRenovationDto);
            }
            return appointments;
        }
    }
}