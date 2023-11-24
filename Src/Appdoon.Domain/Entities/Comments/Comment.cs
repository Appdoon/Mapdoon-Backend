using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.Replies;
using Appdoon.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Domain.Entities.RoadMaps;

namespace Appdoon.Domain.Entities.Comments
{
    public class Comment : BaseEntity
    {
        public User? User { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt {  get; set; } = DateTime.Now;
        public bool IsEdited { get; set; } = false;
        public RoadMap? Roadmap { get; set; }
        public int? RoadmapId { get; set; }
        public List<Reply> replies { get; set; } = new List<Reply>();
    }
}
