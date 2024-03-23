﻿using Records.Domain.Entities;
using Records.Domain.Interfaces;
using Records.Infrastructure.Contexts;

namespace Records.Infrastructure.Repositories;
public class RecordsRepository(RecordsContext context) : IRecordsRepository
{
    private readonly RecordsContext _context = context;

    public DateTime Create(Guid userId)
    {
        var Record = new Record(userId, DateTime.Now);
        _context.Record.Add(Record);
        _context.SaveChanges();
        return Record.RecordDate;
    }


    public List<Record> GetAllByUserIdAndDate(Guid userId, DateOnly date)
    {
        DateTime startDateTime = date.ToDateTime(TimeOnly.Parse("12:00 AM"));
        DateTime endDateTime = date.ToDateTime(TimeOnly.Parse("11:59 PM"));
        return
        [
            .. _context.Record
                        .Where(r => r.RecordDate >= startDateTime && r.RecordDate <= endDateTime && r.UserId == userId)
                        .OrderBy(r => r.RecordDate)
,
        ];
    }

    public List<Record> GetAllByUserIdAndMonth(Guid userId, int month)
    {
        return
        [
            .. _context.Record
                        .Where(r => r.RecordDate.Month == month && r.UserId == userId)
                        .OrderBy(r => r.RecordDate)
,
        ];
    }
}
