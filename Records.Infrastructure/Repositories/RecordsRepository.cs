using Records.Domain.Entities;
using Records.Domain.Interfaces;
using Records.Infrastructure.Contexts;
using System;

namespace Records.Infrastructure.Repositories;
public class RecordsRepository(RecordsContext context) : IRecordsRepository
{
    private readonly RecordsContext _context = context;

    public DateTime Create(Guid userId)
    {
        var Record = new Record(userId, DateTime.UtcNow);
        _context.Record.Add(Record);
        _context.SaveChanges();
        return Record.RecordDate;
    }

    public List<Record> GetAllByDate(DateOnly date)
    {
        DateTime startDateTime = date.ToDateTime(TimeOnly.Parse("12:00 AM"));
        DateTime endDateTime = date.ToDateTime(TimeOnly.Parse("11:59 PM"));
        return _context.Record
            .Where(r => r.RecordDate >= startDateTime && r.RecordDate <= endDateTime)
            .OrderBy(r => r.RecordDate)
            .ToList();
    }
}
