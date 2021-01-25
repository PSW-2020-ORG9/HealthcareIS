using General.Repository;
using Schedule.API.DTOs;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Model.Procedures;
using Schedule.API.Model.Shifts;
using Schedule.API.Model.Utilities;
using Schedule.API.Services.Procedures.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.API.Services.Procedures
{
    public class ExaminationSchedulingService : IExaminationSchedulingService
    {
        private readonly RepositoryWrapper<IExaminationRepository> _examinationWrapper;
        private readonly RepositoryWrapper<IShiftRepository> _shiftsWrapper;


        public ExaminationSchedulingService(IExaminationRepository examinationRepository, IShiftRepository shiftRepository)
        {
            this._examinationWrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            this._shiftsWrapper = new RepositoryWrapper<IShiftRepository>(shiftRepository);

        }

        public IEnumerable<int> GetUnavailableRooms(SchedulingDto schDto)
        {
            HashSet<int> unavailableRoomsIds = new HashSet<int>();
            List<Examination> relocationRoomsExaminations = _examinationWrapper.Repository
                .GetMatching(e => (e.RoomId == schDto.SourceRoomId) 
                || e.RoomId == schDto.DestinationRoomId).ToList();

            foreach (Examination examination in relocationRoomsExaminations)
            {
                if(examination.TimeInterval.Overlaps(schDto.TimeInterval))
                {
                    unavailableRoomsIds.Add(examination.RoomId);
                }
            }
            return unavailableRoomsIds;
        }

        public IEnumerable<int> GetDoctorsByRoomsAndShifts(SchedulingDto schDto)
        {
            HashSet<int> doctors = new HashSet<int>();
            List<Shift> shifts = _shiftsWrapper.Repository
                .GetMatching(s => (s.AssignedExamRoomId == schDto.SourceRoomId
                || s.AssignedExamRoomId == schDto.DestinationRoomId) 
                && s.TimeInterval.Start.Date == schDto.TimeInterval.Start.Date).ToList();

            foreach (Shift shift in shifts)
            {
                doctors.Add(shift.DoctorId);
            }
            return doctors.ToList();
        }
    }
}
