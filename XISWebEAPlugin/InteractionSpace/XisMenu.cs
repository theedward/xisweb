using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisMenu : XisCompositeWidget
    {
        public List<XisMenuGroup> Groups { get; set; }
        public List<XisMenuItem> Items { get; set; }

        public XisMenu(EA.Repository repository, EA.Diagram diagram, XisWidget parent, string name, MenuType type)
            : base(repository, parent)
        {
            Element = XisWebHelper.CreateXisMenu(parent.Element, name, type);
            Groups = new List<XisMenuGroup>();
            Items = new List<XisMenuItem>();
        }

        public XisMenu(EA.Repository repository, EA.Diagram diagram, EA.Package package, string name, MenuType type)
            : base(repository)
        {
            if (type == MenuType.ContextMenu)
            {
                Element = XisWebHelper.CreateXisMenu(package, name, type);
                Items = new List<XisMenuItem>();
            }
            else
            {
                throw new Exception("Unsupported constructor for XisMenu of type: " + type.ToString());
            }
        }
    }
}
