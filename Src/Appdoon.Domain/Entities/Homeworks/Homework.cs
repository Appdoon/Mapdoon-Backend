using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.Progress;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using System.Collections.Generic;

namespace Appdoon.Domain.Entities.HomeWorks
{
	public class Homework : BaseEntity
	{
		public string Title { get; set; } = string.Empty;
		public string Question { get; set; } = string.Empty;
        public decimal MinScore { get; set; }
		public List<HomeworkProgress> HomeworkProgresses { get; set; } = new();
		// Teacher Actually
		public User? Creator { get; set; }
		public int CreatorId { get; set; }
		public ChildStep? ChildStep { get; set; }
	}
}
