using Schedule.API.DTOs;
using System;
using System.Collections.Generic;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IEquipmentRelocationSchedulingService
    {
        IEnumerable<int> GetUnavailableRooms(EquipmentRelocationDto eqRealDto);
        IEnumerable<int> GetDoctorsByRoomsAndShifts(EquipmentRelocationDto eqRealDto);

    }
}
