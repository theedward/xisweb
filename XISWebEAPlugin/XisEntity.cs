using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XisWebEAPlugin
{
    class XisEntity
    {
        public EA.Element Element { get; set; }
        public List<XisEntity> Details { get; set; }
        public List<XisEntity> References { get; set; }
        public string Filter { get; set; }
        public string Cardinality { get; set; }
        public string BeCardinality { get; set; }

        public XisEntity(EA.Element element, string filter)
        {
            this.Element = element;
            this.Filter = filter;
        }
    }
}
