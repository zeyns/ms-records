using Records.Domain.DTOs;


public static class WorkedHoursHelper

{
    public static Double GetWorkedHours(List<EntryAndExitRecordsDTO> entryAndExitRecords)
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

