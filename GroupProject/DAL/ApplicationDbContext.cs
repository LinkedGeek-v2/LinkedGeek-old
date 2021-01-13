using GroupProject.Models;
using GroupProject.Models.AssociativeModels;
using GroupProject.Models.CompanyModels;
using GroupProject.Models.DeveloperModels;
using GroupProject.Models.FixedModels;
using GroupProject.Models.SharedModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GroupProject.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<SkillsRequired> SkillsRequired { get; set; }
        public DbSet<JobsApplied> JobsApplied { get; set; }
        public DbSet<DeveloperSkills> DeveloperSkills { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        public ApplicationDbContext() : base("GroupProjectContext", throwIfV1Schema: false)
        { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(u => u.Followers)
               .WithRequired(f => f.Followee)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<JobsApplied>()
               .HasRequired(ja => ja.Job)
               .WithMany(j => j.JobsApplied)
               .HasForeignKey(ja => ja.JobID)
               .WillCascadeOnDelete();
        }
    }
}