using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.RoadMaps;
using Appdoon.Domain.Entities.Users;
using System.Collections.Generic;

namespace Mapdoon.Domain.Entities.Chat
{
	public class ChatMessage : BaseEntity
	{
		public string Message { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }

        public User? Sender { get; set; }
		public int SenderId { get; set; }

        public RoadMap? RoadMap { get; set; }
		public int? RoadMapId { get; set; }

		public ChatMessage? ReplyMessage { get; set; }
        public int? ReplyMessageId { get; set; }
    }
}
