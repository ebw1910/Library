using LibraryCRM.Data.Models;
using LibraryCRM.Models;
using LibraryCRM.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Data
{
    public static class ContextSeed
    {
        public static async Task SeedBookGenre(ApplicationDbContext context)
        {
            context.Database.Migrate();
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(
                new Genre
                {
                    Name = "Фантастика"
                },
                 new Genre
                 {
                     Name = "Приключения"
                 },
                 new Genre
                 {
                     Name = "Роман"
                 },
                 new Genre
                 {
                     Name = "Детектив"
                 }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                new Book
                {
                    Name = "На западном фронте без перемен",
                    Author = "Автор: Эрих Мария Ремарк",
                    GenresID = 3

                },
                new Book
                {
                    Name = "451 градус по Фаренгейту",
                    Author = "Автор: Рэй Брэдбери",
                    GenresID = 1
                },
                new Book
                {
                    Name = "Записки о Шерлоке Холмсе",
                    Author = "Автор: Артур Конан Дойл",
                    GenresID = 4
                },
                new Book
                {
                    Name = "Граф Монте-Кристо",
                    Author = "Автор: Александр Дюма",
                    GenresID = 2
                },
                new Book
                {
                    Name = "Десять негритят",
                    Author = "Автор: Агата Кристи",
                    GenresID = 4
                },
                new Book
                {
                    Name = "1984",
                    Author = "Автор: Джордж Оруэлл",
                    GenresID = 1
                },
                 new Book
                 {
                     Name = "Мотылек",
                     Author = "Автор: Анри Шарьер",
                     GenresID = 2
                 },
                new Book
                {
                    Name = "Триумфальная арка",
                    Author = "Автор: Эрих Мария Ремарк",
                    GenresID = 3
                }
                );
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Librarian.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Storekeeper.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Bob",
                LastName = "Bobo",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Storekeeper.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Librarian.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                }

            }
        }
    }
}
