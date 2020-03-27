using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19WebApi.Entities;

namespace Covid19WebApi.DTOs
{
    public class AddVictimLocationsRequest
    {
        public List<VictimVisitedLocation> Locations { get; set; } = new List<VictimVisitedLocation>();
    }

    public class VictimLocationsRequest
    {
        public string CurrentLocation { get; set; }
        public int Radius { get; set; }
    }

    public class LatitudeLongitudeResponse
    {
        public string type { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }
    }
}
