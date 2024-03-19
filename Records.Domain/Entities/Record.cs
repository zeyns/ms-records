namespace Records.Domain.Entities;

public class Record(Guid userId, DateTime recordDate) 
{
    public Guid UserId { get; set; } = userId;
    public DateTime RecordDate { get; set; } = recordDate;   
}