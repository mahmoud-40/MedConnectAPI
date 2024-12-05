using Medical.Data.Interface;
using Medical.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical.Data.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DbContext _context;

        public AppointmentRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> GetById(int id)
        {
            return await _context.Set<Appointment>().FindAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _context.Set<Appointment>().ToListAsync();
        }

        public async Task Add(Appointment appointment)
        {
            await _context.Set<Appointment>().AddAsync(appointment);
        }

        public void Update(Appointment appointment)
        {
            _context.Set<Appointment>().Update(appointment);
        }

        public void Remove(Appointment appointment)
        {
            _context.Set<Appointment>().Remove(appointment);
        }
    }
}
