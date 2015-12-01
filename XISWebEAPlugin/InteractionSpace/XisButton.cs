using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisButton : XisSimpleWidget
    {
        public XisButton(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string name, string onTap = null) : base(repository)
        {
            if (parent is XisInteractionSpace)
            {
                XisInteractionSpace it = parent as XisInteractionSpace;
                Element = XisWebHelper.CreateXisButton(it.Element, name, onTap);
                it.Widgets.Add(this);
            }
            else if (parent is XisVisibilityBoundary)
            {
                XisVisibilityBoundary boundary = parent as XisVisibilityBoundary;
                Element = XisWebHelper.CreateXisButton(boundary.Element, name, onTap);
                boundary.Widgets.Add(this);
            }
            else if (parent is XisCompositeWidget)
            {
                XisCompositeWidget comp = parent as XisCompositeWidget;
                Element = XisWebHelper.CreateXisButton(comp.Element, name, onTap);
                comp.Widgets.Add(this);
            }
        }
    }
}
