using Records.Application.Interfaces.Records;
using Records.Domain.DTOs;
using Records.Domain.Entities;
using Records.Domain.Interfaces;

namespace Records.Application.UseCases.Records;

public class GetDateUserRecordsInfoUseCase(IRecordsRepository RecordsRepository) : IGetDateUserRecordsInfoUseCase
{
    private readonly IRecordsRepository _RecordsRepository = RecordsRepository;

    public DateRecordsInfoDTO Execute(DateOnly date)
    {
        List<Record> dateRecords = _RecordsRepository.GetAllByUserIdAndDate(Guid.Empty, date);
        DateRecordsInfoDTO dateRecordsInfoDTO = new DateRecordsInfoDTO();
        dateRecordsInfoDTO.Records = GetEntryAndExitRecords(dateRecords);
        dateRecordsInfoDTO.Intervals = GetEntryAndExitIntervalRecords(dateRecordsInfoDTO.Records);
        dateRecordsInfoDTO.WorkedHours = GetWorkedHours(dateRecordsInfoDTO.Records);
        return dateRecordsInfoDTO;
    }

    private List<EntryAndExitRecordsDTO> GetEntryAndExitRecords(List<Record> records)
    {
        List<EntryAndExitRecordsDTO> entryAndExitRecords = new List<EntryAndExitRecordsDTO>();
        for(int i = 0; i < records.Count; i += 2) {
            int nextPosition = i + 1;
            var entryAndExit = new EntryAndExitRecordsDTO();
            entryAndExit.Entry = records[i].RecordDate;
            entryAndExit.Exit = nextPosition < records.Count
                ? records[i+1].RecordDate
                : null;
            entryAndExitRecords.Add(entryAndExit);
        }
        return entryAndExitRecords;
    }

    private List<EntryAndExitRecordsDTO> GetEntryAndExitIntervalRecords(List<EntryAndExitRecordsDTO> entryAndExitRecords)
    {
        List<EntryAndExitRecordsDTO> entryAndExitIntervalRecords = new List<EntryAndExitRecordsDTO>();
        for (int i = 0; i < entryAndExitRecords.Count; i += 2)
        {
            int nextPosition = i + 1;
            var entryAndExit = new EntryAndExitRecordsDTO();
            entryAndExit.Entry = entryAndExitRecords[i].Exit;
            entryAndExit.Exit = entryAndExitRecords[nextPosition].Entry;
            entryAndExitIntervalRecords.Add(entryAndExit);
        }
        return entryAndExitIntervalRecords;

    }

    private Double GetWorkedHours(List<EntryAndExitRecordsDTO> entryAndExitRecords)
    {
        Double workedHours = 0d;
        foreach(var entryAndExitRecord in entryAndExitRecords)
        {
            workedHours += (entryAndExitRecord.Exit ?? DateTime.UtcNow)
                .Subtract(entryAndExitRecord.Entry ?? DateTime.UtcNow)
                .TotalHours;
        }
        return Math.Round(workedHours, 2);

    }

    



}
