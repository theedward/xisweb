using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XisWebEAPlugin.InteractionSpace;
using System.Reflection;
using System.IO;
using System.Xml;

namespace XisWebEAPlugin
{
    [ComVisible(true)]
    public class CodeGenerator
    {
        // define menu constants
        private const string menuHeader = "-&XIS-Web Plugin";
        private const string menuValidateModel = "&Validate Model";
        private const string menuGenerateModels = "&Generate Models";
        private const string menuGenerateCode = "&Generate Code";
        private const string menuDeleteGenModels = "&Delete Generated Models";
        public Rules rules;

        public CodeGenerator()
        {
            rules = new Rules();
        }

        ///
        /// Called Before EA starts to check Add-In Exists
        /// Nothing is done here.
        /// This operation needs to exists for the addin to work
        ///
        /// <param name="Repository" />the EA repository
        /// a string
        public String EA_Connect(EA.Repository Repository)
        {
            //No special processing required.
            return "XisWebEAPlugin.CodeGenerator connected";
        }

        ///
        /// Called when user Clicks Add-Ins Menu item from within EA.
        /// Populates the Menu with our desired selections.
        /// Location can be "TreeView" "MainMenu" or "Diagram".
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        ///
        public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {
            switch (MenuName)
            {
                // defines the top level menu option
                case "":
                    return menuHeader;
                // defines the submenu options
                case menuHeader:
                    return new string[] {
                        menuValidateModel,
                        menuGenerateModels,
                        menuGenerateCode,
                        "-",
                        menuDeleteGenModels
                    };
                default:
                    return "";
            }
        }

        ///
        /// returns true if a project is currently opened
        ///
        /// <param name="Repository" />the repository
        /// true if a project is opened in EA
        bool IsProjectOpen(EA.Repository Repository)
        {
            try
            {
                return Repository.Models != null;
            }
            catch
            {
                return false;
            }
        }

        ///
        /// Called once Menu has been opened to see what menu items should active.
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the menu item
        /// <param name="IsEnabled" />boolean indicating whethe the menu item is enabled
        /// <param name="IsChecked" />boolean indicating whether the menu is checked
        public void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            if (IsProjectOpen(Repository))
            {
                switch (ItemName)
                {
                    case menuValidateModel:
                        IsEnabled = true;
                        break;
                    case menuGenerateModels:
                        IsEnabled = true;
                        break;
                    case menuGenerateCode:
                        IsEnabled = true;
                        break;
                    case menuDeleteGenModels:
                        IsEnabled = true;
                        break;
                    default:
                        IsEnabled = false;
                        break;
                }
            }
            else
            {
                // If no open project, disable all menu options
                IsEnabled = false;
            }
        }

