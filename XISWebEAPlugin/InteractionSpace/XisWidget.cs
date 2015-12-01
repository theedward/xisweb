using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XisWebEAPlugin.InteractionSpace
{
    class XisWidget
    {
        public EA.Element Element { get; set; }
        public EA.Repository Repository { get; set; }

        public XisWidget(EA.Repository repository)
        {
            Repository = repository;
        }

        public EA.DiagramObject SetPosition(EA.Diagram diagram, int left, int right, int top, int bottom,
            int sequence = 0)
        {
            EA.DiagramObject diagObj = GetDiagramObject(diagram);

            if (diagObj != null)
            {
                diagObj.left = left;
                diagObj.right = right;
                diagObj.top = -top;
                diagObj.bottom = -bottom;
                diagObj.Sequence = sequence;
                diagObj.Update();
                SetPositionTaggedValues(left, right, top, bottom);
            }
            else
            {
                diagObj = diagram.DiagramObjects.AddNew(Element.Name, "Class");
                diagObj.ElementID = Element.ElementID;
                diagObj.Update();

                diagObj.left = left;
                diagObj.right = right;
                diagObj.top = -top;
                diagObj.bottom = -bottom;
                diagObj.Sequence = sequence;
                diagObj.Update();
                SetPositionTaggedValues(left, right, top, bottom);
            }

            if (diagObj.Sequence != sequence)
            {
                string query = "update t_diagramobjects set Sequence = " + sequence + " where Object_ID = "
                    + diagObj.ElementID;
                Repository.Execute(query);
                //diagObj.Sequence = sequence;
                diagObj = GetDiagramObject(diagram);
            }
            return diagObj;
        }

        public EA.DiagramObject GetDiagramObject(EA.Diagram diagram)
        {
            diagram.DiagramObjects.Refresh();
            foreach (EA.DiagramObject obj in diagram.DiagramObjects)
	        {
                if (obj.ElementID == Element.ElementID)
                {
                    return obj;
                }
	        }
            return null;
        }

        public void SetValue(string value)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "value")
                {
                    tv.Value = value;
                    tv.Update();
                    break;
                }
            }
        }

        public void SetValueFromExpression(string valueFromExpression)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "valueFromExpression")
                {
                    tv.Value = valueFromExpression;
                    tv.Update();
                    break;
                }
            }
        }

        private void SetPositionTaggedValues(int left, int right, int top, int bottom)
        {
            int value = 0;
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "posX":
                        value = (left + right) / 2;
                        tv.Value = value.ToString();
                        tv.Update();
                        break;
                    case "posY":
                        value = (top + bottom) / 2;
                        tv.Value = value.ToString();
                        tv.Update();
                        break;
                    case "width":
                        value = right - left;
                        tv.Value = value.ToString();
                        tv.Update();
                        break;
                    case "height":
                        value = bottom - top;
                        tv.Value = value.ToString();
                        tv.Update();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
