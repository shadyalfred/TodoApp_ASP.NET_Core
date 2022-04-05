using Microsoft.AspNetCore.Identity;

namespace TodoApp.Models;

public class User : IdentityUser
{
    public IEnumerable<Todo> Todos { get; set; } = new List<Todo>();
}