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
    public class Player
    {
        [Key]
        [ProtoMember(1)]
        public int pno { get; set; }
        [ProtoMember(2)]
        public int personId { get; set; }
        [ProtoMember(3)]
        public string familyName { get; set; }
        [ProtoMember(4)]
        public string firstName { get; set; }
        [ProtoMember(5)]
        public string internationalFamilyName { get; set; }
        [ProtoMember(6)]
        public string internationalFirstName { get; set; }
        [ProtoMember(7)]
        public string scoreboardName { get; set; }
        [ProtoMember(8)]
        public string TVName { get; set; }
        [ProtoMember(9)]
        public string nickName { get; set; }
        [ProtoMember(10)]
        public string website { get; set; }
        [ProtoMember(11)]
        public int height { get; set; }
        [ProtoMember(12)]
        public string externalId { get; set; }
        [ProtoMember(13)]
        public string internationalReference { get; set; }
        [ProtoMember(14)]
        public string shirtNumber { get; set; }
        [ProtoMember(15)]
        public string playingPosition { get; set; }
        [ProtoMember(16)]
        public bool starter { get; set; }
        [ProtoMember(17)]
        public bool captain { get; set; }
        [ProtoMember(18)]
        public bool active { get; set; }
        [ProtoMember(19)]
        public string nationalityCode { get; set; }
        [ProtoMember(20)]
        public string nationalityCodeIOC { get; set; }
        [ProtoMember(21)]
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
