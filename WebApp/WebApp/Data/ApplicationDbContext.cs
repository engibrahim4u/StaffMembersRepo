using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
     
        public DbSet<Setting> Settings { get; set; }
      
        public DbSet<Place> Places { get; set; }
        
        public DbSet<PlaceUser> PlaceUsers { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DayName> DayNames { get; set; }
       
        public DbSet<ScientificLevel> ScientificLevels { get; set; }

       
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<EMail> EMails { get; set; }
       
        public DbSet<News> News { get; set; }
        public DbSet<Event> Events { get; set; }
                public DbSet<EventImage> EventImages { get; set; }
        public DbSet<EventActivity> EventActivities { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Roles");
            });
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<ApplicationUser>().ToTable("Users");

            builder.Seed();
            builder.Entity<EMail>().HasKey(nameof(EMail.Code));


        }
    }
}
