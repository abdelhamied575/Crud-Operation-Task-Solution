using Crud_Operation_Task.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Repository.Context
{
    public class AppDbContext:IdentityDbContext<User>
    {


        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);




            // Define role IDs
            string adminRoleId = Guid.NewGuid().ToString();
            string managerRoleId = Guid.NewGuid().ToString();
            string employeeRoleId = Guid.NewGuid().ToString();

            // Seed Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = managerRoleId, Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Id = employeeRoleId, Name = "Employee", NormalizedName = "EMPLOYEE" }
            );

            // Seed Admin User
            string adminUserId = Guid.NewGuid().ToString();
            var adminUser = new User
            {
                Id = adminUserId,
                UserName = "admin",
                FirstName = "Abdelhamied",
                LastName = "Belal",
                Email = "admin@admin.com",
                NormalizedEmail="ADMIN@ADMIN.COM"
            };

            // Set Admin Password
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            builder.Entity<User>().HasData(adminUser);

            // Assign Admin User to System Administrator Role
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId }
            );

        }



    }
}
