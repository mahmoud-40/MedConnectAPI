using System;
using Medical.Data.Interface;
using Medical.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical.Data.Repository;

public class RecordRepository : GenericRepository<Record>, IRecordRepository
{
    private readonly MedicalContext db;

    public RecordRepository(MedicalContext _db) : base(_db)
    {
        db = _db;
    }

    public async Task<IEnumerable<Record>> GetByPatientId(string patientId)
    {
        return await db.Records.Where(e => e.PatientId == patientId).ToListAsync();
    }

    public async Task<Record?> Get(string patientId, string providerId)
    {
        return await db.Records.SingleOrDefaultAsync(e => e.PatientId == patientId && e.ProviderId == providerId);
    }

    public async Task<List<Record>> GetRecordsByProviderId(string providerId)
    {
        return await db.Records.Where(e => e.ProviderId == providerId).ToListAsync();
    }
}
