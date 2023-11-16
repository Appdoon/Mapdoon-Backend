using Appdoon.Application.Interfaces;
using Appdoon.Domain.Entities.Homeworks;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.Rates;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Common.User;
using Microsoft.EntityFrameworkCore;

namespace Appdoon.Presistence.Contexts
{
	public class DatabaseContext : DbContext, IDatabaseContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<RoadMap> RoadMaps { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Step> Steps { get; set; }
		public DbSet<ChildStep> ChildSteps { get; set; }
		public DbSet<Linker> Linkers { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<StepProgress> StepProgresses { get; set; }
		public DbSet<ChildStepProgress> ChildStepProgresses { get; set; }
        public DbSet<HomeworkProgress> HomeworkProgresses { get; set; }
		public DbSet<Homework> Homeworks { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<RateRoadMap> Rates { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>().HasData(new Role() { Name = UserRole.Admin.ToString(), Id = (int)UserRole.Admin });
			modelBuilder.Entity<Role>().HasData(new Role() { Name = UserRole.Teacher.ToString(), Id = (int)UserRole.Teacher });
			modelBuilder.Entity<Role>().HasData(new Role() { Name = UserRole.User.ToString(), Id = (int)UserRole.User });

			modelBuilder.Entity<User>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<Role>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<RoadMap>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<Category>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<Step>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<ChildStep>().HasQueryFilter(u => u.IsRemoved == false);

			modelBuilder.Entity<Linker>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<Lesson>().HasQueryFilter(u => u.IsRemoved == false);

			modelBuilder.Entity<StepProgress>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<ChildStepProgress>().HasQueryFilter(u => u.IsRemoved == false);

			modelBuilder.Entity<Homework>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<HomeworkProgress>().HasQueryFilter(u => u.IsRemoved == false);
			modelBuilder.Entity<Question>().HasQueryFilter(u => u.IsRemoved == false);

			modelBuilder.Entity<RateRoadMap>().HasQueryFilter(u => u.IsRemoved == false);

            // Registerd RoadMaps for User
            modelBuilder.Entity<User>()
				.HasMany<RoadMap>(u => u.SignedRoadMaps)
				.WithMany(r => r.Students);

			// Bookmarked RoadMap for User
			modelBuilder.Entity<User>()
				.HasMany<RoadMap>(u => u.BookmarkedRoadMaps)
				.WithMany(r => r.UsersBookmarked);

			// Creatore of RoadMap (Not Null)
			modelBuilder.Entity<RoadMap>()
				.HasOne(r => r.Creatore)
				.WithMany(u => u.CreatedRoadMaps)
				.HasForeignKey(r => r.CreatoreId)
				.OnDelete(DeleteBehavior.NoAction);
            // User and Created Homeworks
            //modelBuilder.Entity<User>()
            //	.HasMany<Homework>(u => u.CreatedHomeworks)
            //	.WithOne(h => h.Creator)
            //	.HasForeignKey(h => h.CreatorId)
            //	.OnDelete(DeleteBehavior.NoAction);

            //// HomeworkProgress and User
            //modelBuilder.Entity<HomeworkProgress>()
            //	.HasOne(h => h.User)
            //	.WithMany(u => u.HomeworkProgresses)
            //	.HasForeignKey(h => h.UserId)
            //	.OnDelete(DeleteBehavior.NoAction);

            //// Homework Progress and Homework
            //modelBuilder.Entity<HomeworkProgress>()
            //	.HasOne(h => h.Homework)
            //	.WithMany(h => h.HomeworkProgresses)
            //	.HasForeignKey(h => h.HomeworkId)
            //	.OnDelete(DeleteBehavior.NoAction);

            // // fk user and rate
            // modelBuilder.Entity<RateRoadMap>()
            // .HasOne(r => r.User)
            // .WithOne()
            // .HasForeignKey<RateRoadMap>(r => r.UserId);
            // //fk roadmap and rate
            // modelBuilder.Entity<RateRoadMap>()
            // .HasOne(r => r.RoadMap)
            // .WithOne()
            // .HasForeignKey<RateRoadMap>(r => r.RoadMapId);

            //var temp = Database.GetPendingMigrations();
            //Database.MigrateAsync();
        }
	}
}
