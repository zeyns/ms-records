namespace Records.Domain.DTOs;

public class DailyRecordsInfoDTO
{
    public  Int16? WorkedHours { get; set; }
    public  List<EntryAndExitRecordsDTO>? Intervals { get; set; }
    public  List<EntryAndExitRecordsDTO>? Records { get; set; }
}