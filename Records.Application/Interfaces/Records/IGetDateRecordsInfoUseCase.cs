using Records.Domain.DTOs;

namespace Records.Application.Interfaces.Records;

public interface IGetDateRecordsInfoUseCase : IUseCase<DateRecordsInfoDTO?, DateOnly>
{
}