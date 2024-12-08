using System.ComponentModel.DataAnnotations;

namespace Medical.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
