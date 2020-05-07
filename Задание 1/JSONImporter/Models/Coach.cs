using System;
using System.Collections.Generic;
using System.Text;

namespace JSONImporter
{
    public class Coach
    {
        public int personId { get; set; }
        public string familyName { get; set; }
        public string firstName { get; set; }
        public string internationalFamilyName { get; set; }
        public string internationalFirstName { get; set; }
        public string scoreBoardName { get; set; }
        public string TVName { get; set; }
        public string nickName { get; set; }
        public string externalId { get; set; }
        public string nationalityCode { get; set; }
        public string nationalityCodeIOC { get; set; }
        public string nationality { get; set; }
    }
}
