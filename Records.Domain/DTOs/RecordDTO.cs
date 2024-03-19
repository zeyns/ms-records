using System.ComponentModel.DataAnnotations;

namespace Records.Domain.DTOs;

public class RecordDTO
{
    [Required]
    public Guid UserId { get; set; }
}