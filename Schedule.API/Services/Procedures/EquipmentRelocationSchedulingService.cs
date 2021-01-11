using General.Repository;
using Schedule.API.DTOs;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Model.Procedures;
using Schedule.API.Model.Shifts;
using Schedule.API.Services.Procedures.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.API.Services.Procedures
{
    public class EquipmentRelocationSchedulingService : IEquipmentRelocationSchedulingService
    {
        private readonly RepositoryWrapper<IExaminationRepository> _examinationWrapper;
        private readonly RepositoryWrapper<IShiftRepository> _shiftsWrapper;


        public EquipmentRelocationSchedulingService(IExaminationRepository examinationRepository, IShiftRepository shiftRepository)
        {
            this._examinationWrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            this._shiftsWrapper = new RepositoryWrapper<IShiftRepository>(shiftRepository);

        }

        public IEnumerable<int> GetUnavailableRooms(EquipmentRelocationDto eqRealDto)
        {
            HashSet<int> unavailableRoomsIds = new HashSet<int>();
            List<Examination> relocationRoomsExaminations = _examinationWrapper.Repository
                .GetMatching(e => e.RoomId == eqRealDto.SourceRoomId 
                || e.RoomId == eqRealDto.DestinationRoomId).ToList();

            foreach (Examination examination in relocationRoomsExaminations)
            {
                if (eqRealDto.TimeInterval.Overlaps(examination.TimeInterval))
                {
                    unavailableRoomsIds.Add(examination.RoomId);
                }
            }
            return unavailableRoomsIds;
        }

        public IEnumerable<int> GetDoctorsByRoomsAndShifts(EquipmentRelocationDto eqRealDto)
        {
            HashSet<int> doctors = new HashSet<int>();
            List<Shift> shifts = _shiftsWrapper.Repository
                .GetMatching(s => s.AssignedExamRoomId == eqRealDto.SourceRoomId
                || s.AssignedExamRoomId == eqRealDto.DestinationRoomId).ToList();

            foreach (Shift shift in shifts)
            {
                if (shift.TimeInterval.Start.Date == eqRealDto.TimeInterval.Start.Date)
                {
                    doctors.Add(shift.DoctorId);
                }
            }
            return doctors.ToList();
        } 
    }
}
