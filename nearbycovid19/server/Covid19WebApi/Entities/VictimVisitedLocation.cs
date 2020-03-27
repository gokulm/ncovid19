using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoltOn.Data;

namespace Covid19WebApi.Entities
{
    public class VictimVisitedLocation : BaseEntity<Guid>
    {
        public string VisitedDate { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
