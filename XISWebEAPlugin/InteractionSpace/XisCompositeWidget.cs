using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XisWebEAPlugin.InteractionSpace
{
    abstract class XisCompositeWidget : XisWidget
    {
        public List<XisWidget> Widgets { get; set; }

        public XisCompositeWidget(EA.Repository repository, XisWidget parent) : base(repository)
        {
            Widgets = new List<XisWidget>();

            // TODO: Review this addition!
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

        public XisCompositeWidget(EA.Repository repository) : base(repository)
        {
            Widgets = new List<XisWidget>();
        }

        public void SetEntityName(string entityName)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "entityName")
                {
                    tv.Value = entityName;
                    tv.Update();
                    break;
                }
            }
        }
    }
}
