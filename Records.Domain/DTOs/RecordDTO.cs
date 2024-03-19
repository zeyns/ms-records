using System.ComponentModel.DataAnnotations;

namespace Records.Domain.DTOs;

public class RecordDTO
{
    [Required]
    public Guid userId { get; set; } = null!;
}