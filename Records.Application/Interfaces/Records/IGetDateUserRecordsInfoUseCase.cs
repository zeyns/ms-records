using Records.Domain.DTOs;

namespace Records.Application.Interfaces.Records;

public interface IGetDateUserRecordsInfoUseCase
{
    DateRecordsInfoDTO Execute(Guid userId, DateOnly date);
}