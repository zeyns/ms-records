namespace Records.Domain.DTOs;

public class DateRecordsInfoDTO
{
    public  Double? WorkedHours { get; set; }
    public  List<EntryAndExitRecordsDTO>? Intervals { get; set; }
    public  List<EntryAndExitRecordsDTO>? Records { get; set; }
}