
using System.Windows.Forms;
using System;
namespace XisWebEAPlugin.InteractionSpace
{
    class XisWebHelper
    {
        public static EA.Diagram CreateDiagram(EA.Package package, string diagramName, string diagramType)
        {
            EA.Diagram diagram = package.Diagrams.AddNew(diagramName, diagramType);
            diagram.ShowDetails = 0;
            diagram.Update();
            package.Update();
            return diagram;
        }

        //public static EA.DiagramObject CreateElementLink(EA.Repository repository, EA.Element element, EA.Diagram diagram)
        //{
        //    EA.DiagramObject obj = diagram.DiagramObjects.AddNew(element.Name, element.Type);
        //    obj.Update();
        //    diagram.Update();
        //    diagram.DiagramObjects.Refresh();
        //    string query = "update t_diagramobjects set Object_ID = " + element.ElementID + " where Diagram_ID = " + diagram.DiagramID + " and Object_ID = 0"; ;
        //    repository.Execute(query);
        //    return obj;
        //}

        public static EA.DiagramObject SetPosition(EA.Repository repository, EA.Diagram diagram, EA.Element element,
            int left, int right, int top, int bottom, int sequence = 0)
        {
            //try
            //{
            diagram.DiagramObjects.Refresh();
            for (short i = 0; i < diagram.DiagramObjects.Count; i++)
            {
                EA.DiagramObject obj = diagram.DiagramObjects.GetAt(i);

                if (obj.ElementID == element.ElementID)
                {
                    obj.left = left;
                    obj.right = right;
                    obj.top = -top;
                    obj.bottom = -bottom;
                    obj.Sequence = sequence;
                    obj.Update();
                    return obj;
                }
            }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.StackTrace);
            //}

            EA.DiagramObject diagObj = diagram.DiagramObjects.AddNew(element.Name, "Class");
            diagObj.ElementID = element.ElementID;
            diagObj.Update();

            diagObj.left = left;
            diagObj.right = right;
            diagObj.top = -top;
            diagObj.bottom = -bottom;
            diagObj.Sequence = sequence;
            diagObj.Update();
            return diagObj;
        }

