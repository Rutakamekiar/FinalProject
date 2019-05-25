using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PL.Models;
using System.Data.Entity;

namespace PL
{
    public sealed class AuthorizationDbInitializer
        : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            IdentityRole admin = new IdentityRole { Name = Roles.Admin.ToString() };
            IdentityRole manager = new IdentityRole { Name = Roles.Manager.ToString()};
            IdentityRole user = new IdentityRole { Name = Roles.User.ToString() };

            // добавляем роли в бд
            roleManager.Create(admin);
            roleManager.Create(manager);
            roleManager.Create(user);

            // создаем пользователей
            string login = "admin@gmail.com";
            var myAdmin = new ApplicationUser
            {
                Email = login,
                UserName = login
            };
            string password = "myAdmin";
            var result = userManager.Create(myAdmin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(myAdmin.Id, admin.Name);
                userManager.AddToRole(myAdmin.Id, manager.Name);
                userManager.AddToRole(myAdmin.Id, user.Name);
            }

            login = "manager@gmail.com";
            var myManager = new ApplicationUser
            {
                Email = login,
                UserName = login
            };
            password = "manager";
            result = userManager.Create(myManager, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(myManager.Id, manager.Name);
                userManager.AddToRole(myManager.Id, user.Name);
            }

            login = "myUser@gmail.com";
            var myUser = new ApplicationUser
            {
                Email = login,
                UserName = login
            };
            password = "myUser";
            result = userManager.Create(myUser, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(myUser.Id, user.Name);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}