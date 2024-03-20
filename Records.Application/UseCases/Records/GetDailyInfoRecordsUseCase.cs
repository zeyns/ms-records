using Records.Application.Interfaces.Records;
using Records.Domain.DTOs;
using Records.Domain.Entities;
using Records.Domain.Interfaces;

namespace Records.Application.UseCases.Records;

public class GetDailyInfoRecordsUseCase(IRecordsRepository RecordsRepository) : IGetDateRecordsInfoUseCase
{
    private readonly IRecordsRepository _RecordsRepository = RecordsRepository;

    public DailyRecordsInfoDTO? Execute(DateOnly date)
    {
        List<Record> dayRecords = _RecordsRepository.GetAllByDate(date);
        return new DailyRecordsInfoDTO() ;
    }

    
}