        public static EA.Element CreateXisInteractionSpace(EA.Package package, EA.Diagram diagram,
            string name, string title, InteractionSpaceType type,bool isMainScreen = false)
        {
            EA.Element element = package.Elements.AddNew(name, "Class");
            element.Stereotype = "XisInteractionSpace";
            element.Update();

            foreach (EA.TaggedValue tv in element.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "isMainScreen":
                        tv.Value = isMainScreen.ToString().ToLower();
                        break;
                    case "name":
                        tv.Value = name;
                        break;
                    case "type":
                        tv.Value = type.ToString();
                        break;
                    case "title":
                        tv.Value = title;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return element;
        }

        public static void SetXisWidgetValues(EA.Element widget, int posX, int posY,
            int width, int height, string value)
        {
            foreach (EA.TaggedValue tv in widget.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "posX":
                        tv.Value = posX.ToString();
                        break;
                    case "posY":
                        tv.Value = posY.ToString();
                        break;
                    case "width":
                        tv.Value = width.ToString();
                        break;
                    case "height":
                        tv.Value = height.ToString();
                        break;
                    case "value":
                        tv.Value = value;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
        }

        public static EA.Element CreateXisCompositeWidget(EA.Package package, string name, string onTap = null,
            string onLongTap = null, string searchBy = null, string orderBy = null)
        {
            EA.Element composite = package.Elements.AddNew(name, "Class");
            composite.Stereotype = "XisCompositeWidget";
            composite.Update();

            foreach (EA.TaggedValue tv in composite.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    case "onLongTap":
                        tv.Value = onLongTap;
                        break;
                    case "searchBy":
                        tv.Value = searchBy;
                        break;
                    case "orderBy":
                        tv.Value = orderBy;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return composite;
        }

        public static EA.Element CreateXisCompositeWidget(EA.Element parent, string name, string onTap = null,
            string onLongTap = null, string searchBy = null, string orderBy = null)
        {
            EA.Element composite = parent.Elements.AddNew(name, "Class");
            composite.Stereotype = "XisCompositeWidget";
            composite.Update();

            foreach (EA.TaggedValue tv in composite.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    case "onLongTap":
                        tv.Value = onLongTap;
                        break;
                    case "searchBy":
                        tv.Value = searchBy;
                        break;
                    case "orderBy":
                        tv.Value = orderBy;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return composite;
        }

        public static EA.Element CreateXisMenu(EA.Element parent, string name, MenuType type)
        {
            EA.Element menu = parent.Elements.AddNew(name, "Class");
            menu.Stereotype = "XisMenu";
            menu.Update();

            foreach (EA.TaggedValue tv in menu.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "type":
                        tv.Value = type.ToString();
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return menu;
        }

        public static EA.Element CreateXisMenu(EA.Package package, string name, MenuType type)
        {
            EA.Element menu = package.Elements.AddNew(name, "Class");
            menu.Stereotype = "XisMenu";
            menu.Update();

            foreach (EA.TaggedValue tv in menu.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "type":
                        tv.Value = type.ToString();
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return menu;
        }

        public static EA.Element CreateXisMenuGroup(EA.Element parent, string name)
        {
            EA.Element group = parent.Elements.AddNew(name, "Class");
            group.Stereotype = "XisMenuGroup";
            group.Update();

            foreach (EA.TaggedValue tv in group.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return group;
        }

        public static EA.Element CreateXisMenuItem(EA.Element parent, string name, string onTap)
        {
            EA.Element item = parent.Elements.AddNew(name, "Class");
            item.Stereotype = "XisMenuItem";
            item.Update();

            foreach (EA.TaggedValue tv in item.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return item;
        }

        public static EA.Element CreateXisList(EA.Element parent, string name, string searchBy = null, string orderBy = null)
        {
            EA.Element list = parent.Elements.AddNew(name, "Class");
            list.Stereotype = "XisList";
            list.Update();

            foreach (EA.TaggedValue tv in list.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "searchBy":
                        tv.Value = searchBy;
                        break;
                    case "orderBy":
                        tv.Value = orderBy;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return list;
        }

        public static EA.Element CreateXisListItem(EA.Element parent, string name, string onTap = null, string onLongTap = null)
        {
            EA.Element item = parent.Elements.AddNew(name, "Class");
            item.Stereotype = "XisListItem";
            item.Update();

            foreach (EA.TaggedValue tv in item.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    case "onLongTap":
                        tv.Value = onLongTap;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return item;
        }

        public static EA.Element CreateXisCollapsible(EA.Element parent, string name)
        {
            EA.Element list = parent.Elements.AddNew(name, "Class");
            list.Stereotype = "XisCollapsible";
            list.Update();

            foreach (EA.TaggedValue tv in list.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return list;
        }

        public static EA.Element CreateXisCollapsibleItem(EA.Element parent, string name)
        {
            EA.Element item = parent.Elements.AddNew(name, "Class");
            item.Stereotype = "XisCollapsibleItem";
            item.Update();

            foreach (EA.TaggedValue tv in item.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return item;
        }

        public static EA.Element CreateXisForm(EA.Element parent, string name, string entityName)
        {
            EA.Element form = parent.Elements.AddNew(name, "Class");
            form.Stereotype = "XisForm";
            form.Update();

            foreach (EA.TaggedValue tv in form.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "entityName":
                        tv.Value = entityName;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return form;
        }

        public static XisSimpleWidget ProcessXisAttribute(EA.Repository repository, EA.Diagram diagram,
            XisWidget parent, EA.Attribute attribute, string entityName)
        {
            XisSimpleWidget widget = null;

            switch (attribute.Type.ToLower())
            {
                case "int":
                case "double":
                case "string":
                    widget = new XisTextBox(repository, parent, diagram, entityName + ToUpperFirst(attribute.Name) + "TxtBox", attribute.Name, attribute.Name);
                    widget.SetEntityAttributeName(entityName + "." + attribute.Name);
                    break;
                case "bool":
                case "boolean":
                    // Dropdown
                    widget = new XisDropdown(repository, parent, diagram, entityName + ToUpperFirst(attribute.Name) + "Dropdown", attribute.Name);
                    widget.SetEntityAttributeName(entityName + "." + attribute.Name);
                    break;
                case "date":
                    widget = new XisDatePicker(repository, parent, diagram, entityName + ToUpperFirst(attribute.Name) + "DatePicker");
                    widget.SetEntityAttributeName(entityName + "." + attribute.Name);
                    break;
                case "time":
                    widget = new XisTimePicker(repository, parent, diagram, entityName + ToUpperFirst(attribute.Name) + "TimePicker");
                    widget.SetEntityAttributeName(entityName + "." + attribute.Name);
                    break;
                default:
                    break;
            }

            return widget;
        }

        public static EA.Element CreateXisLabel(EA.Element parent, string name)
        {
            EA.Element label = parent.Elements.AddNew(name, "Class");
            label.Stereotype = "XisLabel";
            label.Update();

            foreach (EA.TaggedValue tv in label.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return label;
        }

        public static EA.Element CreateXisTextBox(EA.Element parent, string name, string label, string hint, int lines = 1)
        {
            EA.Element textBox = parent.Elements.AddNew(name, "Class");
            textBox.Stereotype = "XisTextBox";
            textBox.Update();

            foreach (EA.TaggedValue tv in textBox.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "label":
                        tv.Value = label;
                        break;
                    case "hint":
                        tv.Value = hint;
                        break;
                    case "lines":
                        tv.Value = lines.ToString();
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return textBox;
        }

        public static EA.Element CreateXisButton(EA.Element parent, string name, string onTap = null)
        {
            EA.Element button = parent.Elements.AddNew(name, "Class");
            button.Stereotype = "XisButton";
            button.Update();

            foreach (EA.TaggedValue tv in button.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return button;
        }

        public static EA.Element CreateXisImage(EA.Element parent, string src)
        {
            EA.Element image = parent.Elements.AddNew(parent.Name+"Image", "Class");
            image.Stereotype = "XisImage";
            image.Update();

            foreach (EA.TaggedValue tv in image.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "src":
                        tv.Value = src;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return image;
        }

        public static EA.Element CreateXisSiteMap(EA.Element parent)
        {
            EA.Element siteMap = parent.Elements.AddNew(parent.Name+"SiteMap", "Class");
            siteMap.Stereotype = "XisSiteMap";
            siteMap.Update();

            //foreach (EA.TaggedValue tv in siteMap.TaggedValues)
            //{
            //    switch (tv.Name)
            //    {
            //        case "src":
            //            tv.Value = src;
            //            break;
            //        default:
            //            break;
            //    }
            //    tv.Update();
            //}
            return siteMap;
        }

        public static EA.Element CreateXisDropdown(EA.Element parent, string name, string label)
        {
            EA.Element dropdown = parent.Elements.AddNew(name, "Class");
            dropdown.Stereotype = "XisDropdown";
            dropdown.Update();

            foreach (EA.TaggedValue tv in dropdown.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "label":
                        tv.Value = label;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return dropdown;
        }

        public static EA.Element CreateXisDatePicker(EA.Element parent, string name, string timezone = null)
        {
            EA.Element datePicker = parent.Elements.AddNew(name, "Class");
            datePicker.Stereotype = "XisDatePicker";
            datePicker.Update();

            foreach (EA.TaggedValue tv in datePicker.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "timezone":
                        tv.Value = timezone;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return datePicker;
        }

        public static EA.Element CreateXisTimePicker(EA.Element parent, string name, string timezone = null)
        {
            EA.Element timePicker = parent.Elements.AddNew(name, "Class");
            timePicker.Stereotype = "XisTimePicker";
            timePicker.Update();

            foreach (EA.TaggedValue tv in timePicker.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "timezone":
                        tv.Value = timezone;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return timePicker;
        }

        public static EA.Element CreateXisVisibilityBoundary(EA.Element parent, string name,
            bool create = false, bool view = false, bool edit = false)
        {
            EA.Element boundary = parent.Elements.AddNew(name, "Class");
            boundary.Stereotype = "XisVisibilityBoundary";
            boundary.Update();

            foreach (EA.TaggedValue tv in boundary.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "Create":
                        tv.Value = create.ToString().ToLower();
                        break;
                    case "View":
                        tv.Value = view.ToString().ToLower();
                        break;
                    case "Edit":
                        tv.Value = edit.ToString().ToLower();
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            return boundary;
        }

        public static EA.Method CreateXisAction(EA.Repository repository, EA.Element parent, string name, ActionType type, string navigation = null)
        {
            EA.Method action = null;

            switch (type)
	        {
                case ActionType.OK:
                case ActionType.Cancel:
                case ActionType.Create:
                case ActionType.Read:
                case ActionType.Update:
                case ActionType.Delete:
                case ActionType.DeleteAll:
                case ActionType.WebService:
                case ActionType.Navigate:
                case ActionType.Custom:
                    action = parent.Methods.AddNew(name, "");
                    break;
	        }

            action.Stereotype = "XIS-Web::XisAction";
            action.StereotypeEx = "XIS-Web::XisAction";
            action.Update();
            action.TaggedValues.Refresh();

            if (action.TaggedValues.Count == 0)
            {
                EA.MethodTag typeTv = action.TaggedValues.AddNew("type", "XIS-Web::ActionType");
                typeTv.Value = type.ToString();
                typeTv.Update();
                EA.MethodTag navigationTv = action.TaggedValues.AddNew("navigation", "String");
                navigationTv.Value = navigation;
                navigationTv.Update(); 
            }
            parent.Methods.Refresh();

            return action;
        }

        private static string ToUpperFirst(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length > 1)
                {
                    s = s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1);
                }
                else
                {
                    s = s.ToUpper();
                }
            }
            return s;
        }
    }

    enum MenuType
    {
        OptionsMenu,
        ContextMenu
    }

    enum InteractionSpaceType
    {
        HomeInteractionSpace,
        MasterEntityList,
        MasterEntityEditor,
        DetailEntityEditor,
        ReferenceEntityEditor,
        ServiceInteractionSpace,
        CustomInteractionSpace
    }

    enum GestureType
    {
        Tap,
        DoubleTap,
        LongTap,
        Swipe,
        Pinch,
        Stretch
    }

    enum ActionType
    {
        OK,
        Cancel,
        Create,
        Read,
        Update,
        Delete,
        DeleteAll,
        WebService,
        Navigate,
        Custom
    }

    enum PrimitiveType
    {
        Integer,
        Double,
        String,
        Boolean,
        Date,
        Time,
        Image,
        URL
    }
}
