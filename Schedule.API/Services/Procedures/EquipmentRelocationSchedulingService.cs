using General.Repository;
using Schedule.API.DTOs;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Shifts;
using Schedule.API.Services.Procedures.Interface;
using System.Collections.Generic;


namespace Schedule.API.Services.Procedures
{
    public class EquipmentRelocationSchedulingService : IEquipmentRelocationSchedulingService
    {
        private readonly RepositoryWrapper<IShiftRepository> _shiftWrapper;

        public EquipmentRelocationSchedulingService(IShiftRepository shiftRepository)
        {
            this._shiftWrapper = new RepositoryWrapper<IShiftRepository>(shiftRepository);
        }

        public IEnumerable<Room> CheckRoomsAvailability(EquipmentRelocationDto eqRealDto)
        {
            List<Room> availableRooms = new List<Room>();
            foreach (Shift shift in _shiftWrapper.Repository.GetAll())
            {
                if (shift.AssignedExamRoomId == eqRealDto.SourceRoomId
                    || shift.AssignedExamRoomId == eqRealDto.DestinationRoomId)
                {
                    if (!shift.TimeInterval.Contains(eqRealDto.TimeInterval))
                    {
                        availableRooms.Add(shift.AssignedExamRoom);
                    }
                }
            }
            return availableRooms;
        }
    }
}
