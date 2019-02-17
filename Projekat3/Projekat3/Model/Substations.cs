using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projekat2.Models
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("NetworkModel")]
    public class Substations
    {
        [XmlArray("Substations")]
        [XmlArrayItem("SubstationEntity", typeof(SubstationEntity))]
        public SubstationEntity[] SubstationEntities { get; set; }
        [XmlArray("Nodes")]
        [XmlArrayItem("NodeEntity", typeof(NodeEntity))]
        public NodeEntity[] NodesEntities { get; set; }
        [XmlArray("Switches")]
        [XmlArrayItem("SwitchEntity", typeof(SwitchEntity))]
        public SwitchEntity[] SwitchEntities { get; set; }
        [XmlArray("Lines")]
        [XmlArrayItem("LineEntity", typeof(LineEntity))]
        public LineEntity[] LineEntities { get; set; }
    }
}
