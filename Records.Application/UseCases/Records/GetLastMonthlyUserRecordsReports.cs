using Records.Domain.DTOs;
using Records.Domain.Entities;
using Records.Domain.Interfaces;

namespace Records.Application.UseCases.Records;

public class GetLastMonthlyUserRecordsReports(IRecordsRepository RecordsRepository) : IGetLastMonthlyUserRecordsReports
{
    private readonly IRecordsRepository _RecordsRepository = RecordsRepository;

    public MonthlyRecordsReport Execute(Guid userId)
    {
        List<Record> monthRecords = _RecordsRepository.GetAllByUserIdAndMonth(userId, DateTime.UtcNow.AddMonths(-1).Month);
        MonthlyRecordsReport monthlyRecordsReport = new MonthlyRecordsReport();
        monthlyRecordsReport.Records = EntryAndExitHelper.GetEntryAndExitRecords(monthRecords);
        monthlyRecordsReport.DatesWithRecords = monthRecords.Select(x => x.RecordDate.ToShortDateString()).Distinct().ToList();
        monthlyRecordsReport.WorkedHours = WorkedHoursHelper.GetWorkedHours(monthlyRecordsReport.Records);
        return monthlyRecordsReport;
    }
}
