using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.HomeWorks;
using Appdoon.Domain.Entities.Users;

namespace Appdoon.Domain.Entities.Progress
{
	public class HomeworkProgress : BaseEntity
	{
        public string Answer { get; set; } = string.Empty;
        public Homework? Homework { get; set; }
		public int HomeworkId { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
		public decimal? Score { get; set; }
		public bool IsDone { get; set; } = false;
	}
}
