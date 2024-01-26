using Appdoon.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appdoon.Domain.Entities.Users;
using Appdoon.Domain.Entities.RoadMaps;

namespace Mapdoon.Domain.Entities.Paymnet
{
    public class PaymentRecords : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public RoadMap RoadMap { get; set; }
        public int RoadmapId { get; set; }
        public bool IsFinaly { get; set; }
    }
}
