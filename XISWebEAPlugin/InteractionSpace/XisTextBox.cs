using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisTextBox : XisSimpleWidget
    {
        public XisTextBox(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string name, string label = null, string hint = null, int lines = 1) : base(repository)
        {
            Element = XisWebHelper.CreateXisTextBox(parent.Element, name, label, hint, lines);

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
