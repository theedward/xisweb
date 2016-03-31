using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XisWebEAPlugin.InteractionSpace;

namespace XISWebEAPlugin.InteractionSpace
{
    class XisCollapsible : XisCompositeWidget
    {
        public XisMenu Menu { get; set; }
        public XisMenu ContextMenu { get; set; }
        public List<XisCollapsibleItem> Items { get; set; }

        public XisCollapsible(EA.Repository repository, EA.Diagram diagram,
            XisInteractionSpace parent, string name)
            : base(repository, parent)
        {
            Element = XisWebHelper.CreateXisCollapsible(parent.Element, name);
            Items = new List<XisCollapsibleItem>();
        }
    }
}
