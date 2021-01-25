using Schedule.API.Model.Procedures;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IProcedureService<T> where T: Procedure
    {
        T GetByID(int id);
        T Schedule(T procedure);
        T ScheduleEmergency(T procedure);
        T ScheduleRenovation(T procedure);
    }
}