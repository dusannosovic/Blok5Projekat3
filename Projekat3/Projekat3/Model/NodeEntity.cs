using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projekat2.Models
{
    [Serializable()]
    public class NodeEntity
    {
        [XmlElement("Id")]
        public string Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("X")]
        public double X { get; set; }
        [XmlElement("Y")]
        public double Y { get; set; }
    }
}
