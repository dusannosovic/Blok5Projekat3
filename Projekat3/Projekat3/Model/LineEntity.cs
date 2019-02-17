using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projekat2.Models
{
    [Serializable()]
    public class LineEntity
    {
        [XmlElement("Id")]
        public string Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("IsUnderground")]
        public bool IsUnderground { get; set; }
        [XmlElement("R")]
        public double R { get; set; }
        [XmlElement("ConductorMaterial")]
        public string ConductorMaterial { get; set; }
        [XmlElement("LineType")]
        public string LineType { get; set; }
        [XmlElement("ThermalConstantHeat")]
        public double ThermalConstantHeat { get; set; }
        [XmlElement("FirstEnd")]
        public string FirstEnd { get; set; }
        [XmlElement("SecondEnd")]
        public string SecondEnd { get; set; }
        [XmlArray("Vertices")]
        [XmlArrayItem("Point", typeof(Point))]
        public Point[] Vertices { get; set; }
    }
}