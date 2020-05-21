using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace JSONImporter
{
    [ProtoContract(SkipConstructor = true)]
    public class Detail
    {
        [Key]
        [ProtoMember(1)]
        [ForeignKey("detail")]
        public int detailId { get; set; }
        [ProtoMember(2)]
        public string teamName { get; set; }
        [ProtoMember(3)]
        public string teamNameInternational { get; set; }
        [ProtoMember(4)]
        public string teamId { get; set; }
        [ProtoMember(5)]
        public string externalId { get; set; }
        [ProtoMember(6)]
        public string internationalReference { get; set; }
        [ProtoMember(7)]
        public string teamNickname { get; set; }
        [ProtoMember(8)]
        public string teamCode { get; set; }
        [ProtoMember(9)]
        public string teamCodeLong { get; set; }
        [ProtoMember(10)]
        public string teamCodeInternational { get; set; }
        [ProtoMember(11)]
        public string teamCodeLongInternational { get; set; }
        [ProtoMember(12)]
        public string teamNickNameInternational { get; set; }
        [ProtoMember(13)]
        public string countryCode { get; set; }
        [ProtoMember(14)]
        public string countryCodeIOC { get; set; }
        [ProtoMember(15)]
        public string country { get; set; }
        [ProtoMember(16)]
        public string website { get; set; }
        [ProtoMember(17)]
        public bool isHomeCompetitor { get; set; }

        public static byte[] ProtoSerialize<T>(T record) where T : class
        {
            if (null == record) return null;

            try
            {
                using (var stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, record);
                    return stream.ToArray();
                }
            }
            catch
            {
                // Log error
                throw;
            }
        }

        public static T ProtoDeserialize<T>(byte[] data) where T : class
        {
            if (null == data) return null;

            try
            {
                using (var stream = new MemoryStream(data))
                {
                    return Serializer.Deserialize<T>(stream);
                }
            }
            catch
            {
                // Log error
                throw;
            }
        }
    }
}
