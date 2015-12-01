using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EA;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisInteractionSpace : XisWidget
    {
        public XisMenu Menu { get; set; }
        public XisMenu ContextMenu { get; set; }
        public List<XisWidget> Widgets { get; set; }
        public bool IsMainScreen;
        public bool IsFirstSubScreen;

        public XisInteractionSpace(EA.Repository repository, EA.Package package, EA.Diagram diagram,
            string name, string title, bool isMainScreen = false, bool isFirstSubScreen = false)
            : base(repository)
        {
            Element = XisWebHelper.CreateXisInteractionSpace(package, diagram, name, title, isMainScreen);
            Widgets = new List<XisWidget>();
            IsMainScreen = isMainScreen;
            IsFirstSubScreen = isFirstSubScreen;
            // Set Default IS size, position and z-order
            SetPosition(diagram, 217, 642, 10, 540);
        }
    }
}
