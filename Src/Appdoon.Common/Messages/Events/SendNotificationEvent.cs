using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Common.Messages.Events
{
	public class SendNotificationEvent
	{
        public string Notification { get; set; }
        public DateTime SendTime { get; set; }
        public int NotificationId { get; set; }
        public int? ReceiverId { get; set; }
    }
}
