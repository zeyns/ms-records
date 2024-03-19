using Records.Domain.Entities;
using Records.Domain.Interfaces;
using Records.Infrastructure.Contexts;

namespace Records.Infrastructure.Repositories;

public class RecordsRepository(RecordsContext context) : IRecordsRepository
{
    private readonly RecordsContext _context = context;

    public DateTime Create(Guid userId)
    {
        var Record = new Record(dto.userId, utcNow);
        _context.Record.Add(Record);
        _context.SaveChanges();
        return Record.recordDate;
    }
}
