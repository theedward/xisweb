using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisListItem : XisCompositeWidget
    {
        public XisListItem(EA.Repository repository, EA.Diagram diagram,
            XisList parent, string name, string onTap = null, string onLongTap = null)
            : base(repository, parent)
        {
            Element = XisWebHelper.CreateXisListItem(parent.Element, name, onTap, onLongTap);
            parent.Items.Add(this);
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

        public void SetOnLongTapAction(string onLongTap)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "onLongTap")
                {
                    tv.Value = onLongTap;
                    tv.Update();
                    return;
                }
            }
        }
    }
}
