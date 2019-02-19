using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat3.Model
{
    public class GraphNode
    {
        public bool Visited { get; set; } = false;
        public bool Element { get; set; } = false;
        public List<string> VisitedIds { get; set; }

        public GraphNode()
        {
            VisitedIds = new List<string>();
        }
    }
}
