using Microsoft.AspNetCore.Identity;
using RakSport.Domain.Models.User;

namespace RakSport.Infrastructure.Seeder
{
    public class IdentitySeeder
    {
        public static async Task SeedInformations(UserManager<UserEntity> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var roles = new (string RoleName, Guid RoleId)[]
            {
                ("admin", Guid.Parse("e93b3ba5-43f2-4f8a-bea1-a0467ff7e168")),

                ("user",Guid.Parse("5f830072-f834-4a6f-9cec-ae35daa73279"))
            };

            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role.RoleName);

                if (!roleExist)
                {
                    var identityRole = new IdentityRole<Guid>(role.RoleName);

                    await roleManager.CreateAsync(identityRole);
                }
            }

            var adminId = Guid.Parse("e93b3ba5-43f2-4f8a-bea1-a0467ff7e168");

            string AdminFullName = "RakSport Admin";

            string AdminEmail = "RakSport.Admin@gmail.com";

            string AdminPassword = "R@kS2025!Secure#Admin";

            string AdminPhoneNumber = "09397438089";


            var existingUser = await userManager.FindByIdAsync(adminId.ToString());

            if (existingUser == null)
            {
                var user = new UserEntity(adminId, AdminPhoneNumber, "admin")
                {
                    Email = AdminEmail,

                    PhoneNumber = AdminPhoneNumber,
                };

                user.SetFullName(AdminFullName);

                var createResult = await userManager.CreateAsync(user, AdminPassword);

                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin");
                }
                else
                {
                    throw new Exception("Error occurred while creating the admin user.");
                }
            }
        }
    }
}


