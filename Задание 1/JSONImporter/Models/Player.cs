using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSONImporter
{
    public class Player
    {
        [Key]
        public int pno { get; set; }
        public int personId { get; set; }
        public string familyName { get; set; }
        public string firstName { get; set; }
        public string internationalFamilyName { get; set; }
        public string internationalFirstName { get; set; }
        public string scoreboardName { get; set; }
        public string TVName { get; set; }
        public string nickName { get; set; }
        public string website { get; set; }
        public int height { get; set; }
        public string externalId { get; set; }
        public string internationalReference { get; set; }
        public string shirtNumber { get; set; }
        public string playingPosition { get; set; }
        public bool starter { get; set; }
        public bool captain { get; set; }
        public bool active { get; set; }
        public string nationalityCode { get; set; }
        public string nationalityCodeIOC { get; set; }
        public string nationality { get; set; }
    }
}
