using Schedule.API.Model.Procedures;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IProcedureService<T> where T: Procedure
    {
        public T GetByID(int id);
        public T Schedule(T procedure);
        public T ScheduleEmergency(T procedure);
    }
}