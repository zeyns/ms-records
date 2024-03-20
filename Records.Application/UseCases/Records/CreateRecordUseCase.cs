using Records.Application.Interfaces.Records;
using Records.Domain.DTOs;
using Records.Domain.Interfaces;

namespace Records.Application.UseCases.Records;

public class CreateRecordUseCase(IRecordsRepository RecordsRepository) : ICreateRecordUseCase
{
    private readonly IRecordsRepository _RecordsRepository = RecordsRepository;

    public DateTime? Execute(RecordDTO dto)
    {
        return _RecordsRepository.Create(dto.UserId);
    }
}
