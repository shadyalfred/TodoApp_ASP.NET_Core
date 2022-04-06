using Microsoft.AspNetCore.Identity;
using TodoApp.Models;

namespace TodoApp.Data;

public class SeedDb
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        SeedRoles(serviceProvider).Wait();
        SeedUsers(serviceProvider).Wait();
        SeedTodos(serviceProvider).Wait();
    }

    private static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");

        if (adminRole is null)
        {
            adminRole = new IdentityRole("Admin");
            await roleManager.CreateAsync(adminRole);
        }

        IdentityRole userRole = await roleManager.FindByNameAsync("User");

        if (userRole is null)
        {
            userRole = new IdentityRole("User");
            await roleManager.CreateAsync(userRole);
        }
    }

    private static async Task SeedUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        var admin = await userManager.FindByEmailAsync("admin@admin.com");

        if (admin is null)
        {
            var userStore = serviceProvider.GetRequiredService<IUserStore<User>>();
            var emailStore = (IUserEmailStore<User>)userStore;

            admin = new User();
            admin.UserName = "admin@admin.com";
            admin.Email = "admin@admin.com";
            admin.EmailConfirmed = true;

            await userManager.CreateAsync(admin, "P@ssword0");

            await userManager.AddToRoleAsync(admin, "Admin");
        }

        User? user = (await userManager.GetUsersInRoleAsync("User")).FirstOrDefault();

        if (user is null)
        {
            user = new User();
            user.UserName = "user@users.com";
            user.Email = "user@users.com";
            user.EmailConfirmed = true;

            await userManager.CreateAsync(user, "P@ssword0");
            await userManager.AddToRoleAsync(user, "User");
        }
    }

    private static async Task SeedTodos(IServiceProvider serviceProvider)
    {
        var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

        if (db.Todos.Any())
        {
            return;
        }

        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        User user = (await userManager.GetUsersInRoleAsync("User")).First();
        Todo[] todos = new Todo[]
        {
            new Todo() { Title = "First todo", Body = "Buy milk", OwnerId = user.Id },
            new Todo() { Title = "Second todo", Body = "Buy cheese", OwnerId = user.Id },
            new Todo() { Title = "Third todo", Body = "Buy flour", OwnerId = user.Id },
        };

        foreach (var todo in todos)
        {
            db.Todos.Add(todo);
        }

        db.SaveChanges();
    }
}