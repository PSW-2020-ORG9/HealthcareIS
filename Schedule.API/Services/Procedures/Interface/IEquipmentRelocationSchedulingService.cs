using Schedule.API.DTOs;
using Schedule.API.Model.Dependencies;
using System.Collections.Generic;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IEquipmentRelocationSchedulingService
    {
        IEnumerable<Room> CheckRoomsAvailability(EquipmentRelocationDto eqRealDto);
    }
}
