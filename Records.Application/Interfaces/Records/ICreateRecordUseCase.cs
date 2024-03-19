using Records.Domain.DTOs;

namespace Records.Application.Interfaces.Records;

public interface ICreateRecordUseCase : IUseCase<DateTime?, RecordDTO>
{
}