        ///
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the selected menu item
        public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                // user has clicked the 'Generate Code' menu option
                case menuValidateModel:
                    this.ValidateModel(Repository);
                    break;
                case menuGenerateModels:
                    this.GenerateModels(Repository);
                    break;
                case menuGenerateCode:
                    this.GenerateCode(Repository);
                    break;
                case menuDeleteGenModels:
                    this.DeleteGenModels(Repository);
                    break;
                default:
                    break;
            }
        }

        #region Rules Region

        public void EA_OnInitializeUserRules(EA.Repository Repository)
        {
            if (Repository != null)
            {
                rules.ConfigureCategories(Repository);
                rules.ConfigureRules(Repository);
                rules.isValid = true;
            }
        }

        public void EA_OnRunPackageRule(EA.Repository Repository, string RuleID, long PackageID)
        {
            rules.RunPackageRule(Repository, RuleID, PackageID);
        }

        public void EA_OnRunDiagramRule(EA.Repository Repository, string RuleID, long DiagramID)
        {
            rules.RunDiagramRule(Repository, RuleID, DiagramID);
        }

        public void EA_OnRunElementRule(EA.Repository Repository, string RuleID, EA.Element Element)
        {
            rules.RunElementRule(Repository, RuleID, Element);
        }

        public void EA_OnRunAttributeRule(EA.Repository Repository, string RuleID, string AttributeGUID, long ObjectID)
        {
            rules.RunAttributeRule(Repository, RuleID, AttributeGUID, ObjectID);
        }

        public void EA_OnRunConnectorRule(EA.Repository Repository, string RuleID, long ConnectorID)
        {
            rules.RunConnectorRule(Repository, RuleID, ConnectorID);
        }

        public void EA_OnRunMethodRule(EA.Repository Repository, string RuleID, string MethodGUID, long ObjectID)
        {
            rules.RunMethodRule(Repository, RuleID, MethodGUID, ObjectID);
        }

        public void EA_OnEndValidation(EA.Repository Repository, object Args)
        {
            if (!rules.isValid)
            {
                MessageBox.Show("Validation ended with errors!",
                    "XIS-Web Rules",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Validation ended successfully!",
                    "XIS-Web Rules",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }
            rules.isValid = true;
        }

        #endregion

        public String EA_OnInitializeTechnologies(EA.Repository Repository)
        {
            string technology = "";
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\XIS-Web MDG";
            string mdgPath = dir + "\\XIS-Web Technology.xml";
            Assembly assem = this.GetType().Assembly;

            try
            {
                string text = File.ReadAllText(mdgPath);

                if (text.Contains("%PATH%"))
                {
                    text = text.Replace("%PATH%", dir + "\\XIS-Web_Template.xml");
                    File.WriteAllText(mdgPath, text);
                }
                technology = text;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error initializing XIS-Web MDG Technology: " + e.Message,
                    "XIS-Web MDG Technology",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop,
                    MessageBoxDefaultButton.Button1);
            }
            return technology;
        }

        private void ValidateModel(EA.Repository Repository)
        {
            EA.Package package = Repository.Models.GetAt(0);
            EA.Project project = Repository.GetProjectInterface();
            project.ValidatePackage(package.PackageGUID);
        }

        private void GenerateModels(EA.Repository Repository)
        {
            EA.Package rootPackage = Repository.Models.GetAt(0).Packages.GetAt(0);
            EA.Package useCaseView = null;
            EA.Package navigationView = null;
            EA.Package interactionView = null;

            foreach (EA.Package package in rootPackage.Packages)
            {
                if (package.StereotypeEx == "InteractionSpace View")
                {
                    interactionView = package;
                }
                else if (package.StereotypeEx == "NavigationSpace View")
                {
                    navigationView = package;
                }
                else if (package.StereotypeEx == "UseCases View")
                {
                    useCaseView = package;
                }
            }

            if (useCaseView != null)
            {
                EA.Element startingUC = null;
                List<EA.Element> useCases = new List<EA.Element>();
                // Process all Use Cases
                foreach (EA.Element element in useCaseView.Elements)
                {
                    if (element.Type == "UseCase"
                        && (element.Stereotype == "XisEntityUseCase" || element.Stereotype == "XisServiceUseCase"))
                    {
                        bool isStartingUseCase = bool.Parse(M2MTransformer.GetTaggedValue(element.TaggedValues, "isStartingUseCase").Value);

                        if (isStartingUseCase)
                        {
                            startingUC = element;
                        }
                        else
                        {
                            EA.Connector conn = null;
                            bool extends = false;

                            for (short i = 0; i < element.Connectors.Count; i++)
                            {
                                conn = element.Connectors.GetAt(i);

                                if (conn.Stereotype == "extend")
                                {
                                    extends = true;
                                    break;
                                }
                            }

                            if (!extends)
                            {
                                useCases.Add(element);
                            }
                        }
                    }
                }

                if (startingUC != null)
                {
                    useCases.Insert(0, startingUC);

                    if (useCases.Count > 0)
                    {
                        if (useCases.Count < 2)
                        {
                            M2MTransformer.ProcessUseCase(Repository, navigationView, interactionView, useCases);
                            MessageBox.Show("Models successfully generated!\r\nCheck the contents of «InteractionSpace View» and «NavigationSpace View».",
                                "XIS-Web Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            new ModelGenerationForm(Repository, navigationView, interactionView, useCases).Show();
                        }
                    }
                }
            }
        }

        private void GenerateCode(EA.Repository Repository)
        {
            new CodeGenerationForm(Repository).Show();
        }

        private void DeleteGenModels(EA.Repository Repository)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Are you sure you want to delete the contents of «InteractionSpace View» and «NavigationSpace View»?",
                "XIS-Web Plugin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (dialogResult == DialogResult.Yes)
            {
                EA.Package rootPackage = Repository.Models.GetAt(0).Packages.GetAt(0);
                EA.Package navigationView = null;
                EA.Package interactionView = null;

                foreach (EA.Package package in rootPackage.Packages)
                {
                    if (package.StereotypeEx == "InteractionSpace View")
                    {
                        interactionView = package;
                    }
                    else if (package.StereotypeEx == "NavigationSpace View")
                    {
                        navigationView = package;
                    }
                }

                if (interactionView != null)
                {
                    if (interactionView.Diagrams.Count > 0)
                    {
                        EA.Collection diagrams = interactionView.Diagrams;

                        for (short i = 0; i < diagrams.Count; i++)
                        {
                            interactionView.Diagrams.Delete(i);
                        }
                    }

                    if (interactionView.Elements.Count > 0)
                    {
                        EA.Collection elements = interactionView.Elements;

                        for (short i = 0; i < elements.Count; i++)
                        {
                            interactionView.Elements.Delete(i);
                        }
                    }

                    Repository.RefreshModelView(interactionView.PackageID);
                }

                if (navigationView != null)
                {
                    if (navigationView.Diagrams.Count > 0)
                    {
                        EA.Collection diagrams = navigationView.Diagrams;

                        for (short i = 0; i < diagrams.Count; i++)
                        {
                            navigationView.Diagrams.Delete(i);
                        }
                    }

                    if (navigationView.Elements.Count > 0)
                    {
                        EA.Collection elements = navigationView.Elements;

                        for (short i = 0; i < elements.Count; i++)
                        {
                            navigationView.Elements.Delete(i);
                        }
                    }

                    Repository.RefreshModelView(navigationView.PackageID);
                }
            }            
        }

        ///
        /// EA calls this operation when it exits. Can be used to do some cleanup work.
        ///
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
