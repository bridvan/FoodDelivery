using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccessLayer
{

    // inherit Identity Db Contest for Identity  provide Riole And User
    public class LearningDbContext:IdentityDbContext<AppUser,AppRole,string>
    {
        private readonly IConfigurationRoot _config;

        // provide Option Parameter to IdentityDbContext Class 
        public LearningDbContext(DbContextOptions<LearningDbContext>  options,IConfigurationRoot config)
            :base(options)
        {
            _config = config;
        }
            
        // Db set Of User And Role
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<OtherLocation> OtherLocations { get; set; }
        //override this Methods to use sql server and provide Connection Strings
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:LearningDbConnectionString"]);
            //base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<AppRole>().ToTable("Roles");
            modelBuilder.Entity<OtherLocation>().ToTable("OtherLocation");
        }
    }
}
