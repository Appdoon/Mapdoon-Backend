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
        [Range(1, 5)]
        public float Question1 {get;set;}
        [Range(1, 5)]
        public float Question2 {get;set;}
        [Range(1, 5)]
        public float Question3 {get;set;}
        [Range(1, 5)]
        public float Question4 {get;set;}
        [Range(1, 5)]
        public float Question5 {get;set;}
        public User User {get;set;}
        public int UserId {get;set;}
        public RoadMap RoadMap {get;set;}
        public int RoadMapId {get;set;}
    }
}