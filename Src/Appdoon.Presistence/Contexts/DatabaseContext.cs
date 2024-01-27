using Appdoon.Application.Interfaces;
using Appdoon.Domain.Commons;
using Mapdoon.Domain.Entities.Paymnet;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.Rates;
using Appdoon.Domain.Entities.Replies;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using Mapdoon.Common.User;
using Mapdoon.Domain.Entities.Chat;
using Mapdoon.Domain.Entities.Notification;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using ZarinPal.Class;

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
		public DbSet<RateRoadMap> Rates { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Reply> Replies { get; set; }
		public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
		public DbSet<PaymentRecords> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>().HasData(new Role() { Name = UserRole.Admin.ToString(), Id = (int)UserRole.Admin });
			modelBuilder.Entity<Role>().HasData(new Role() { Name = UserRole.Teacher.ToString(), Id = (int)UserRole.Teacher });
			modelBuilder.Entity<Role>().HasData(new Role() { Name = UserRole.User.ToString(), Id = (int)UserRole.User });

			// Apply Query filter for IsRemoved property
			Expression<Func<BaseEntity, bool>> filterExpr = e => !e.IsRemoved;
			foreach(var mutableEntityType in modelBuilder.Model.GetEntityTypes())
			{
				if(mutableEntityType.ClrType.IsAssignableTo(typeof(BaseEntity)))
				{
					var parameter = Expression.Parameter(mutableEntityType.ClrType);
					var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
					var lambdaExpression = Expression.Lambda(body, parameter);

					// set filter
					mutableEntityType.SetQueryFilter(lambdaExpression);
				}
			}

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

			// HomeworkProgress and User
			//modelBuilder.Entity<HomeworkProgress>()
			//	.HasOne(h => h.User)
			//	.WithMany(u => u.HomeworkProgresses)
			//	.HasForeignKey(h => h.UserId)
			//	.OnDelete(DeleteBehavior.NoAction);


   //         // Homework Progress and Homework
   //         modelBuilder.Entity<HomeworkProgress>()
			//	.HasOne(h => h.Homework)
			//	.WithMany(h => h.HomeworkProgresses)
			//	.HasForeignKey(h => h.HomeworkId)
			//	.OnDelete(DeleteBehavior.NoAction);



			//var temp = Database.GetPendingMigrations();
			//Database.MigrateAsync();
		}
	}
}
