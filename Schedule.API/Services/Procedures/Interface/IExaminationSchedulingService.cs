using Schedule.API.DTOs;
using Schedule.API.Model.Utilities;
using System.Collections.Generic;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IExaminationSchedulingService
    {
        IEnumerable<int> GetUnavailableRooms(SchedulingDto schDto);
        IEnumerable<int> GetDoctorsByRoomsAndShifts(SchedulingDto schDto);
    }
}
