using System;
using System.Collections.Generic;
using System.Text;

namespace JSONImporter.Models
{
    class BulkTeams
    {
        public List<TeamsDBModel> teams { get; set; }
        public List<TeamDBModel> team { get; set; }
        public List<Player> players { get; set; }
        public List<Coach> coach { get; set; }
        public List<Detail> detail { get; set; }
    }
}
