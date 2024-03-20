using Records.Domain.Entities;

namespace Records.Domain.Interfaces;

public interface IRecordsRepository
{
    DateTime Create(Guid userId);

    List<Record> GetAllByDate(DateOnly date);
}