using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSONImporter.Models
{
    public class TeamsDBModel
    {
        [Key]
        public int teamNumber { get; set; }
        public int detail { get; set; }
        public int[] players { get; set; }
        public int coach { get; set; }
    }
}
