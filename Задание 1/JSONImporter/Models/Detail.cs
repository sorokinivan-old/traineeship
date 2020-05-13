using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSONImporter
{
    public class Detail
    {
        [Key]
        public int detailId { get; set; }
        public string teamName { get; set; }
        public string teamNameInternational { get; set; }
        public string teamId { get; set; }
        public string externalId { get; set; }
        public string internationalReference { get; set; }
        public string teamNickname { get; set; }
        public string teamCode { get; set; }
        public string teamCodeLong { get; set; }
        public string teamCodeInternational { get; set; }
        public string teamCodeLongInternational { get; set; }
        public string teamNickNameInternational { get; set; }
        public string countryCode { get; set; }
        public string countryCodeIOC { get; set; }
        public string country { get; set; }
        public string website { get; set; }
        public bool isHomeCompetitor { get; set; }
    }
}
