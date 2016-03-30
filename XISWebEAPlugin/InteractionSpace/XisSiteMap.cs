using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XisWebEAPlugin.InteractionSpace;

namespace XISWebEAPlugin.InteractionSpace
{
    class XisSiteMap : XisSimpleWidget
    {
        public XisSiteMap(EA.Repository repository, XisWidget parent, EA.Diagram diagram) : base(repository)
        {
            if (parent is XisInteractionSpace)
            {
                XisInteractionSpace it = parent as XisInteractionSpace;
                Element = XisWebHelper.CreateXisSiteMap(it.Element);
                it.Widgets.Add(this);
            }
        }
    }
}
