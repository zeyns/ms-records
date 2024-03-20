namespace Records.Domain.DTOs;

public class MonthlyRecordsReport
{
    public Double? WorkedHours { get; set; }
    public List<String>? DatesWithRecords { get; set; }
    public List<EntryAndExitRecordsDTO>? Records { get; set; }
}