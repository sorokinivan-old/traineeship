using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JSONImporter.Models
{
    public class TeamsDBModel
    {
        [Key]
        public int teamNumber { get; set; }
        [ForeignKey("Detail")]
        public int detail { get; set; }
        public int[] players { get; set; }
        [ForeignKey("Coach")]
        public int coach { get; set; }
    }
}
