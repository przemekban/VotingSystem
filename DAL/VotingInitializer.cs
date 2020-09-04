using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VotingSystem.Models;

namespace VotingSystem.DAL
{
    public class VotingInitializer : DropCreateDatabaseIfModelChanges<VotingContext>
    {
        protected override void Seed(VotingContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));

            var user = new ApplicationUser { UserName = "test@test.com" };
            string passwor = "ZAQ!2wsx";

            userManager.Create(user, passwor);

            userManager.AddToRole(user.Id, "User");

            var admin = new ApplicationUser { UserName = "admin@admin.com" };
            string passworAd = "ZAQ!2wsx";

            userManager.Create(admin, passworAd);

            userManager.AddToRole(admin.Id, "Admin");

            Profile profilUser = new Profile { Login = "test@test.com" };
            Profile profilAdmin = new Profile { Login = "admin@admin.com" };
            context.Profiles.Add(profilUser);
            context.Profiles.Add(profilAdmin);
            context.SaveChanges();

        }
    }
}