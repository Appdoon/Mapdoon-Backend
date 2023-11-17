using Appdoon.Domain.Commons;
using Appdoon.Domain.Entities.Users;
using Appdoon.Domain.Entities.RoadMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Appdoon.Domain.Entities.Rates
{
    public class RateRoadMap : BaseEntity
    {
        public float Score{get;set;}
        public User User {get;set;}
        public int UserId {get;set;}
        public RoadMap RoadMap {get;set;}
        public int RoadMapId {get;set;}
    }
}