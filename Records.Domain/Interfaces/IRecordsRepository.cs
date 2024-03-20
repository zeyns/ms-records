using Records.Domain.Entities;

namespace Records.Domain.Interfaces;

public interface IRecordsRepository
{
    DateTime Create(Guid userId);

    List<Record> GetAllByUserIdAndDate(Guid userId, DateOnly date);

    List<Record> GetAllByUserIdAndMonth(Guid userId, int month);
}