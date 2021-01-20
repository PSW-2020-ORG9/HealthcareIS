using Schedule.API.Model.Utilities;
using System.Collections.Generic;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IExaminationSchedulingService
    {
        IEnumerable<int> GetUnavailableRooms(int firstRoom, int secondRoom, TimeInterval timeInterval);
        IEnumerable<int> GetDoctorsByRoomsAndShifts(int firstRoom, int secondRoom, TimeInterval timeInterval);
    }
}
