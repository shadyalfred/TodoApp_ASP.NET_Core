#nullable disable

using TodoApp.Models;

namespace TodoApp.ViewModels.Todos;

public class IndexCreate
{
    public Todo Todo { get; set; }
    public IEnumerable<Todo> Todos { get; set; }
}