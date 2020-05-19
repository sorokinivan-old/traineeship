using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace JSONImporter
{
    [ProtoContract(SkipConstructor = true)]
    public class Teams
    {
        [ProtoMember(1)]
        public int teamNumber { get; set; }
        [ProtoMember(2)]
        public Detail detail { get; set; }
        [ProtoMember(3)]
        public List<Player> players { get; set; }
        [ProtoMember(4)]
        public Coach coach { get; set; }

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
