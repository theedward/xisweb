using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XisWebEAPlugin.InteractionSpace;

namespace XISWebEAPlugin.InteractionSpace
{
    class XisImage: XisSimpleWidget
    {
        public XisImage(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string src) : base(repository)
        {
            if (parent is XisInteractionSpace)
            {
                XisInteractionSpace it = parent as XisInteractionSpace;
                Element = XisWebHelper.CreateXisImage(it.Element, src);
                it.Widgets.Add(this);
            }
            else if (parent is XisVisibilityBoundary)
            {
                XisVisibilityBoundary boundary = parent as XisVisibilityBoundary;
                Element = XisWebHelper.CreateXisImage(boundary.Element, src);
                boundary.Widgets.Add(this);
            }
            else if (parent is XisCompositeWidget)
            {
                XisCompositeWidget comp = parent as XisCompositeWidget;
                Element = XisWebHelper.CreateXisImage(comp.Element, src);
                comp.Widgets.Add(this);
            }
        }
    }
}
