using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisDropdown : XisSimpleWidget
    {
        public XisDropdown(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string name, string label) : base(repository)
        {
            Element = XisWebHelper.CreateXisDropdown(parent.Element, name, label);

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

        public void SetLabel(string label)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "label")
                {
                    tv.Value = label;
                    tv.Update();
                    return;
                }
            }
        }
    }
}
