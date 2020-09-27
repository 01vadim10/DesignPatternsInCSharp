using System.IO;
using System.Xml.Serialization;

namespace PrototypePattern
{
    public static class ExtensionHelper
    {
        public static T DeepCopy<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(ms, self);
                ms.Position = 0;
                
                return (T) formatter.Deserialize(ms);
            }
        }
    }
}
