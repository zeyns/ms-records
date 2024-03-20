using Records.Domain.DTOs;
using Records.Domain.Entities;

public static class EntryAndExitHelper
{
    public static List<EntryAndExitRecordsDTO> GetEntryAndExitRecords(List<Record> records)
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

    public static List<EntryAndExitRecordsDTO> GetEntryAndExitIntervalRecords(List<EntryAndExitRecordsDTO> entryAndExitRecords)
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

}