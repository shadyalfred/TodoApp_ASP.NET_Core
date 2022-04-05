using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TodoApp.Data;

public class SeedDb
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        SeedRoles(serviceProvider).Wait();
        SeedUsers(serviceProvider).Wait();
        SeedTodos(serviceProvider);
    }

    private static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");

        if (adminRole is null)
        {
            adminRole = new IdentityRole("Admin");
            roleManager.CreateAsync(adminRole).Wait();
        }
    }

    private static async Task SeedUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var admin = await userManager.FindByEmailAsync("admin@admin.com");

        if (admin is null)
        {
            var userStore = serviceProvider.GetRequiredService<IUserStore<IdentityUser>>();
            var emailStore = (IUserEmailStore<IdentityUser>)userStore;

            admin = new IdentityUser();
            admin.UserName = "admin@admin.com";
            admin.Email = "admin@admin.com";
            admin.EmailConfirmed = true;

            userManager.CreateAsync(admin, "P@ssword0").Wait();

            userManager.AddToRoleAsync(admin, "Admin").Wait();
        }

    }

    private static void SeedTodos(IServiceProvider serviceProvider)
    {
    }
}