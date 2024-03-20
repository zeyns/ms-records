using Records.Domain.DTOs;
using Records.Domain.Entities;
using Records.Domain.Interfaces;

namespace Records.Application.UseCases.Records;

public class GetLastMonthlyUserRecordsReports(IRecordsRepository RecordsRepository) : IGetLastMonthlyUserRecordsReports
{
    private readonly IRecordsRepository _RecordsRepository = RecordsRepository;

    public MonthlyRecordsReport Execute(Guid userId)
    {
        List<Record> monthRecords = _RecordsRepository.GetAllByUserIdAndMonth(userId, DateTime.UtcNow.AddMonths(0).Month);
        MonthlyRecordsReport monthlyRecordsReport = new MonthlyRecordsReport();
        monthlyRecordsReport.Records = GetEntryAndExitRecords(monthRecords);
        monthlyRecordsReport.DatesWithRecords = monthRecords.Select(x => x.RecordDate.ToShortDateString()).Distinct().ToList();
        monthlyRecordsReport.WorkedHours = GetWorkedHours(monthlyRecordsReport.Records);
        return monthlyRecordsReport;
    }

    private List<EntryAndExitRecordsDTO> GetEntryAndExitRecords(List<Record> records)
    {
        List<EntryAndExitRecordsDTO> entryAndExitRecords = new List<EntryAndExitRecordsDTO>();
        for (int i = 0; i < records.Count; i += 2)
        {
            int nextPosition = i + 1;
            var entryAndExit = new EntryAndExitRecordsDTO();
            entryAndExit.Entry = records[i].RecordDate;
            entryAndExit.Exit = nextPosition < records.Count
                ? records[i + 1].RecordDate
                : null;
            entryAndExitRecords.Add(entryAndExit);
        }
        return entryAndExitRecords;
    }

    private Double GetWorkedHours(List<EntryAndExitRecordsDTO> entryAndExitRecords)
    {
        Double workedHours = 0d;
        foreach (var entryAndExitRecord in entryAndExitRecords)
        {
            workedHours += (entryAndExitRecord.Exit ?? DateTime.UtcNow)
                .Subtract(entryAndExitRecord.Entry ?? DateTime.UtcNow)
                .TotalHours;
        }
        return Math.Round(workedHours, 2);

    }





}
