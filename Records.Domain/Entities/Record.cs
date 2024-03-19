namespace Records.Domain.Entities;

public class Record(Guid userId, DateTime recordDate) : Entity
{
    public Guid userId { get; set; } = userId;
    public DateTime recordDate { get; set; } = recordDate;   
}