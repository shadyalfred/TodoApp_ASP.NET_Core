using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TodoApp.Models;

public class Todo
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public bool IsDone { get; set; }

    [Required]
    public string OwnerId { get; set; } = string.Empty;

    public virtual IdentityUser Owner { get; set; } = default!;
}