using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;

public class Todo
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public bool IsDone { get; set; }

    [Required]
    public string OwnerId { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual User Owner { get; set; } = default!;
}