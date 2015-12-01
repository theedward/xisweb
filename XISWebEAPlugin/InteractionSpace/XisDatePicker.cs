using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisDatePicker : XisSimpleWidget
    {
        public XisDatePicker(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string name, string timezone = null) : base(repository)
        {
            Element = XisWebHelper.CreateXisDatePicker(parent.Element, name, timezone);

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
