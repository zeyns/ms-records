using Records.Application.Interfaces.Records;
using Records.Domain.DTOs;
using Records.Domain.Entities;
using Records.Domain.Interfaces;

namespace Records.Application.UseCases.Records;

public class GetDateUserRecordsInfoUseCase(IRecordsRepository RecordsRepository) : IGetDateUserRecordsInfoUseCase
{
    private readonly IRecordsRepository _RecordsRepository = RecordsRepository;

    public DateRecordsInfoDTO Execute(Guid userId, DateOnly date)
    {
        List<Record> dateRecords = _RecordsRepository.GetAllByUserIdAndDate(userId, date);
        DateRecordsInfoDTO dateRecordsInfoDTO = new DateRecordsInfoDTO();
        dateRecordsInfoDTO.Records = EntryAndExitHelper.GetEntryAndExitRecords(dateRecords);
        dateRecordsInfoDTO.Intervals = EntryAndExitHelper.GetEntryAndExitIntervalRecords(dateRecordsInfoDTO.Records);
        dateRecordsInfoDTO.WorkedHours = WorkedHoursHelper.GetWorkedHours(dateRecordsInfoDTO.Records);
        return dateRecordsInfoDTO;
    }
}
