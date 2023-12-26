using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.Users;

namespace Mapdoon.Domain.Entities.Notification
{
	public class Notification : BaseEntity
	{
		public User? Receiver { get; set; }
		public int? ReceiverId { get; set; }

        public string Message { get; set; } = string.Empty;

		public bool IsSeen { get; set; } = false;
    }
}
