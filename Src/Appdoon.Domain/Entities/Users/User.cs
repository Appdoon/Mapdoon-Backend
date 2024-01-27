using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.Rates;
using Appdoon.Domain.Entities.RoadMaps;
using Mapdoon.Domain.Entities.Notification;
using System.Collections.Generic;

namespace Appdoon.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string ProfileImageSrc { get; set; } = string.Empty;
        public List<Role> Roles { get; set; } = new();
        public List<RoadMap> SignedRoadMaps { get; set; } = new();
        public List<RoadMap> BookmarkedRoadMaps { get; set; } = new();
        public List<RoadMap> CreatedRoadMaps { get; set; } = new();

        public List<StepProgress> StepProgresses { get; set; } = new();
        public List<ChildStepProgress> ChildStepProgresses { get; set; } = new();
        public List<HomeworkProgress> HomeworkProgresses { get; set; } = new();
        public List<Lesson> CreatedLessons { get; set; } = new();
        public List<Homework> CreatedHomeworks { get; set; } = new();
        public List<RateRoadMap> Rates { get; set; } = new();
        public List<Notification> UserNotifications { get; set; } = new();
    }
}
