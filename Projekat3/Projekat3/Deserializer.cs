using Projekat2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projekat2
{
    static public class Deserializer
    {
        static readonly string path = @"C:\Users\pc\Desktop\Blok5Projekat3\Projekat3\Geographic.Xml";
        static StreamReader reader;
        public static Substations DeserializeSubs()
        {
            StreamReader reader = new StreamReader(path);
            Substations substations;
            XmlSerializer serializer = new XmlSerializer(typeof(Substations));
            substations = (Substations)serializer.Deserialize(reader);
            reader.Close();
            return substations;
        }

    }
}
