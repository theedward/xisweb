using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisMenuItem : XisSimpleWidget
    {
        public XisMenuItem(EA.Repository repository, EA.Diagram diagram, XisWidget parent, string name, string onTap = null)
            : base(repository)
        {
            if (parent is XisMenu)
            {
                Element = XisWebHelper.CreateXisMenuItem(parent.Element, name, onTap);
                XisMenu menu = parent as XisMenu;
                menu.Items.Add(this);
                menu.Widgets.Add(this);
            }
            else if (parent is XisMenuGroup)
            {
                Element = XisWebHelper.CreateXisMenuItem(parent.Element, name, onTap);
                XisMenuGroup group = parent as XisMenuGroup;
                group.Items.Add(this);
                group.Widgets.Add(this);
            }
            else if (parent is XisVisibilityBoundary)
            {
                XisVisibilityBoundary boundary = parent as XisVisibilityBoundary;
                Element = XisWebHelper.CreateXisMenuItem(boundary.Element, name, onTap);
                boundary.Widgets.Add(this);
            }
        }

        public string GetOnTapAction()
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "onTap")
                {
                    return tv.Value;
                }
            }
            return null;
        }

        public void SetOnTapAction(string onTap)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "onTap")
                {
                    tv.Value = onTap;
                    tv.Update();
                    return;
                }
            }
        }
    }
}
