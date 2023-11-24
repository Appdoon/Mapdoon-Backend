using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.Comments;
using Appdoon.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdoon.Domain.Entities.Replies
{
    public class Reply : BaseEntity
    {
        public User? User { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsEdited { get; set; } = false;
        public Comment? comment { get; set; }
        public int? commentId { get; set; }
    }
}
