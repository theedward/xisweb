using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisLabel : XisSimpleWidget
    {
        public XisLabel(EA.Repository repository, XisWidget parent, EA.Diagram diagram, string name) : base(repository)
        {
            Element = XisWebHelper.CreateXisLabel(parent.Element, name);

            if (parent is XisInteractionSpace)
            {
                XisInteractionSpace it = parent as XisInteractionSpace;
                it.Widgets.Add(this);
            }
            else if (parent is XisCompositeWidget)
            {
                XisCompositeWidget comp = parent as XisCompositeWidget;
                comp.Widgets.Add(this);
            }
        }
    }
}
