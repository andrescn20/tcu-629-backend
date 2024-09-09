using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;

public static class DbInitializer
{
    public static async Task SeedUsersAndRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Seed Roles
        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed Users
        if (!userManager.Users.Any())
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@example.com"
            };

            var normalUser = new ApplicationUser
            {
                UserName = "user",
                Email = "user@example.com"
            };

            // Create Admin User
            var adminResult = await userManager.CreateAsync(adminUser, "Password123!");
            if (adminResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Create Normal User
            var userResult = await userManager.CreateAsync(normalUser, "Password123!");
            if (userResult.Succeeded)
            {
                await userManager.AddToRoleAsync(normalUser, "User");
            }
        }
    }
    public static void SeedSensorData(MonitoringDbContext context)
    {

        if (!context.TemperatureData.Any())
        {
            var random = new Random();
            var temperatureDataList = new List<TemperatureData>();

            for (int i = 1; i <= 50; i++)
            {
                temperatureDataList.Add(new TemperatureData
                {
                    Temperature = Math.Round((decimal)(random.Next(180, 380) / 10), 2), 
                    Timestamp = DateTime.Now.AddMinutes(-i * 5), 
                    SensorId = 46 
                });
            }

            for (int i = 1; i <= 76; i++)
            {
                temperatureDataList.Add(new TemperatureData
                {
                    Temperature = Math.Round((decimal)(random.Next(180,380) / 10), 2),
                    Timestamp = DateTime.Now.AddMinutes(-i * 5),
                    SensorId = 50
                });
            }

            context.TemperatureData.AddRange(temperatureDataList);
            context.SaveChanges();
        }

    }
}
