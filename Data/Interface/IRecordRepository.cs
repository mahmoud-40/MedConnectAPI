using System;
using Medical.Models;

namespace Medical.Data.Interface;

public interface IRecordRepository : IGenericRepository<Record>
{
    Task<IEnumerable<Record>> GetByPatientId(string patientId);
    Task<Record?> Get(string patientId, string providerId);
    Task<List<Record>> GetRecordsByProviderId(string providerId);
}
