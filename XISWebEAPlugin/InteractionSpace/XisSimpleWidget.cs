using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XisWebEAPlugin.InteractionSpace
{
    abstract class XisSimpleWidget : XisWidget
    {
        public XisSimpleWidget(EA.Repository repository) : base(repository)
        {
            Repository = repository;
        }

        public void SetEntityAttributeName(string entityAttributeName)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "entityAttributeName")
                {
                    tv.Value = entityAttributeName;
                    tv.Update();
                    break;
                }
            }
        }
    }
}
