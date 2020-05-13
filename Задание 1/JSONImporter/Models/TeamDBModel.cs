using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSONImporter.Models
{
    public class TeamDBModel
    {
        [Key]
        public int messageId { get; set; }
        public int[] teams { get; set; }
    }
}
