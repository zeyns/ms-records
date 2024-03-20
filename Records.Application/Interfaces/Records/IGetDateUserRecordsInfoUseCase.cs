using Records.Domain.DTOs;

namespace Records.Application.Interfaces.Records;

public interface IGetDateUserRecordsInfoUseCase : IUseCase<DateRecordsInfoDTO?, DateOnly>
{
}