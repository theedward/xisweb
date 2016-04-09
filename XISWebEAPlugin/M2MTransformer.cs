using System;
using System.Collections.Generic;
using System.Linq;
using XisWebEAPlugin.InteractionSpace;
using System.Windows.Forms;
using XISWebEAPlugin.InteractionSpace;

namespace XisWebEAPlugin
{
    static class M2MTransformer
    {
        private static XisInteractionSpace homeIS;
        private static EA.Diagram homeDiagram;
        private static EA.Diagram nsDiagram;
        private static EA.Repository repository;

        public static void ProcessUseCase(EA.Repository rep, EA.Package navigationPackage, EA.Package interactionPackage,
            List<EA.Element> useCases, string patternType = null)
        {
            repository = rep;
            string[] res = repository.ConnectionString.Split('\\');
            string projectName = res[res.Length - 1].Split('.')[0];
            nsDiagram = XisWebHelper.CreateDiagram(navigationPackage, projectName + "NavigationSpace Diagram",
                "XIS-Web_Diagrams::NavigationSpaceViewModel");
            bool isStartingUC = true;

            if (patternType != null)
            {
                homeDiagram = XisWebHelper.CreateDiagram(interactionPackage, "HomeIS Diagram",
                    "XIS-Web_Diagrams::InteractionSpaceViewModel");
                homeIS = new XisInteractionSpace(repository, interactionPackage, homeDiagram, "Home","HomeIS", InteractionSpaceType.HomeInteractionSpace, true);
            }

            foreach (EA.Element useCase in useCases)
            {
                foreach (EA.Connector connector in useCase.Connectors)
                {
                    if (connector.Stereotype == "XisEntityUC-BEAssociation")
                    {
                        EA.Element be = repository.GetElementByID(connector.SupplierID);
                        XisEntity master = null;
                        List<XisEntity> details = new List<XisEntity>();
                        List<XisEntity> references = new List<XisEntity>();

                        #region [Get Entities (Master, Details and References)]
                        foreach (EA.Connector beConn in be.Connectors)
                        {
                            switch (beConn.Stereotype)
                            {
                                case "XisBE-EntityMasterAssociation":
                                    master = new XisEntity(repository.GetElementByID(beConn.SupplierID),
                                        GetConnectorTag(beConn.TaggedValues, "filter").Value);
                                    break;
                                case "XisBE-EntityDetailAssociation":
                                    details.Add(new XisEntity(repository.GetElementByID(beConn.SupplierID),
                                        GetConnectorTag(beConn.TaggedValues, "filter").Value));
                                    break;
                                case "XisBE-EntityReferenceAssociation":
                                    references.Add(new XisEntity(repository.GetElementByID(beConn.SupplierID),
                                        GetConnectorTag(beConn.TaggedValues, "filter").Value));
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        if (master != null)
                        {
                            //MessageBox.Show(master.Element.Name);

                            #region [Add cardinality to Entities]
                            if (details.Count > 0 || references.Count > 0)
                            {
                                foreach (EA.Connector conn in master.Element.Connectors)
                                {
                                    foreach (XisEntity detail in details)
                                    {
                                        if (conn.ClientID == detail.Element.ElementID)
                                        {
                                            detail.Cardinality = string.IsNullOrEmpty(conn.ClientEnd.Cardinality) ? "1" : conn.ClientEnd.Cardinality;
                                            detail.BeCardinality = string.IsNullOrEmpty(conn.SupplierEnd.Cardinality) ? "1" : conn.SupplierEnd.Cardinality;
                                            //MessageBox.Show("client: " + detail.Element.Name);
                                        }
                                        else if (conn.SupplierID == detail.Element.ElementID)
                                        {
                                            detail.Cardinality = string.IsNullOrEmpty(conn.SupplierEnd.Cardinality) ? "1" : conn.SupplierEnd.Cardinality;
                                            detail.BeCardinality = string.IsNullOrEmpty(conn.ClientEnd.Cardinality) ? "1" : conn.ClientEnd.Cardinality;
                                            //MessageBox.Show("supplier: " + detail.Element.Name);
                                        }
                                    }

                                    foreach (XisEntity reference in references)
                                    {
                                        if (conn.ClientID == reference.Element.ElementID)
                                        {
                                            reference.Cardinality = string.IsNullOrEmpty(conn.ClientEnd.Cardinality) ? "1" : conn.ClientEnd.Cardinality;
                                            reference.BeCardinality = string.IsNullOrEmpty(conn.SupplierEnd.Cardinality) ? "1" : conn.SupplierEnd.Cardinality;
                                            //MessageBox.Show("client: " + reference.Element.Name);
                                        }
                                        else if (conn.SupplierID == reference.Element.ElementID)
                                        {
                                            reference.Cardinality = string.IsNullOrEmpty(conn.SupplierEnd.Cardinality) ? "1" : conn.SupplierEnd.Cardinality;
                                            reference.BeCardinality = string.IsNullOrEmpty(conn.ClientEnd.Cardinality) ? "1" : conn.ClientEnd.Cardinality;
                                            //MessageBox.Show("supplier: " + reference.Element.Name);
                                        }
                                    }
                                }
                            }
                            #endregion

                            master.Details = details;
                            master.References = references;

                            EA.TaggedValue ucType = GetTaggedValue(useCase.TaggedValues, "type");

                            if (ucType != null)
                            {
                                if (ucType.Value == "EntityManagement")
                                {
                                    if (isStartingUC && useCases.Count > 1)
                                    {
                                        ProcessManagerUseCase(interactionPackage, master, useCase, be, isStartingUC,
                                            useCases.GetRange(1, useCases.Count - 1), patternType);
                                    }
                                    else
                                    {
                                        ProcessManagerUseCase(interactionPackage, master, useCase, be, isStartingUC, null, patternType);
                                    }
                                }
                                else if (ucType.Value == "EntityConfiguration")
                                {
                                    if (isStartingUC && useCases.Count > 1)
                                    {
                                        ProcessDetailUseCase(interactionPackage, master, useCase, be, isStartingUC,
                                            useCases.GetRange(1, useCases.Count - 1), patternType);
                                    }
                                    else
                                    {
                                        ProcessDetailUseCase(interactionPackage, master, useCase, be, isStartingUC, null, patternType);
                                    }
                                }
                            }
                        }
                    }
                    else if (connector.Stereotype == "XisServiceUC-BEAssociation")
                    {
                        EA.Element be = repository.GetElementByID(connector.SupplierID);
                        XisEntity master = null;
                        List<XisEntity> details = new List<XisEntity>();
                        List<XisEntity> references = new List<XisEntity>();

                        foreach (EA.Connector beConn in be.Connectors)
                        {
                            switch (beConn.Stereotype)
                            {
                                case "XisBE-EntityMasterAssociation":
                                    master = new XisEntity(repository.GetElementByID(beConn.SupplierID),
                                        GetConnectorTag(beConn.TaggedValues, "filter").Value);
                                    break;
                                default:
                                    break;
                            }
                        }

                        if (master != null)
                        {   
                            ProcessServiceUseCase(interactionPackage, master, useCase, be, isStartingUC, null, patternType);
                        }
                    }
                }
                isStartingUC = false;
            }

            if (patternType != null)
            {
                if (patternType == "Springboard")
                {
                    ComputeSprinboardPositions();
                }
                else
                {
                    ComputePositions(homeIS, homeDiagram);
                }
            }
        }

        private static void ComputeSprinboardPositions()
        {
            EA.DiagramObject spaceObj = homeIS.GetDiagramObject(homeDiagram);
            EA.DiagramObject obj = null;
            XisWidget w = homeIS.Widgets.First();
            int width = spaceObj.right - spaceObj.left - 20;

            if (homeIS.Widgets.Count > 9)
            {
                // 4 buttons in the horizontal
                width -= 30;
                int buttonW = width / 4;

                obj = w.SetPosition(homeDiagram,
                    spaceObj.left + 10, spaceObj.left + 10 + buttonW, -spaceObj.top + 40,
                    -spaceObj.top + 90 + 30 * w.Element.Methods.Count, spaceObj.Sequence - 1);
                int height = -obj.bottom + obj.top;

                for (int i = 1; i < homeIS.Widgets.Count; i++)
                {
                    w = homeIS.Widgets[i];
                    int aux = obj.right + 10 + buttonW;
                    if (aux > spaceObj.right - 10)
                    {
                        // restart on bottom
                        obj = w.SetPosition(homeDiagram, spaceObj.left + 10, spaceObj.left + 10 + buttonW,
                            -obj.bottom + 10, -obj.bottom + 10 + height, obj.Sequence);
                    }
                    else
                    {
                        obj = w.SetPosition(homeDiagram, obj.right + 10, obj.right + 10 + buttonW, -obj.top, -obj.bottom, obj.Sequence);
                    }
                }
            }
            else
            {
                // 3 buttons in the horizontal
                int buttonW = 0;

                if (homeIS.Widgets.Count < 3)
                {
                    width -= 10 * (homeIS.Widgets.Count - 1);
                    buttonW = width / homeIS.Widgets.Count;
                    obj = w.SetPosition(homeDiagram, spaceObj.left + 10, spaceObj.left + 10 + buttonW, -spaceObj.top + 40,
                        -spaceObj.top + 90 + 30 * w.Element.Methods.Count, spaceObj.Sequence - 1);

                    for (int i = 1; i < homeIS.Widgets.Count; i++)
                    {
                        w = homeIS.Widgets[i];
                        obj = w.SetPosition(homeDiagram, obj.right + 10, obj.right + 10 + buttonW, -obj.top, -obj.bottom, obj.Sequence);
                    }
                }
                else
                {
                    width -= 20;
                    buttonW = width / 3;
                    obj = w.SetPosition(homeDiagram, spaceObj.left + 10, spaceObj.left + 10 + buttonW, -spaceObj.top + 40,
                        -spaceObj.top + 90 + 30 * w.Element.Methods.Count, spaceObj.Sequence - 1);
                    int height = -obj.bottom + obj.top;

                    for (int i = 1; i < homeIS.Widgets.Count; i++)
                    {
                        w = homeIS.Widgets[i];
                        int aux = obj.right + 10 + buttonW;
                        if (aux > spaceObj.right)
                        {
                            // restart on bottom
                            obj = w.SetPosition(homeDiagram, spaceObj.left + 10, spaceObj.left + 10 + buttonW,
                                -obj.bottom + 10, -obj.bottom + 10 + height, obj.Sequence);
                        }
                        else
                        {
                            obj = w.SetPosition(homeDiagram, obj.right + 10, obj.right + 10 + buttonW, -obj.top, -obj.bottom, obj.Sequence);
                        }
                    }
                }
            }
            homeIS.SetPosition(homeDiagram, spaceObj.left, spaceObj.right, -spaceObj.top, -obj.bottom + 10, spaceObj.Sequence);
        }

        private static void AddToHomeISByPattern(EA.Element useCase, XisInteractionSpace targetIS, string patternType)
        {
            String actionName = "goTo" + targetIS.Element.Name;

            switch (patternType)
            {
                case "Springboard":
                    XisButton b = new XisButton(repository, homeIS, homeDiagram, useCase.Name, actionName);
                    b.SetValue(useCase.Name);
                    XisWebHelper.CreateXisAction(repository, b.Element, actionName, ActionType.Navigate, targetIS.Element.Name);
                    CreateXisInteractionSpaceAssociation(actionName, homeIS, targetIS);
                    break;
                case "List Menu":
                    XisList list = null;
                    if (homeIS.Widgets.Count > 0)
                    {
                        list = homeIS.Widgets.First() as XisList;
                    }
                    else
                    {
                        list = new XisList(repository, homeDiagram, homeIS, homeIS.Element.Name + "List");
                    }
                    XisListItem item = new XisListItem(repository, homeDiagram, list, useCase.Name, actionName);
                    XisWebHelper.CreateXisAction(repository, item.Element, actionName, ActionType.Navigate, targetIS.Element.Name);
                    CreateXisInteractionSpaceAssociation(actionName, homeIS, targetIS);
                    break;
                //case "Tab Menu":
                //    // TODO: Implement Tab
                //    break;
                default:
                    break;
            }
        }

        public static void ProcessManagerUseCase(EA.Package package, XisEntity master,
            EA.Element useCase, EA.Element be, bool isStartingUC, List<EA.Element> useCases = null, String patternType = null)
        {
            // Create IS Diagram
            EA.Diagram listDiagram = XisWebHelper.CreateDiagram(package, master.Element.Name + "ListIS Diagram",
                "XIS-Web_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace listIS = null;

            if (isStartingUC && patternType != null)
            {
                listIS = new XisInteractionSpace(repository, package, listDiagram,
                    master.Element.Name + "ListIS", "Manage " + master.Element.Name + "s", InteractionSpaceType.MasterEntityList);
            }
            else
            {
                listIS = new XisInteractionSpace(repository, package, listDiagram,
                    master.Element.Name + "ListIS", "Manage " + master.Element.Name + "s", InteractionSpaceType.MasterEntityList, isStartingUC, !isStartingUC);

                if (isStartingUC && patternType == null)
                {
                    homeIS = listIS;
                }
            }

            //Image & Sitemap Creation - Define a proper source path!
            XisImage img = new XisImage(repository, listIS, listDiagram, "../../images/default.png");

            XisSiteMap siteMap = new XisSiteMap(repository, listIS, listDiagram);

            // List Creation
            XisList list = new XisList(repository, listDiagram, listIS, master.Element.Name + "List");
            list.SetEntityName(master.Element.Name);

            //if (ContainsSearch(operations))
            //{
            //    string searchBy = "";
            //    string tv = null;

            //    foreach (EA.Attribute attr in master.Element.Attributes)
            //    {
            //        tv = GetAttributeTag(attr.TaggedValues, "isKey").Value;
            //        if (!string.IsNullOrEmpty(tv) && tv.ToLower() == "true")
            //        {
            //            searchBy += master.Element.Name + "." + attr.Name + ";";
            //        }
            //    }

            //    if (string.IsNullOrEmpty(searchBy))
            //    {
            //        list.SetSearchBy(master.Element.Name + ".Id");
            //    }
            //    else
            //    {
            //        list.SetSearchBy(searchBy.Substring(0, searchBy.Length - 1));
            //    }
            //}

            XisListItem item = new XisListItem(repository, listDiagram, list, list.Element.Name + "Item");
            
            if (ContainsUpdateMaster(useCase))
            {
                string actionName = "edit" + master.Element.Name;
                item.SetOnTapAction(actionName);
            }
            else if (ContainsReadMaster(useCase))
            {
                string actionName = "view" + master.Element.Name;
                item.SetOnTapAction(actionName);
            }

            if (master.Element.Attributes.Count > 1)
            {
                EA.Attribute first = master.Element.Attributes.GetAt(0);
                EA.Attribute second = master.Element.Attributes.GetAt(1);
                XisLabel lbl1 = new XisLabel(repository, item, listDiagram, first.Name + "Lbl");
                lbl1.SetEntityAttributeName(master.Element.Name + "." + first.Name);
                XisLabel lbl2 = new XisLabel(repository, item, listDiagram, second.Name + "Lbl");
                lbl2.SetEntityAttributeName(master.Element.Name + "." + second.Name);
            }
            else if (master.Element.Attributes.Count == 1)
            {
                EA.Attribute attr = master.Element.Attributes.GetAt(0);
                item.SetValueFromExpression(master.Element.Name + "." + attr.Name);
            }

            // Read, Update, Create
            //Menu Creation
            #region Create Options Menu
            Dictionary<ActionType, XisMenuItem> detailModes = new Dictionary<ActionType, XisMenuItem>(3);

            if (ContainsCreateMaster(useCase) || ContainsDeleteMaster(useCase))
            {
                XisMenu menu = new XisMenu(repository, listDiagram, listIS, listIS.Element.Name + "Menu", MenuType.OptionsMenu);

                if (ContainsCreateMaster(useCase))
                {
                    menu.SetEntityName(master.Element.Name);
                    string actionName = "create" + master.Element.Name;
                    XisMenuItem menuItem = new XisMenuItem(repository, listDiagram, menu,
                        "Create" + master.Element.Name + "Item", actionName);
                    menuItem.SetValue("Create " + master.Element.Name);
                    detailModes.Add(ActionType.Create, menuItem);
                }

                if (ContainsDeleteMaster(useCase))
                {
                    menu.SetEntityName(master.Element.Name);
                    string actionName = "deleteAll" + master.Element.Name + "s";
                    XisMenuItem menuItem = new XisMenuItem(repository, listDiagram, menu,
                        "DeleteAll" + master.Element.Name + "Item", actionName);
                    menuItem.SetValue("Delete all " + master.Element.Name + "s");
                    XisWebHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.DeleteAll);
                }
                listIS.Menu = menu;
            }
            #endregion
            #region Create Context Menu
            if (ContainsReadMaster(useCase) || ContainsUpdateMaster(useCase) || ContainsDeleteMaster(useCase))
            {
                XisMenu context = new XisMenu(repository, listDiagram, package, list.Element.Name + "ContextMenu", MenuType.ContextMenu);

                if (ContainsReadMaster(useCase))
                {
                    context.SetEntityName(master.Element.Name);
                    string actionName = "view" + master.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, listDiagram, context,
                        "View" + master.Element.Name + "Item", actionName);
                    contextItem.SetValue("View " + master.Element.Name);
                    detailModes.Add(ActionType.Read, contextItem);
                }

                if (ContainsUpdateMaster(useCase))
                {
                    context.SetEntityName(master.Element.Name);
                    string actionName = "edit" + master.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, listDiagram, context,
                        "Edit" + master.Element.Name + "Item", actionName);
                    contextItem.SetValue("Edit " + master.Element.Name);
                    detailModes.Add(ActionType.Update, contextItem);
                }

                //This action is passing to the Editor page
                /*if (ContainsDeleteMaster(useCase))
                {
                    context.SetEntityName(master.Element.Name);
                    string actionName = "delete" + master.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, listDiagram, context,
                        "Delete" + master.Element.Name + "Item", actionName);
                    contextItem.SetValue("Delete " + master.Element.Name);
                    XisWebHelper.CreateXisAction(repository, contextItem.Element, actionName, ActionType.Delete);
                }*/
                listIS.ContextMenu = context;
            }
            #endregion

            // Navigation between home UC and the others
            if (patternType != null)
            {
                AddToHomeISByPattern(useCase, listIS, patternType);
            }
            else if (isStartingUC)
            {
                if (useCases != null)
                {
                    AssociateFirstSubSpaces(listDiagram, useCases, listIS, be.ElementID, master.Element.Name);
                }
            }

            #region Check ServiceUC Extensions
            Dictionary<List<EA.Element>, bool> services = new Dictionary<List<EA.Element>, bool>();

            if (useCase.Connectors.Count > 0)
            {
                List<EA.Element> extends = new List<EA.Element>();
                Dictionary<EA.Element, bool> providers = new Dictionary<EA.Element, bool>();

                foreach (EA.Connector conn in useCase.Connectors)
                {
                    if (conn.Stereotype == "extend")
                    {
                        EA.Element el = repository.GetElementByID(conn.ClientID);
                        extends.Add(el);
                    }
                }

                foreach (EA.Element e in extends)
                {
                    bool hasBE = false;
                    EA.Element provider = null;

                    foreach (EA.Connector conn in e.Connectors)
                    {
                        if (conn.Stereotype == "XisServiceUC-BEAssociation")
                        {
                            hasBE = true;
                        }
                        else if (conn.Stereotype == "XisServiceUC-ProviderAssociation")
                        {
                            provider = repository.GetElementByID(conn.SupplierID);
                            providers.Add(provider, false);
                        }
                    }

                    if (provider != null && hasBE)
                    {
                        providers[provider] = true;
                    }
                }

                foreach (EA.Element p in providers.Keys)
                {
                    List<EA.Element> lst = new List<EA.Element>();

                    foreach (EA.Connector c in p.Connectors)
                    {
                        if (c.Stereotype == "XisProvider-ServiceRealization")
                        {
                            EA.Element el = repository.GetElementByID(c.SupplierID);
                            lst.Add(el);
                        }
                    }

                    if (lst.Count > 0)
                    {
                        services.Add(lst, providers[p]);
                    }
                }

                if (services.Count > 0)
                {
                    if (services.Values.Contains(true))
                    {
                        if (listIS.ContextMenu == null)
                        {
                            listIS.ContextMenu = new XisMenu(repository, listDiagram, package, list.Element.Name + "ContextMenu",
                                MenuType.ContextMenu);
                        }

                        foreach (List<EA.Element> lst in services.Keys)
                        {
                            if (services[lst])
                            {
                                foreach (EA.Element serv in lst)
                                {
                                    foreach (EA.Method method in serv.Methods)
                                    {
                                        if (method.Stereotype == "XisServiceMethod")
                                        {
                                            XisMenuItem menuItem = new XisMenuItem(repository, listDiagram, listIS.ContextMenu,
                                                method.Name, serv.Name + "." + method.Name);
                                            menuItem.SetValue(method.Name);
                                            XisWebHelper.CreateXisAction(repository, menuItem.Element, menuItem.GetOnTapAction(),
                                                ActionType.WebService);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (services.Values.Contains(false))
                    {
                        if (listIS.Menu == null)
                        {
                            listIS.Menu = new XisMenu(repository, listDiagram, listIS, listIS.Element.Name + "Menu", MenuType.OptionsMenu);
                        }

                        foreach (List<EA.Element> lst in services.Keys)
                        {
                            if (!services[lst])
                            {
                                foreach (EA.Element serv in lst)
                                {
                                    foreach (EA.Method method in serv.Methods)
                                    {
                                        if (method.Stereotype == "XisServiceMethod")
                                        {
                                            XisMenuItem menuItem = new XisMenuItem(repository, listDiagram, listIS.Menu,
                                                method.Name, serv.Name + "." + method.Name);
                                            menuItem.SetValue(method.Name);
                                            XisWebHelper.CreateXisAction(repository, menuItem.Element, menuItem.GetOnTapAction(),
                                                ActionType.WebService);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            if (detailModes.Count > 0 || !string.IsNullOrEmpty(item.GetOnTapAction()))
            {
                XisInteractionSpace detailIS = CreateMasterEditorIS(package, master, listIS, useCase, be, services);
                
                foreach (ActionType key in detailModes.Keys)
                {
                    XisMenuItem mItem = detailModes[key];
                    XisWebHelper.CreateXisAction(repository, mItem.Element, mItem.GetOnTapAction(),
                        key, detailIS.Element.Name);
                    CreateXisInteractionSpaceAssociation(mItem.GetOnTapAction(), listIS, detailIS);
                }

                if (!string.IsNullOrEmpty(item.GetOnTapAction()))
                {
                    XisWebHelper.CreateXisAction(repository, item.Element, item.GetOnTapAction(),
                        ActionType.Update, detailIS.Element.Name);
                    CreateXisInteractionSpaceAssociation(item.GetOnTapAction(), listIS, detailIS);
                }
            }

            if (homeIS != listIS)
            {
                if (listIS.Menu == null)
                {
                    listIS.Menu = new XisMenu(repository, listDiagram, listIS, listIS.Element.Name + "Menu", MenuType.OptionsMenu);
                }

                string actionBack = "backTo" + homeIS.Element.Name;
                XisMenuItem backMenuItem = new XisMenuItem(repository, listDiagram, listIS.Menu,
                    "BackTo" + homeIS.Element.Name + "Item", actionBack);
                backMenuItem.SetValue("Go Back");
                XisWebHelper.CreateXisAction(repository, backMenuItem.Element, actionBack, ActionType.Cancel);
                CreateXisInteractionSpaceAssociation(actionBack, listIS, homeIS);
            }

            ComputePositions(listIS, listDiagram);

            if (listIS.ContextMenu != null)
            {
                EA.DiagramObject obj = listIS.GetDiagramObject(listDiagram);
                int center = (obj.top + obj.bottom) / -2;
                listIS.ContextMenu.SetPosition(listDiagram, obj.right + 50, obj.right + 330, -obj.top, -obj.top + 70);
                ComputePositions(listIS.ContextMenu, listDiagram);

                // Create XisIS-MenuAssociation
                EA.DiagramObject sourceObj = item.GetDiagramObject(listDiagram);
                EA.Connector c = item.Element.Connectors.AddNew("", "Association");
                c.ClientID = item.Element.ElementID;
                c.SupplierID = listIS.ContextMenu.Element.ElementID;
                c.Direction = "Source -> Destination";
                c.Stereotype = "XisIS-MenuAssociation";
                c.Update();
                item.Element.Update();
                item.Element.Connectors.Refresh();
            }

            // Associate BE
            AssociateBEtoIS(listDiagram, listIS, be);

            if (!isStartingUC)
            {
                // TODO: Link subspaces
                //CreateXisInteractionSpaceAssociation(repository, "goTo" + listIS.Element.Name, homeIS, listIS);
            }
        }

        public static void ProcessDetailUseCase(EA.Package package, XisEntity master,
            EA.Element useCase, EA.Element be, bool isStartingUC, List<EA.Element> useCases = null, String patternType = null)
        {
            EA.Diagram detailDiagram = XisWebHelper.CreateDiagram(package, master.Element.Name + "EditorIS Diagram",
                "XIS-Web_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = null;

            if (isStartingUC && patternType != null)
            {
                detailIS = new XisInteractionSpace(repository, package, detailDiagram,
                    master.Element.Name + "EditorIS", master.Element.Name + " Editor", InteractionSpaceType.MasterEntityEditor);
            }
            else
            {
                detailIS = new XisInteractionSpace(repository, package, detailDiagram,
                    master.Element.Name + "EditorIS", master.Element.Name + " Editor", InteractionSpaceType.MasterEntityEditor, isStartingUC, !isStartingUC);

                if (isStartingUC && patternType == null)
                {
                    homeIS = detailIS;
                }
            }

            #region Process Master attributes
            if (!string.IsNullOrEmpty(master.Filter))
            {
                XisForm form = new XisForm(repository, detailDiagram, detailIS, master.Element.Name + "Form", master.Element.Name);
                List<EA.Attribute> filtered = GetFilteredAttributeList(master);
                foreach (EA.Attribute attr in filtered)
                {
                    XisWebHelper.ProcessXisAttribute(repository, detailDiagram, form, attr, master.Element.Name);
                }
            }
            else
            {
                XisForm form = new XisForm(repository, detailDiagram, detailIS, master.Element.Name + "Form", master.Element.Name);
                foreach (EA.Attribute attr in master.Element.Attributes)
                {
                    XisWebHelper.ProcessXisAttribute(repository, detailDiagram, form, attr, master.Element.Name);
                }
            }
            #endregion

            #region Process Details info
            foreach (XisEntity d in master.Details)
            {
                if (d.Cardinality.Contains("*"))
                {
                    // Needs Manager screen
                    //string actionName = "goTo" + d.Element.Name + "ManagerIS";
                    //XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "ManagerButton", actionName);
                    //btn.SetValue("Manage " + d.Element.Name);
                    //XisInteractionSpace managerIS = 
                    //XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, managerIS.Element.Name);
                    //CreateXisInteractionSpaceAssociation(actionName, detailIS, managerIS);
                    XisCollapsible collapsible = CreateDetailOrRefManagerCollapsible(package, detailDiagram, d, detailIS, useCase, true, be, d.Element.Name + "Collapsible");
                }
                else
                {
                    List<EA.Attribute> filtered = new List<EA.Attribute>();

                    if (!string.IsNullOrEmpty(d.Filter))
                    {
                        filtered = GetFilteredAttributeList(d);
                    }

                    if (filtered.Count > 0)
                    {
                        if (filtered.Count > 3)
                        {
                            string actionName = "goTo" + d.Element.Name + "EditorIS";
                            XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "EditorButton", actionName);
                            btn.SetValue(d.Element.Name);
                            XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, d, detailIS, useCase, true, be);
                            XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, editorIS.Element.Name);
                            CreateXisInteractionSpaceAssociation(actionName, detailIS, editorIS);
                        }
                        else
                        {
                            XisForm form = new XisForm(repository, detailDiagram, detailIS, d.Element.Name + "Form", d.Element.Name);
                            foreach (EA.Attribute attr in filtered)
                            {
                                XisWebHelper.ProcessXisAttribute(repository, detailDiagram, form, attr, d.Element.Name);
                            }

                            if (ContainsReadDetail(useCase))
                            {
                                if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                                {
                                    XisVisibilityBoundary b = new XisVisibilityBoundary(repository, detailDiagram, form,
                                        "Save" + d.Element.Name + "Boundary", ContainsCreateDetail(useCase), false, ContainsUpdateDetail(useCase));
                                    b.SetEntityName(d.Element.Name);
                                    string actionName = "save" + d.Element.Name;
                                    XisButton btn = new XisButton(repository, b, detailDiagram, d.Element.Name + "SaveButton", actionName);
                                    btn.SetValue("Save " + d.Element.Name);
                                    XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                                }
                            }
                            else if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                            {
                                string actionName = "save" + d.Element.Name;
                                XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + d.Element.Name);
                                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                    }
                    else if (d.Element.Attributes.Count > 3)
                    {
                        string actionName = "goTo" + d.Element.Name + "EditorIS";
                        XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "EditorButton", actionName);
                        btn.SetValue(d.Element.Name);
                        XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, d, detailIS, useCase, true, be);
                        XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, editorIS.Element.Name);
                        CreateXisInteractionSpaceAssociation(actionName, detailIS, editorIS);
                    }
                    else
                    {
                        XisForm form = new XisForm(repository, detailDiagram, detailIS, d.Element.Name + "Form", d.Element.Name);
                        foreach (EA.Attribute attr in d.Element.Attributes)
                        {
                            XisWebHelper.ProcessXisAttribute(repository, detailDiagram, form, attr, d.Element.Name);
                        }

                        if (ContainsReadDetail(useCase))
                        {
                            if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                            {
                                XisVisibilityBoundary b = new XisVisibilityBoundary(repository, detailDiagram, form,
                                    "Save" + d.Element.Name + "Boundary", ContainsCreateDetail(useCase), false, ContainsUpdateDetail(useCase));
                                b.SetEntityName(d.Element.Name);
                                string actionName = "save" + d.Element.Name;
                                XisButton btn = new XisButton(repository, b, detailDiagram, d.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + d.Element.Name);
                                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                        else if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                        {
                            string actionName = "save" + d.Element.Name;
                            XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "SaveButton", actionName);
                            btn.SetValue("Save " + d.Element.Name);
                            XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                        }
                    }
                }
            }
            #endregion

            #region Process References info
            foreach (XisEntity r in master.References)
            {
                if (r.Cardinality.Contains("*"))
                {
                    // Needs Manager screen
                    //string actionName = "goTo" + r.Element.Name + "ManagerIS";
                    //XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "ManagerButton", actionName);
                    //btn.SetValue("Manage " + r.Element.Name);
                    //XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, viewIS.Element.Name);
                    //CreateXisInteractionSpaceAssociation(actionName, detailIS, viewIS);
                    XisCollapsible collapsible = CreateDetailOrRefManagerCollapsible(package, detailDiagram, r, detailIS, useCase, false, be, r.Element.Name + "Collapsible");
                }
                else
                {
                    List<EA.Attribute> filtered = new List<EA.Attribute>();

                    if (!string.IsNullOrEmpty(r.Filter))
                    {
                        filtered = GetFilteredAttributeList(r);
                    }

                    if (filtered.Count > 0)
                    {
                        if (filtered.Count > 3)
                        {
                            string actionName = "goTo" + r.Element.Name + "EditorIS";
                            XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "EditorButton", actionName);
                            btn.SetValue(r.Element.Name);
                            XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, r, detailIS, useCase, false, be);
                            XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, editorIS.Element.Name);
                            CreateXisInteractionSpaceAssociation(actionName, detailIS, editorIS);
                        }
                        else
                        {
                            XisForm form = new XisForm(repository, detailDiagram, detailIS, r.Element.Name + "Form", r.Element.Name);
                            foreach (EA.Attribute attr in filtered)
                            {
                                XisWebHelper.ProcessXisAttribute(repository, detailDiagram, form, attr, r.Element.Name);
                            }

                            if (ContainsReadReference(useCase))
                            {
                                if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                                {
                                    XisVisibilityBoundary b = new XisVisibilityBoundary(repository, detailDiagram, form,
                                        "Save" + r.Element.Name + "Boundary", ContainsCreateReference(useCase), false, ContainsUpdateReference(useCase));
                                    b.SetEntityName(r.Element.Name);
                                    string actionName = "save" + r.Element.Name;
                                    XisButton btn = new XisButton(repository, b, detailDiagram, r.Element.Name + "SaveButton", actionName);
                                    btn.SetValue("Save " + r.Element.Name);
                                    XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                                }
                            }
                            else if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                            {
                                string actionName = "save" + r.Element.Name;
                                XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + r.Element.Name);
                                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                    }
                    else if (r.Element.Attributes.Count > 3)
                    {
                        string actionName = "goTo" + r.Element.Name + "EditorIS";
                        XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "EditorButton", actionName);
                        btn.SetValue(r.Element.Name);
                        XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, r, detailIS, useCase, false, be);
                        XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, editorIS.Element.Name);
                        CreateXisInteractionSpaceAssociation(actionName, detailIS, editorIS);
                    }
                    else
                    {
                        XisForm form = new XisForm(repository, detailDiagram, detailIS, r.Element.Name + "Form", r.Element.Name);
                        foreach (EA.Attribute attr in r.Element.Attributes)
                        {
                            XisWebHelper.ProcessXisAttribute(repository, detailDiagram, form, attr, r.Element.Name);
                        }

                        if (ContainsReadReference(useCase))
                        {
                            if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                            {
                                XisVisibilityBoundary b = new XisVisibilityBoundary(repository, detailDiagram, form,
                                    "Save" + r.Element.Name + "Boundary", ContainsCreateReference(useCase), false, ContainsUpdateReference(useCase));
                                b.SetEntityName(r.Element.Name);
                                string actionName = "save" + r.Element.Name;
                                XisButton btn = new XisButton(repository, b, detailDiagram, r.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + r.Element.Name);
                                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                        else if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                        {
                            string actionName = "save" + r.Element.Name;
                            XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "SaveButton", actionName);
                            btn.SetValue("Save " + r.Element.Name);
                            XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                        }
                    }
                }
            }
            #endregion

            // Navigation between main UC and the others
            if (patternType != null)
            {
                AddToHomeISByPattern(useCase, detailIS, patternType);
            }
            else if (isStartingUC)
            {
                if (useCases != null)
                {
                    AssociateFirstSubSpaces(detailDiagram, useCases, detailIS, be.ElementID, master.Element.Name);
                }
            }

            if (ContainsCreateMaster(useCase) || ContainsUpdateMaster(useCase))
            {
                XisWidget parent = detailIS;

                if (ContainsReadMaster(useCase))
                {
                    parent = new XisVisibilityBoundary(repository, detailDiagram, detailIS, "Save" + master.Element.Name + "Boundary",
                        ContainsCreateMaster(useCase), false, ContainsUpdateMaster(useCase));
                    ((XisVisibilityBoundary)parent).SetEntityName(master.Element.Name);
                }

                XisMenu menu = new XisMenu(repository, detailDiagram, parent, detailIS.Element.Name + "Menu", MenuType.OptionsMenu);
                menu.SetEntityName(master.Element.Name);

                string actionName = "save" + master.Element.Name;
                XisMenuItem menuItem = new XisMenuItem(repository, detailDiagram, menu, "Save" + master.Element.Name, actionName);
                menuItem.SetValue("Save " + master.Element.Name);
                XisWebHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.OK);
                detailIS.Menu = menu;
            }

            #region Check ServiceUC Extensions
            Dictionary<List<EA.Element>, bool> services = new Dictionary<List<EA.Element>, bool>();

            if (useCase.Connectors.Count > 0)
            {
                List<EA.Element> extends = new List<EA.Element>();
                Dictionary<EA.Element, bool> providers = new Dictionary<EA.Element, bool>();

                foreach (EA.Connector conn in useCase.Connectors)
                {
                    if (conn.Stereotype == "extend")
                    {
                        EA.Element el = repository.GetElementByID(conn.ClientID);
                        extends.Add(el);
                    }
                }

                foreach (EA.Element e in extends)
                {
                    bool hasBE = false;
                    EA.Element provider = null;

                    foreach (EA.Connector conn in e.Connectors)
                    {
                        if (conn.Stereotype == "XisServiceUC-BEAssociation")
                        {
                            hasBE = true;
                        }
                        else if (conn.Stereotype == "XisServiceUC-ProviderAssociation")
                        {
                            provider = repository.GetElementByID(conn.SupplierID);
                            providers.Add(provider, false);
                        }
                    }

                    if (provider != null && hasBE)
                    {
                        providers[provider] = true;
                    }
                }

                foreach (EA.Element p in providers.Keys)
                {
                    List<EA.Element> lst = new List<EA.Element>();

                    foreach (EA.Connector c in p.Connectors)
                    {
                        if (c.Stereotype == "XisProvider-ServiceRealization")
                        {
                            EA.Element el = repository.GetElementByID(c.SupplierID);
                            lst.Add(el);
                        }
                    }

                    if (lst.Count > 0)
                    {
                        services.Add(lst, providers[p]);
                    }
                }

                if (services.Count > 0)
                {
                    if (detailIS.Menu == null)
                    {
                        detailIS.Menu = new XisMenu(repository, detailDiagram, detailIS, detailIS.Element.Name + "Menu",
                            MenuType.OptionsMenu);
                    }

                    foreach (List<EA.Element> lst in services.Keys)
                    {
                        foreach (EA.Element serv in lst)
                        {
                            foreach (EA.Method method in serv.Methods)
                            {
                                if (method.Stereotype == "XisServiceMethod")
                                {
                                    XisMenuItem menuItem = new XisMenuItem(repository, detailDiagram, detailIS.Menu,
                                        method.Name, serv.Name + "." + method.Name);
                                    menuItem.SetValue(method.Name);
                                    XisWebHelper.CreateXisAction(repository, menuItem.Element, menuItem.GetOnTapAction(),
                                        ActionType.WebService);
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            if (homeIS != detailIS)
            {
                if (detailIS.Menu == null)
                {
                    detailIS.Menu = new XisMenu(repository, detailDiagram, detailIS, detailIS.Element.Name + "Menu", MenuType.OptionsMenu);
                }

                string actionBack = "backTo" + homeIS.Element.Name;
                XisMenuItem backMenuItem = new XisMenuItem(repository, detailDiagram, detailIS.Menu,
                    "BackTo" + homeIS.Element.Name + "Item", actionBack);
                backMenuItem.SetValue("Go Back");
                XisWebHelper.CreateXisAction(repository, backMenuItem.Element, actionBack, ActionType.Cancel);
                CreateXisInteractionSpaceAssociation(actionBack, detailIS, homeIS);
            }

            ComputePositions(detailIS, detailDiagram);
            // Associate BE
            AssociateBEtoIS(detailDiagram, detailIS, be);

            if (detailIS.GetDiagramObject(nsDiagram) == null && isStartingUC)
            {
                detailIS.SetPosition(nsDiagram, 355, 445, 10, 80);
            }

            if (!isStartingUC)
            {
                // TODO: Link subspaces
                //CreateXisInteractionSpaceAssociation("goTo" + detailIS.Element.Name, homeIS, detailIS);
            }
        }

        private static XisInteractionSpace CreateMasterEditorIS(EA.Package package, XisEntity master,
            XisInteractionSpace previousIS, EA.Element useCase, EA.Element be, Dictionary<List<EA.Element>, bool> services)
        {
            EA.Diagram diagram = XisWebHelper.CreateDiagram(package, master.Element.Name + "EditorIS Diagram",
                "XIS-Web_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = new XisInteractionSpace(repository, package, diagram, master.Element.Name + "EditorIS",
                master.Element.Name + " Editor", InteractionSpaceType.MasterEntityEditor, false, true);

            //Image & Sitemap Creation - Define a proper source path!
            XisImage img = new XisImage(repository, detailIS, diagram, "../../images/default.png");

            XisSiteMap siteMap = new XisSiteMap(repository, detailIS, diagram);

            #region Process Master attributes
            if (!string.IsNullOrEmpty(master.Filter))
            {
                XisForm form = new XisForm(repository, diagram, detailIS, master.Element.Name + "Form", master.Element.Name);
                List<EA.Attribute> filtered = GetFilteredAttributeList(master);
                foreach (EA.Attribute attr in filtered)
                {
                    XisWebHelper.ProcessXisAttribute(repository, diagram, form, attr, master.Element.Name);
                }
            }
            else
            {
                XisForm form = new XisForm(repository, diagram, detailIS, master.Element.Name + "Form", master.Element.Name);
                foreach (EA.Attribute attr in master.Element.Attributes)
                {
                    XisWebHelper.ProcessXisAttribute(repository, diagram, form, attr, master.Element.Name);
                }
            }
            #endregion

            #region Write Details info
            foreach (XisEntity d in master.Details)
            {
                if (d.Cardinality.Contains("*"))
                {
                    // Needs Manager screen
                    //string actionName = "goTo" + d.Element.Name + "ManagerIS";
                    //XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "ManagerButton", actionName);
                    //btn.SetValue("Manage " + d.Element.Name);
                    //XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, managerIS.Element.Name);
                    //CreateXisInteractionSpaceAssociation(actionName, detailIS, managerIS);
                    XisCollapsible collapsible = CreateDetailOrRefManagerCollapsible(package, diagram, d, detailIS, useCase, true, be, d.Element.Name + "Collapsible");
                }
                else
                {
                    List<EA.Attribute> filtered = new List<EA.Attribute>();

                    if (!string.IsNullOrEmpty(d.Filter))
                    {
                        filtered = GetFilteredAttributeList(d);
                    }

                    if (filtered.Count > 0)
                    {
                        if (filtered.Count > 3)
                        {
                            string actionName = "goTo" + d.Element.Name + "EditorIS";
                            XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "EditorButton", actionName);
                            btn.SetValue(d.Element.Name);
                            XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, d, detailIS, useCase, true, be);
                            XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, editorIS.Element.Name);
                            CreateXisInteractionSpaceAssociation(actionName, detailIS, editorIS);
                        }
                        else
                        {
                            XisForm form = new XisForm(repository, diagram, detailIS, d.Element.Name + "Form", d.Element.Name);
                            foreach (EA.Attribute attr in filtered)
                            {
                                XisWebHelper.ProcessXisAttribute(repository, diagram, form, attr, d.Element.Name);
                            }

                            if (ContainsReadDetail(useCase))
                            {
                                if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                                {
                                    XisVisibilityBoundary b = new XisVisibilityBoundary(repository, diagram, form,
                                        "Save" + d.Element.Name + "Boundary", ContainsCreateDetail(useCase), false, ContainsUpdateDetail(useCase));
                                    b.SetEntityName(d.Element.Name);
                                    string actionName = "save" + d.Element.Name;
                                    XisButton btn = new XisButton(repository, b, diagram, d.Element.Name + "SaveButton", actionName);
                                    btn.SetValue("Save " + d.Element.Name);
                                    XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                                }
                            }
                            else if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                            {
                                string actionName = "save" + d.Element.Name;
                                XisButton btn = new XisButton(repository, form, diagram, d.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + d.Element.Name);
                                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                    }
                    else if (d.Element.Attributes.Count > 3)
                    {
                        string actionName = "goTo" + d.Element.Name + "EditorIS";
                        XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "EditorButton", actionName);
                        btn.SetValue(d.Element.Name);
                        XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, d, detailIS, useCase, true, be);
                        XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, editorIS.Element.Name);
                        CreateXisInteractionSpaceAssociation(actionName, detailIS, editorIS);
                    }
                    else
                    {
                        XisForm form = new XisForm(repository, diagram, detailIS, d.Element.Name + "Form", d.Element.Name);
                        foreach (EA.Attribute attr in d.Element.Attributes)
                        {
                            XisWebHelper.ProcessXisAttribute(repository, diagram, form, attr, d.Element.Name);
                        }

                        if (ContainsReadDetail(useCase))
                        {
                            if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                            {
                                XisVisibilityBoundary b = new XisVisibilityBoundary(repository, diagram, form,
                                    "Save" + d.Element.Name + "Boundary", ContainsCreateDetail(useCase), false, ContainsUpdateDetail(useCase));
                                b.SetEntityName(d.Element.Name);
                                string actionName = "save" + d.Element.Name;
                                XisButton btn = new XisButton(repository, b, diagram, d.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + d.Element.Name);
                                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                        else if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                        {
                            string actionName = "save" + d.Element.Name;
                            XisButton btn = new XisButton(repository, form, diagram, d.Element.Name + "SaveButton", actionName);
                            btn.SetValue("Save " + d.Element.Name);
                            XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                        }
                    }
                }
            }
            #endregion

            #region Write References info
            foreach (XisEntity r in master.References)
            {
                if (r.Cardinality.Contains("*"))
                {
                    //// Needs Manager screen
                    //string actionName = "goTo" + r.Element.Name + "ManagerIS";
                    //XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "ManagerButton", actionName);
                    //btn.SetValue("Manage " + r.Element.Name);
                    //XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, viewIS.Element.Name);
                    //CreateXisInteractionSpaceAssociation(actionName, detailIS, viewIS);
                    XisCollapsible collapsible = CreateDetailOrRefManagerCollapsible(package, diagram, r, detailIS, useCase, false, be, r.Element.Name);
                }
                else
                {
                    List<EA.Attribute> filtered = new List<EA.Attribute>();

                    if (!string.IsNullOrEmpty(r.Filter))
                    {
                        filtered = GetFilteredAttributeList(r);
                    }

                    if (filtered.Count > 0)
                    {
                        if (filtered.Count > 3)
                        {
                            string actionName = "goTo" + r.Element.Name + "EditorIS";
                            XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "EditorButton", actionName);
                            btn.SetValue(r.Element.Name);
                            XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, r, detailIS, useCase, false, be);
                            XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, editorIS.Element.Name);
                            CreateXisInteractionSpaceAssociation(actionName, detailIS, editorIS);
                        }
                        else
                        {
                            XisForm form = new XisForm(repository, diagram, detailIS, r.Element.Name + "Form", r.Element.Name);
                            foreach (EA.Attribute attr in filtered)
                            {
                                XisWebHelper.ProcessXisAttribute(repository, diagram, form, attr, r.Element.Name);
                            }

                            if (ContainsReadReference(useCase))
                            {
                                if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                                {
                                    XisVisibilityBoundary b = new XisVisibilityBoundary(repository, diagram, form,
                                        "Save" + r.Element.Name + "Boundary", ContainsCreateReference(useCase), false, ContainsUpdateReference(useCase));
                                    b.SetEntityName(r.Element.Name);
                                    string actionName = "save" + r.Element.Name;
                                    XisButton btn = new XisButton(repository, b, diagram, r.Element.Name + "SaveButton", actionName);
                                    btn.SetValue("Save " + r.Element.Name);
                                    XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                                }
                            }
                            else if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                            {
                                string actionName = "save" + r.Element.Name;
                                XisButton btn = new XisButton(repository, form, diagram, r.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + r.Element.Name);
                                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                    }
                    else if (r.Element.Attributes.Count > 3)
                    {
                        string actionName = "goTo" + r.Element.Name + "EditorIS";
                        XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "EditorButton", actionName);
                        btn.SetValue(r.Element.Name);
                        XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, r, detailIS, useCase, false, be);
                        XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate, editorIS.Element.Name);
                        CreateXisInteractionSpaceAssociation(actionName, detailIS, editorIS);
                    }
                    else
                    {
                        XisForm form = new XisForm(repository, diagram, detailIS, r.Element.Name + "Form", r.Element.Name);
                        foreach (EA.Attribute attr in r.Element.Attributes)
                        {
                            XisWebHelper.ProcessXisAttribute(repository, diagram, form, attr, r.Element.Name);
                        }

                        if (ContainsReadReference(useCase))
                        {
                            if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                            {
                                XisVisibilityBoundary b = new XisVisibilityBoundary(repository, diagram, form,
                                    "Save" + r.Element.Name + "Boundary", ContainsCreateReference(useCase), false, ContainsUpdateReference(useCase));
                                b.SetEntityName(r.Element.Name);
                                string actionName = "save" + r.Element.Name;
                                XisButton btn = new XisButton(repository, b, diagram, r.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + r.Element.Name);
                                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                        else if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                        {
                            string actionName = "save" + r.Element.Name;
                            XisButton btn = new XisButton(repository, form, diagram, r.Element.Name + "SaveButton", actionName);
                            btn.SetValue("Save " + r.Element.Name);
                            XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                        }
                    }
                }
            }
            #endregion

            XisMenu menu = new XisMenu(repository, diagram, detailIS, detailIS.Element.Name + "Menu", MenuType.OptionsMenu);

            if (ContainsCreateMaster(useCase) || ContainsUpdateMaster(useCase))
            {
                menu.SetEntityName(master.Element.Name);
                string actionName = "save" + master.Element.Name;
                XisWidget parent = menu;

                if (ContainsReadMaster(useCase))
                {
                    parent = new XisVisibilityBoundary(repository, diagram, menu, "Save" + master.Element.Name + "Boundary",
                        ContainsCreateMaster(useCase), false, ContainsUpdateMaster(useCase));
                    ((XisVisibilityBoundary)parent).SetEntityName(master.Element.Name);
                }

                XisMenuItem menuItem = new XisMenuItem(repository, diagram, parent, "Save" + master.Element.Name, actionName);
                menuItem.SetValue("Save " + master.Element.Name);
                XisWebHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.OK, previousIS.Element.Name);
                CreateXisInteractionSpaceAssociation(actionName, detailIS, previousIS);
            }

            if (ContainsDeleteMaster(useCase))
            {
                XisWidget parent = menu;

                menu.SetEntityName(master.Element.Name);
                string actionName = "delete" + master.Element.Name;
                XisMenuItem contextItem = new XisMenuItem(repository, diagram, parent,
                    "Delete" + master.Element.Name + "Item", actionName);
                contextItem.SetValue("Delete " + master.Element.Name);
                XisWebHelper.CreateXisAction(repository, contextItem.Element, actionName, ActionType.Delete);
            }

            #region Check ServiceUC Extensions
            if (services.Count > 0)
            {
                if (services.Values.Contains(true))
                {
                    foreach (List<EA.Element> lst in services.Keys)
                    {
                        if (services[lst])
                        {
                            foreach (EA.Element serv in lst)
                            {
                                foreach (EA.Method method in serv.Methods)
                                {
                                    if (method.Stereotype == "XisServiceMethod")
                                    {
                                        XisMenuItem menuItem = new XisMenuItem(repository, diagram, menu,
                                            method.Name, serv.Name + "." + method.Name);
                                        menuItem.SetValue(method.Name);
                                        XisWebHelper.CreateXisAction(repository, menuItem.Element, menuItem.GetOnTapAction(),
                                            ActionType.WebService);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            string cancelAction = "cancel" + master.Element.Name;
            XisMenuItem cancelItem = new XisMenuItem(repository, diagram, menu, "Cancel" + master.Element.Name, cancelAction);
            cancelItem.SetValue("Go Back"/*+ master.Element.Name*/);
            XisWebHelper.CreateXisAction(repository, cancelItem.Element, cancelAction, ActionType.Cancel, previousIS.Element.Name);
            CreateXisInteractionSpaceAssociation(cancelAction, detailIS, previousIS);

            ComputePositions(detailIS, diagram);

            // Associate BE
            AssociateBEtoIS(diagram, detailIS, be);

            return detailIS;
        }

        public static XisInteractionSpace CreateDetailOrRefEditorIS(EA.Package package, XisEntity entity,
            XisInteractionSpace previousIS, EA.Element useCase, bool isDetail, EA.Element be)
        {
            EA.Diagram diagram = XisWebHelper.CreateDiagram(package, entity.Element.Name + "EditorIS Diagram",
                "XIS-Web_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = new XisInteractionSpace(repository, package, diagram, entity.Element.Name + "EditorIS", entity.Element.Name + " Editor", InteractionSpaceType.DetailEntityEditor);

            //Image & Sitemap Creation - Define a proper source path!
            XisImage img = new XisImage(repository, detailIS, diagram, "../../images/default.png");

            XisSiteMap siteMap = new XisSiteMap(repository, detailIS, diagram);

            XisForm form = new XisForm(repository, diagram, detailIS, entity.Element.Name + "Form", entity.Element.Name);

            if (!string.IsNullOrEmpty(entity.Filter))
            {
                List<EA.Attribute> filtered = GetFilteredAttributeList(entity);
                foreach (EA.Attribute attr in filtered)
                {
                    XisWebHelper.ProcessXisAttribute(repository, diagram, form, attr, entity.Element.Name);
                }
            }
            else
            {
                foreach (EA.Attribute attr in entity.Element.Attributes)
                {
                    XisWebHelper.ProcessXisAttribute(repository, diagram, form, attr, entity.Element.Name);
                }
            }

            XisMenu menu = new XisMenu(repository, diagram, detailIS, entity.Element.Name + "Menu", MenuType.OptionsMenu);

            if ((isDetail && (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase)))
                || (!isDetail && (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))))
            {
                menu.SetEntityName(entity.Element.Name);
                string actionName = "save" + entity.Element.Name;
                XisMenuItem menuItem = new XisMenuItem(repository, diagram, menu, "Save" + entity.Element.Name, actionName);
                menuItem.SetValue("Save " + entity.Element.Name);
                XisWebHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.OK, previousIS.Element.Name);
                CreateXisInteractionSpaceAssociation(actionName, detailIS, previousIS);
            }

            string cancelAction = "cancel" + entity.Element.Name;
            XisMenuItem cancelItem = new XisMenuItem(repository, diagram, menu, "Cancel" + entity.Element.Name, cancelAction);
            cancelItem.SetValue("Go Back "/* + entity.Element.Name*/);
            XisWebHelper.CreateXisAction(repository, cancelItem.Element, cancelAction, ActionType.Cancel);
            CreateXisInteractionSpaceAssociation("cancel" + entity.Element.Name, detailIS, previousIS);

            ComputePositions(detailIS, diagram);
            // Associate BE
            AssociateBEtoIS(diagram, detailIS, be);

            return detailIS;
        }

        public static XisCollapsible CreateDetailOrRefManagerCollapsible(EA.Package package, EA.Diagram diagram, XisEntity entity,
            XisInteractionSpace parentIS, EA.Element useCase, bool isDetail, EA.Element be, string name, string searchBy = null, string orderBy = null)
        {
            XisCollapsible collapsible = new XisCollapsible(repository, diagram, parentIS, entity.Element.Name + "Collapsible");
            collapsible.SetEntityName(entity.Element.Name);

            XisCollapsibleItem collapsibleItem = new XisCollapsibleItem(repository, diagram, collapsible, collapsible.Element.Name + "Item");

            XisList list = new XisList(repository, diagram, collapsibleItem, entity.Element.Name + "List");
            list.SetEntityName(entity.Element.Name);

            XisListItem item = new XisListItem(repository, diagram, list, list.Element.Name + "Item");
            if (entity.Element.Attributes.Count > 1)
            {
                EA.Attribute first = entity.Element.Attributes.GetAt(0);
                EA.Attribute second = entity.Element.Attributes.GetAt(1);
                XisLabel lbl1 = new XisLabel(repository, item, diagram, first.Name + "Lbl");
                lbl1.SetEntityAttributeName(entity.Element.Name + "." + first.Name);
                XisLabel lbl2 = new XisLabel(repository, item, diagram, second.Name + "Lbl");
                lbl2.SetEntityAttributeName(entity.Element.Name + "." + second.Name);
            }
            else if (entity.Element.Attributes.Count == 1)
            {
                EA.Attribute attr = entity.Element.Attributes.GetAt(0);
                XisLabel lbl = new XisLabel(repository, item, diagram, attr.Name + "Lbl");
                lbl.SetEntityAttributeName(entity.Element.Name + "." + attr.Name);
            }

            if ((isDetail && ContainsUpdateDetail(useCase))
                    || (!isDetail && ContainsUpdateReference(useCase)))
            {
                string actionName = "edit" + entity.Element.Name;
                item.SetOnTapAction(actionName);
            }
            if ((isDetail && ContainsReadDetail(useCase))
                || (!isDetail && ContainsReadReference(useCase)))
            {
                string actionName = "view" + entity.Element.Name;
                item.SetOnTapAction(actionName);
            }

            Dictionary<ActionType, XisMenuItem> detailModes = new Dictionary<ActionType, XisMenuItem>();

            #region Create Context Menu
            if (isDetail && (ContainsReadDetail(useCase) || ContainsUpdateDetail(useCase) || ContainsDeleteDetail(useCase))
                || !isDetail && (ContainsReadReference(useCase) || ContainsUpdateReference(useCase) || ContainsDeleteReference(useCase)))
            {
                XisMenu context = new XisMenu(repository, diagram, package, list.Element.Name + "ContextMenu", MenuType.ContextMenu);

                if (isDetail && ContainsReadDetail(useCase)
                    || !isDetail && ContainsReadReference(useCase))
                {
                    context.SetEntityName(entity.Element.Name);
                    string actionName = "view" + entity.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, diagram, context,
                        "View" + entity.Element.Name + "Item", actionName);
                    contextItem.SetValue("View " + entity.Element.Name);
                    detailModes.Add(ActionType.Read, contextItem);
                }

                if (isDetail && ContainsUpdateDetail(useCase)
                    || !isDetail && ContainsUpdateReference(useCase))
                {
                    context.SetEntityName(entity.Element.Name);
                    string actionName = "edit" + entity.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, diagram, context,
                        "Edit" + entity.Element.Name + "Item", actionName);
                    contextItem.SetValue("Edit " + entity.Element.Name);
                    detailModes.Add(ActionType.Update, contextItem);
                }

                if (isDetail && ContainsUpdateDetail(useCase)
                    || !isDetail && ContainsUpdateReference(useCase))
                {
                    context.SetEntityName(entity.Element.Name);
                    string actionName = "delete" + entity.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, diagram, context,
                        "Delete" + entity.Element.Name + "Item", actionName);
                    contextItem.SetValue("Delete " + entity.Element.Name);
                    XisWebHelper.CreateXisAction(repository, contextItem.Element, actionName, ActionType.Delete);
                }
                collapsible.ContextMenu = context;
            }
            #endregion

            #region Create Options Menu
            XisMenu menu = new XisMenu(repository, diagram, collapsibleItem, collapsibleItem.Element.Name + "Menu", MenuType.OptionsMenu);

            if ((isDetail && (ContainsCreateDetail(useCase) || ContainsDeleteDetail(useCase)))
                || (!isDetail && (ContainsCreateReference(useCase) || ContainsDeleteReference(useCase))))
            {
                menu.SetEntityName(entity.Element.Name);
                string actionName = "create" + entity.Element.Name;
                XisMenuItem menuItem = new XisMenuItem(repository, diagram, menu,
                    "Create" + entity.Element.Name + "Item", actionName);
                menuItem.SetValue("Create " + entity.Element.Name);
                detailModes.Add(ActionType.Create, menuItem);
            }

            if (isDetail && (ContainsDeleteDetail(useCase))
                || !isDetail && (ContainsDeleteReference(useCase)))
            {
                menu.SetEntityName(entity.Element.Name);
                string actionName = "deleteAll" + entity.Element.Name + "s";
                XisMenuItem menuItem = new XisMenuItem(repository, diagram, menu,
                    "DeleteAll" + entity.Element.Name + "Item", actionName);
                menuItem.SetValue("Delete all " + entity.Element.Name + "s");
                XisWebHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.DeleteAll);
            }

            //No need for Close action in Collapsible since the panel closes itself
            /*string actionClose = "close" + entity.Element.Name;
            XisMenuItem backMenuItem = new XisMenuItem(repository, diagram, menu,
                "Close" + entity.Element.Name + "Item", actionClose);
            backMenuItem.SetValue("Close");
            XisWebHelper.CreateXisAction(repository, backMenuItem.Element, actionClose, ActionType.Cancel);*/

            collapsible.Menu = menu;
            #endregion

            if (detailModes.Count > 0 || !string.IsNullOrEmpty(item.GetOnTapAction()))
            {
                XisInteractionSpace detailIS = CreateDetailOrRefEditorIS(package, entity, parentIS, useCase, isDetail, be);
                foreach (ActionType key in detailModes.Keys)
                {
                    XisMenuItem mItem = detailModes[key];
                    XisWebHelper.CreateXisAction(repository, mItem.Element, mItem.GetOnTapAction(),
                        key, detailIS.Element.Name);
                    CreateXisInteractionSpaceAssociation(mItem.GetOnTapAction(), parentIS, detailIS);
                }

                if (!string.IsNullOrEmpty(item.GetOnTapAction()))
                {
                    XisWebHelper.CreateXisAction(repository, item.Element, item.GetOnTapAction(),
                        ActionType.Update, detailIS.Element.Name);
                    CreateXisInteractionSpaceAssociation(item.GetOnTapAction(), parentIS, detailIS);
                }
            }

            ComputePositions(collapsible, diagram, parentIS.GetDiagramObject(diagram), null);

            if (collapsible.ContextMenu != null)
            {
                EA.DiagramObject obj = collapsible.GetDiagramObject(diagram);
                int center = (obj.top + obj.bottom) / -2;
                collapsible.ContextMenu.SetPosition(diagram, obj.right + 50, obj.right + 330, -obj.top, -obj.top + 70);
                ComputePositions(collapsible.ContextMenu, diagram);

                // Create XisIS-MenuAssociation
                EA.DiagramObject sourceObj = item.GetDiagramObject(diagram);
                EA.Connector c = item.Element.Connectors.AddNew("", "Association");
                c.ClientID = item.Element.ElementID;
                c.SupplierID = collapsible.ContextMenu.Element.ElementID;
                c.Direction = "Source -> Destination";
                c.Stereotype = "XisIS-MenuAssociation";
                c.Update();
                item.Element.Update();
                item.Element.Connectors.Refresh();
            }

            return collapsible;
        }

        public static void ProcessServiceUseCase(EA.Package package, XisEntity master,
            EA.Element useCase, EA.Element be, bool isStartingUC, List<EA.Element> useCases = null, String patternType = null)
        {
            // Create IS Diagram
            string serviceISName = useCase.Name.Replace(" ", "") + "IS";
            EA.Diagram serviceDiagram = XisWebHelper.CreateDiagram(package, serviceISName + " Diagram",
                "XIS-Web_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace serviceIS = null;

            if (isStartingUC && patternType != null)
            {
                serviceIS = new XisInteractionSpace(repository, package, serviceDiagram,
                    serviceISName, useCase.Name, InteractionSpaceType.ServiceInteractionSpace);
            }
            else
            {
                serviceIS = new XisInteractionSpace(repository, package, serviceDiagram,
                    serviceISName, useCase.Name, InteractionSpaceType.ServiceInteractionSpace,isStartingUC, !isStartingUC);

                if (isStartingUC && patternType == null)
                {
                    homeIS = serviceIS;
                }
            }

            // List Creation
            XisList list = new XisList(repository, serviceDiagram, serviceIS, master.Element.Name + "List");
            list.SetEntityName(master.Element.Name);

            XisListItem item = new XisListItem(repository, serviceDiagram, list, list.Element.Name + "Item");

            if (master.Element.Attributes.Count > 1)
            {
                EA.Attribute first = master.Element.Attributes.GetAt(0);
                EA.Attribute second = master.Element.Attributes.GetAt(1);
                XisLabel lbl1 = new XisLabel(repository, item, serviceDiagram, first.Name + "Lbl");
                lbl1.SetEntityAttributeName(master.Element.Name + "." + first.Name);
                XisLabel lbl2 = new XisLabel(repository, item, serviceDiagram, second.Name + "Lbl");
                lbl2.SetEntityAttributeName(master.Element.Name + "." + second.Name);
            }
            else if (master.Element.Attributes.Count == 1)
            {
                EA.Attribute attr = master.Element.Attributes.GetAt(0);
                item.SetValueFromExpression(master.Element.Name + "." + attr.Name);
            }

            // Navigation between home UC and the others
            if (patternType != null)
            {
                AddToHomeISByPattern(useCase, serviceIS, patternType);
            }
            else if (isStartingUC)
            {
                if (useCases != null)
                {
                    AssociateFirstSubSpaces(serviceDiagram, useCases, serviceIS, be.ElementID, master.Element.Name);
                }
            }

            XisMenu menu = new XisMenu(repository, serviceDiagram, serviceIS, serviceIS.Element.Name + "Menu", MenuType.OptionsMenu);
            List<EA.Element> providers = new List<EA.Element>();
            EA.Element provider = null;

            foreach (EA.Connector conn in useCase.Connectors)
            {
                if (conn.Stereotype == "XisServiceUC-ProviderAssociation")
                {
                    provider = repository.GetElementByID(conn.SupplierID);
                    providers.Add(provider);
                }
            }

            List<EA.Element> services = new List<EA.Element>();

            foreach (EA.Element p in providers)
            {
                foreach (EA.Connector c in p.Connectors)
                {
                    if (c.Stereotype == "XisProvider-ServiceRealization")
                    {
                        EA.Element el = repository.GetElementByID(c.SupplierID);
                        services.Add(el);
                    }
                }
            }

            foreach (EA.Element serv in services)
            {
                foreach (EA.Method method in serv.Methods)
                {
                    if (method.Stereotype == "XisServiceMethod")
                    {
                        XisMenuItem menuItem = new XisMenuItem(repository, serviceDiagram, menu,
                            method.Name, serv.Name + "." + method.Name);
                        menuItem.SetValue(method.Name);
                        XisWebHelper.CreateXisAction(repository, menuItem.Element, menuItem.GetOnTapAction(),
                            ActionType.WebService);
                    }
                }
            }

            serviceIS.Menu = menu;

            if (homeIS != serviceIS)
            {
                string actionBack = "backTo" + homeIS.Element.Name;
                XisMenuItem backMenuItem = new XisMenuItem(repository, serviceDiagram, serviceIS.Menu,
                    "BackTo" + homeIS.Element.Name + "Item", actionBack);
                backMenuItem.SetValue("Go Back");
                XisWebHelper.CreateXisAction(repository, backMenuItem.Element, actionBack, ActionType.Cancel);
                CreateXisInteractionSpaceAssociation(actionBack, serviceIS, homeIS);
            }

            ComputePositions(serviceIS, serviceDiagram);

            // Associate BE
            AssociateBEtoIS(serviceDiagram, serviceIS, be);
        }

        #region ComputePositions
        private static void ComputePositions(XisInteractionSpace space, EA.Diagram diagram)
        {
            if (space.Widgets.Count > 0)
            {
                EA.DiagramObject spaceObj = space.GetDiagramObject(diagram);
                ComputePositions(space.Widgets.First(), diagram, spaceObj, null);
                EA.DiagramObject obj = space.Widgets.First().GetDiagramObject(diagram);

                for (int i = 1; i < space.Widgets.Count; i++)
                {
                    ComputePositions(space.Widgets[i], diagram, null, obj);
                    obj = space.Widgets[i].GetDiagramObject(diagram);
                }

                space.SetPosition(diagram, spaceObj.left, spaceObj.right, -spaceObj.top, -obj.bottom + 10, spaceObj.Sequence);
            }
        }

        private static void ComputePositions(XisWidget widget, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            if (widget is XisMenu)
            {
                //MessageBox.Show("MENU " + widget.Element.Name);
                ComputePositions(widget as XisMenu, diagram, parent, sibling);
            }
            else if (widget is XisList)
            {
                //MessageBox.Show("LIST " + widget.Element.Name);
                ComputePositions(widget as XisList, diagram, parent, sibling);
            }
            else if (widget is XisSimpleWidget)
            {
                //MessageBox.Show("SIMPLE " + widget.Element.Name);
                ComputePositions(widget as XisSimpleWidget, diagram, parent, sibling);
            }
            else if (widget is XisCompositeWidget)
            {
                //MessageBox.Show("COMPOSITE " + widget.Element.Name);
                ComputePositions(widget as XisCompositeWidget, diagram, parent, sibling);
            }
        }

        // Use on Context Menus
        private static void ComputePositions(XisMenu menu, EA.Diagram diagram)
        {
            if (menu.Items.Count > 0)
            {
                EA.DiagramObject menuObj = menu.GetDiagramObject(diagram);
                ComputePositions(menu.Items.First(), diagram, menuObj, null);
                EA.DiagramObject obj = menu.Items.First().GetDiagramObject(diagram);

                for (int i = 1; i < menu.Items.Count; i++)
                {
                    ComputePositions(menu.Items[i], diagram, null, obj);
                    obj = menu.Items[i].GetDiagramObject(diagram);
                }

                menu.SetPosition(diagram, menuObj.left, menuObj.right, -menuObj.top, -obj.bottom + 10, menuObj.Sequence);
            }
        }

        private static void ComputePositions(XisList list, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            EA.DiagramObject obj = null;

            if (parent != null)
            {
                list.Element.Methods.Refresh();
                obj = list.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * list.Element.Methods.Count,
                    parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                list.Element.Methods.Refresh();
                obj = list.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * list.Element.Methods.Count,
                    sibling.Sequence);
            }

            if (obj != null)
            {
                if (list.Items.Count > 0)
                {
                    ComputePositions(list.Items.First(), diagram, obj, null);
                    EA.DiagramObject aux = list.Items.First().GetDiagramObject(diagram);

                    for (int i = 1; i < list.Items.Count; i++)
                    {
                        ComputePositions(list.Items[i], diagram, null, aux);
                        aux = list.Items[i].GetDiagramObject(diagram);
                    }

                    list.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
            }
        }

        private static void ComputePositions(XisListItem item, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            EA.DiagramObject obj = null;

            if (parent != null)
            {
                item.Element.Methods.Refresh();
                obj = item.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * item.Element.Methods.Count,
                    parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                item.Element.Methods.Refresh();
                obj = item.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * item.Element.Methods.Count,
                    sibling.Sequence);
            }

            if (obj != null)
            {
                if (item.Widgets.Count > 0)
                {
                    ComputePositions(item.Widgets.First() as XisSimpleWidget, diagram, obj, null);
                    EA.DiagramObject aux = item.Widgets.First().GetDiagramObject(diagram);

                    for (int i = 1; i < item.Widgets.Count; i++)
                    {
                        ComputePositions(item.Widgets[i] as XisSimpleWidget, diagram, null, aux);
                        aux = item.Widgets[i].GetDiagramObject(diagram);
                    }

                    item.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
            }
        }

        private static void ComputePositions(XisMenu menu, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            EA.DiagramObject obj = null;

            if (parent != null)
            {
                menu.Element.Methods.Refresh();
                obj = menu.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * menu.Element.Methods.Count,
                    parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                menu.Element.Methods.Refresh();
                obj = menu.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * menu.Element.Methods.Count,
                    sibling.Sequence);
            }

            if (obj != null)
            {
                if (menu.Widgets.Count > 0)
                {
                    ComputePositions(menu.Widgets.First(), diagram, obj, null);
                    EA.DiagramObject aux = menu.Widgets.First().GetDiagramObject(diagram);

                    for (int i = 1; i < menu.Widgets.Count; i++)
                    {
                        ComputePositions(menu.Widgets[i], diagram, null, aux);
                        aux = menu.Widgets[i].GetDiagramObject(diagram);
                    }

                    menu.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
            }
        }

        private static void ComputePositions(XisCompositeWidget comp, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            EA.DiagramObject obj = null;

            if (parent != null)
            {
                comp.Element.Methods.Refresh();
                obj = comp.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * comp.Element.Methods.Count,
                    parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                comp.Element.Methods.Refresh();
                obj = comp.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * comp.Element.Methods.Count,
                    sibling.Sequence);
            }

            if (obj != null)
            {
                if (comp.Widgets.Count > 0)
                {
                    ComputePositions(comp.Widgets.First(), diagram, obj, null);
                    EA.DiagramObject aux = comp.Widgets.First().GetDiagramObject(diagram);

                    for (int i = 1; i < comp.Widgets.Count; i++)
                    {
                        ComputePositions(comp.Widgets[i], diagram, null, aux);
                        aux = comp.Widgets[i].GetDiagramObject(diagram);
                    }

                    comp.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
            }
        }

        private static void ComputePositions(XisSimpleWidget widget, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            if (parent != null)
            {
                widget.Element.Methods.Refresh();
                EA.Element pElem = repository.GetElementByID(parent.ElementID);
                int pMethodDist = pElem.Methods.Count > 0 ? 15 + pElem.Methods.Count * 20 : 0;
                EA.DiagramObject obj = widget.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40 + pMethodDist,
                    -parent.top + 90 + 30 * widget.Element.Methods.Count + pMethodDist, parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                widget.Element.Methods.Refresh();
                EA.DiagramObject obj = widget.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * widget.Element.Methods.Count,
                    sibling.Sequence);
            }
        } 
        #endregion

        private static void CreateXisInteractionSpaceAssociation(string actionName, XisInteractionSpace source, XisInteractionSpace target)
        {
            int across = 260;
            int down = 180;
            nsDiagram.DiagramObjects.Refresh();

            #region Create Diagram Objects
            if (nsDiagram.DiagramObjects.Count > 0)
            {
                short index = Convert.ToInt16(nsDiagram.DiagramObjects.Count - 1);
                EA.DiagramObject last = null;

                if (source.GetDiagramObject(nsDiagram) == null)
                {
                    if (source.IsMainScreen)
                    {
                        source.SetPosition(nsDiagram, 355, 445, 10, 80);
                    }
                    else
                    {
                        last = nsDiagram.DiagramObjects.GetAt(index);
                        EA.DiagramObject obj = null;

                        if ((last.right + across) > 800)
                        {
                            obj = source.SetPosition(nsDiagram,
                                last.left - across * 3, last.right - across * 3, -last.top + down, -last.bottom + down);
                        }
                        else
                        {
                            obj = source.SetPosition(nsDiagram,
                                last.left + across, last.right + across, -last.top, -last.bottom);
                        }

                        if (target.GetDiagramObject(nsDiagram) == null)
                        {
                            target.SetPosition(nsDiagram,
                                obj.left + across, obj.right + across, -obj.top, -obj.bottom);
                        }
                    }
                }
                else if (target.GetDiagramObject(nsDiagram) == null)
                {
                    if (target.IsMainScreen)
                    {
                        target.SetPosition(nsDiagram, 355, 445, 10, 80);
                    }
                    else
                    {
                        last = nsDiagram.DiagramObjects.GetAt(index);

                        if ((last.right + across) > 800)
                        {
                            target.SetPosition(nsDiagram,
                                last.left - across * 3, last.right - across * 3, -last.top + down, -last.bottom + down);
                        }
                        else
                        {
                            target.SetPosition(nsDiagram,
                                last.left + across, last.right + across, -last.top, -last.bottom);
                        }
                    }
                }
            }
            else
            {
                if (source.IsMainScreen)
                {
                    EA.DiagramObject obj = source.SetPosition(nsDiagram, 355, 445, 10, 80);
                    target.SetPosition(nsDiagram, obj.left - across, obj.right - across, -obj.top + down, -obj.bottom + down);
                }
                else if (target.IsMainScreen)
                {
                    EA.DiagramObject obj = target.SetPosition(nsDiagram, 355, 445, 10, 80);
                    source.SetPosition(nsDiagram, obj.left - across, obj.right - across, -obj.top + down, -obj.bottom + down);
                }
                else if (source.IsFirstSubScreen)
                {
                    //MessageBox.Show(source.Element.Name + "->" + target.Element.Name);
                    EA.DiagramObject obj = source.SetPosition(nsDiagram, 95, 185, 190, 260);
                    target.SetPosition(nsDiagram, obj.left + across, obj.right + across, -obj.top, -obj.bottom);
                }
                else if (target.IsFirstSubScreen)
                {
                    EA.DiagramObject obj = target.SetPosition(nsDiagram, 95, 185, 190, 260);
                    source.SetPosition(nsDiagram, obj.left + across, obj.right + across, -obj.top, -obj.bottom);
                }
                else
                {
                    EA.DiagramObject obj = source.SetPosition(nsDiagram, 355, 445, 190, 260);
                    target.SetPosition(nsDiagram, obj.left + across, obj.right + across, -obj.top, -obj.bottom);
                }
            }
            #endregion

            EA.Connector c = source.Element.Connectors.AddNew(actionName, "Association");
            c.ClientID = source.Element.ElementID;
            c.SupplierID = target.Element.ElementID;
            c.Direction = "Source -> Destination";
            c.Stereotype = "XisInteractionSpaceAssociation";
            c.Update();
            source.Element.Update();
            source.Element.Connectors.Refresh();
        }

        private static void AssociateFirstSubSpaces(EA.Diagram diagram, List<EA.Element> useCases,
            XisInteractionSpace space, int beID, string master)
        {
            XisButton btn = null;
            EA.Element auxBE = null;
            string entityName = null;
            string ucType = null;

            foreach (EA.Element uc in useCases)
            {
                foreach (EA.Connector c in uc.Connectors)
                {
                    if (c.Stereotype == "XisEntityUC-BEAssociation")
                    {
                        if (c.SupplierID != beID)
                        {
                            auxBE = repository.GetElementByID(c.SupplierID);
                            foreach (EA.Connector conn in auxBE.Connectors)
                            {
                                if (conn.Stereotype == "XisBE-EntityMasterAssociation")
                                {
                                    entityName = repository.GetElementByID(conn.SupplierID).Name;
                                }
                            }
                        }
                        else
                        {
                            entityName = master;
                        }
                        break;
                    }
                }
                ucType = GetTaggedValue(uc.TaggedValues, "type").Value;
                string spaceName = null;

                if (ucType == "EntityManagement")
                {
                    spaceName = entityName + "ListIS";
                }
                else if (ucType == "EntityConfiguration")
                {
                    spaceName = entityName + "DetailIS";
                }

                string actionName = "goTo" + spaceName;
                btn = new XisButton(repository, space, diagram, actionName + "Button", actionName);
                btn.SetValue(entityName);
                XisWebHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Navigate,
                    spaceName);
            }
        }

        private static void AssociateBEtoIS(EA.Diagram diagram, XisInteractionSpace source, EA.Element be)
        {
            EA.DiagramObject sourceObj = source.GetDiagramObject(diagram);
            int center = (sourceObj.top + sourceObj.bottom) / -2;

            XisWebHelper.SetPosition(repository, diagram, be, 10, 100, center - 35, center + 35);
            EA.Connector c = source.Element.Connectors.AddNew("", "Association");
            c.ClientID = source.Element.ElementID;
            c.SupplierID = be.ElementID;
            c.Direction = "Source -> Destination";
            c.Stereotype = "XisIS-BEAssociation";
            c.Update();
            source.Element.Update();
            source.Element.Connectors.Refresh();
        }

        #region Getters
        private static List<EA.Attribute> GetFilteredAttributeList(XisEntity entity)
        {
            List<EA.Attribute> lst = new List<EA.Attribute>();

            if (entity.Filter.Contains(';'))
            {
                string[] attrs = entity.Filter.Split(';');
                foreach (string s in attrs)
                {
                    foreach (EA.Attribute attr in entity.Element.Attributes)
                    {
                        if (attr.Name.ToLower() == s.ToLower())
                        {
                            lst.Add(attr);
                        }
                    }
                }
            }
            else
            {
                foreach (EA.Attribute attr in entity.Element.Attributes)
                {
                    if (attr.Name.ToLower() == entity.Filter.ToLower())
                    {
                        lst.Add(attr);
                        break;
                    }
                }
            }
            return lst;
        }

        public static EA.TaggedValue GetTaggedValue(EA.Collection taggedValues, string name)
        {
            foreach (EA.TaggedValue tv in taggedValues)
            {
                if (tv.Name == name)
                {
                    return tv;
                }
            }
            return null;
        }

        public static EA.ConnectorTag GetConnectorTag(EA.Collection taggedValues, string name)
        {
            foreach (EA.ConnectorTag tv in taggedValues)
            {
                if (tv.Name == name)
                {
                    return tv;
                }
            }
            return null;
        }

        public static EA.AttributeTag GetAttributeTag(EA.Collection taggedValues, string name)
        {
            foreach (EA.AttributeTag tv in taggedValues)
            {
                if (tv.Name == name)
                {
                    return tv;
                }
            }
            return null;
        }

        public static EA.MethodTag GetMethodTag(EA.Collection taggedValues, string name)
        {
            foreach (EA.MethodTag tv in taggedValues)
            {
                if (tv.Name == name)
                {
                    return tv;
                }
            }
            return null;
        } 
        #endregion

        #region Containers
        private static bool ContainsCreateMaster(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "CreateMaster").Value);
        }

        private static bool ContainsReadMaster(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "ReadMaster").Value);
        }

        private static bool ContainsUpdateMaster(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "UpdateMaster").Value);
        }

        private static bool ContainsDeleteMaster(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "DeleteMaster").Value);
        }

        private static bool ContainsCreateDetail(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "CreateDetail").Value);
        }

        private static bool ContainsReadDetail(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "ReadDetail").Value);
        }

        private static bool ContainsUpdateDetail(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "UpdateDetail").Value);
        }

        private static bool ContainsDeleteDetail(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "DeleteDetail").Value);
        }

        private static bool ContainsCreateReference(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "CreateReference").Value);
        }

        private static bool ContainsReadReference(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "ReadReference").Value);
        }

        private static bool ContainsUpdateReference(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "UpdateReference").Value);
        }

        private static bool ContainsDeleteReference(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "DeleteReference").Value);
        }

        //private static bool ContainsSearch(string[] operations)
        //{
        //    if (operations != null)
        //    {
        //        foreach (string op in operations)
        //        {
        //            if (op.ToLower() == "s" || op.ToLower() == "search")
        //            {
        //                return true;
        //            }
        //        } 
        //    }
        //    return false;
        //} 
        #endregion

        public enum Mode
        {
            Create,
            View,
            Edit
        }
    }
}
