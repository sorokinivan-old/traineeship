using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ProtoBuf;
using System.IO;

namespace JSONImporter
{
    [ProtoContract(SkipConstructor = true)]
    public class Coach
    {
        [Key]
        [ProtoMember(1)]
        [ForeignKey("coach")]
        public int personId { get; set; }
        [ProtoMember(2)]
        public string familyName { get; set; }
        [ProtoMember(3)]
        public string firstName { get; set; }
        [ProtoMember(4)]
        public string internationalFamilyName { get; set; }
        [ProtoMember(5)]
        public string internationalFirstName { get; set; }
        [ProtoMember(6)]
        public string scoreBoardName { get; set; }
        [ProtoMember(7)]
        public string TVName { get; set; }
        [ProtoMember(8)]
        public string nickName { get; set; }
        [ProtoMember(9)]
        public string externalId { get; set; }
        [ProtoMember(10)]
        public string nationalityCode { get; set; }
        [ProtoMember(11)]
        public string nationalityCodeIOC { get; set; }
        [ProtoMember(12)]
        public string nationality { get; set; }


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
