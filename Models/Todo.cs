#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;

public class Todo
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    [DisplayName("Is done?")]
    public bool IsDone { get; set; }

    [Required]
    public string OwnerId { get; set; } = string.Empty;

    [DisplayName("Created at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual User Owner { get; set; }
}