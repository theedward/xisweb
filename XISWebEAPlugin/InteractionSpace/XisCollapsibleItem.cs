using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XisWebEAPlugin.InteractionSpace;

namespace XISWebEAPlugin.InteractionSpace
{
    class XisCollapsibleItem : XisCompositeWidget
    {
        public XisCollapsibleItem(EA.Repository repository, EA.Diagram diagram,
            XisCollapsible parent, string name)
            : base(repository, parent)
        {
            Element = XisWebHelper.CreateXisCollapsibleItem(parent.Element, name);
            parent.Items.Add(this);
        }
    }
}
