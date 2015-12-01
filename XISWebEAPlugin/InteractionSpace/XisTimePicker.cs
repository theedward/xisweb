using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisTimePicker : XisSimpleWidget
    {
        public XisTimePicker(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string name, string timezone = null) : base(repository)
        {
            Element = XisWebHelper.CreateXisTimePicker(parent.Element, name, timezone);

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
