using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSONImporter
{
    public class Teams
    {
        public int teamNumber { get; set; }
        public Detail detail { get; set; }
        public List<Player> players { get; set; }
        public Coach coach { get; set; }
    }
}
