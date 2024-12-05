using Medical.Models;

namespace Medical.Data.Interface
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetById(int id);
        Task<IEnumerable<Appointment>> GetAll();
        Task Add(Appointment appointment);
        void Update(Appointment appointment);
        void Remove(Appointment appointment);
    }
}
