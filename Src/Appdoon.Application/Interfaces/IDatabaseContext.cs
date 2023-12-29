using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Rates;
using Appdoon.Domain.Entities.Users;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Domain.Entities.Replies;
using Mapdoon.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mapdoon.Domain.Entities.Chat;
using Mapdoon.Domain.Entities.Notification;

namespace Appdoon.Application.Interfaces
{
	public interface IDatabaseContext : ITransientService
	{
		DbSet<User> Users { get; set; }
		DbSet<Role> Roles { get; set; }
		DbSet<RoadMap> RoadMaps { get; set; }
		DbSet<Category> Categories { get; set; }
		DbSet<Step> Steps { get; set; }
		DbSet<ChildStep> ChildSteps { get; set; }
		DbSet<Linker> Linkers { get; set; }
		DbSet<Lesson> Lessons { get; set; }
		DbSet<StepProgress> StepProgresses { get; set; }
		DbSet<ChildStepProgress> ChildStepProgresses { get; set; }
		DbSet<Homework> Homeworks { get; set; }
		DbSet<HomeworkProgress> HomeworkProgresses { get; set; }
		DbSet<RateRoadMap> Rates { get; set; }
		DbSet<Comment> Comments { get; set; }
		DbSet<Reply> Replies { get; set; }	
		DbSet<ChatMessage> ChatMessages { get; set; }
		DbSet<Notification> Notifications { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		public DatabaseFacade Database { get; }

		EntityEntry Entry(object entity);
    }
}
