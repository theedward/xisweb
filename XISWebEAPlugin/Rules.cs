using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace XisWebEAPlugin
{
    public class Rules
    {
        private string m_sCategoryID;
        private ArrayList m_RuleIDs;
        private ArrayList m_RuleIDEx;
        public bool isValid;

        // Domain View Rules
        private const string rule01 = "Rule01";
        private const string rule02 = "Rule02";
        private const string rule03 = "Rule03";
        private const string rule04 = "Rule04";
        private const string rule05 = "Rule05";
        private const string rule06 = "Rule06";
        private const string rule07 = "Rule07";
        private const string rule08 = "Rule08";
        private const string rule09 = "Rule09";
        private const string rule10 = "Rule10";
        private const string rule11 = "Rule11";
        private const string rule12 = "Rule12";
        // BusinessEntities View Rules
        private const string rule13 = "Rule13";
        private const string rule14 = "Rule14";
        private const string rule15 = "Rule15";
        private const string rule16 = "Rule16";
        // UseCases View Rules
        private const string rule17 = "Rule17";
        private const string rule18 = "Rule18";
        private const string rule19 = "Rule19";
        private const string rule20 = "Rule20";
        private const string rule21 = "Rule21";
        private const string rule22 = "Rule22";
        // Architectural View Rules
        private const string rule23 = "Rule23";
        private const string rule24 = "Rule24";
        private const string rule25 = "Rule25";
        private const string rule26 = "Rule26";
        private const string rule27 = "Rule27";
        private const string rule28 = "Rule28";
        private const string rule29 = "Rule29";
        // NavigationSpace View Rules
        private const string rule30 = "Rule30";
        // InteractionSpace View Rules
        private const string rule31 = "Rule31";
        private const string rule32 = "Rule32";
        private const string rule33 = "Rule33";
        private const string rule34 = "Rule34";
        private const string rule35 = "Rule35";
        private const string rule36 = "Rule36";
        private const string rule37 = "Rule37";
        private const string rule38 = "Rule38";
        private const string rule39 = "Rule39";
        private const string rule40 = "Rule40";
        private const string rule41 = "Rule41";
        private const string rule42 = "Rule42";
        private const string rule43 = "Rule43";
        private const string rule44 = "Rule44";
        private const string rule45 = "Rule45";
        private const string rule46 = "Rule46";
        private const string rule47 = "Rule47";
        private const string rule48 = "Rule48";
        private const string rule49 = "Rule49";
        private const string rule50 = "Rule50";
        private const string rule51 = "Rule51";
        private const string rule52 = "Rule52";
        private const string rule53 = "Rule53";
        private const string rule54 = "Rule54";
        private const string rule55 = "Rule55";
        private const string rule56 = "Rule56";
        private const string rule57 = "Rule57";
        private const string rule58 = "Rule58";
        private const string rule59 = "Rule59";
        private const string rule60 = "Rule60";
        private const string rule61 = "Rule61";
        private const string rule62 = "Rule62";
        private const string rule63 = "Rule63";
        private const string rule64 = "Rule64";
        private const string rule65 = "Rule65";
        private const string rule66 = "Rule66";
        private const string rule67 = "Rule67";
        private const string rule68 = "Rule68";
        private const string rule69 = "Rule69";
        private const string rule70 = "Rule70";
        private const string rule71 = "Rule71";
        private const string rule72 = "Rule72";
        private const string rule73 = "Rule73";
        private const string rule74 = "Rule74";
        private const string rule75 = "Rule75";
        private const string rule76 = "Rule76";
        private const string rule77 = "Rule77";
        private const string rule78 = "Rule78";
        private const string rule79 = "Rule79";
        private const string rule80 = "Rule80";
        private const string rule81 = "Rule81";
        private const string rule82 = "Rule82";
        private const string rule83 = "Rule83";
        private const string rule84 = "Rule84";
        private const string rule85 = "Rule85";
        private const string rule86 = "Rule86";
        private const string rule87 = "Rule87";
        private const string rule88 = "Rule88";
        private const string rule89 = "Rule89";
        private const string rule90 = "Rule90";
        private const string rule91 = "Rule91";
        private const string rule92 = "Rule92";
        private const string rule93 = "Rule93";
        private const string rule94 = "Rule94";
        private const string rule95 = "Rule95";
        private const string rule96 = "Rule96";
        private const string rule97 = "Rule97";
        private const string rule98 = "Rule98";
        private const string rule99 = "Rule99";
        private const string rule100 = "Rule100";
        private const string rule101 = "Rule101";
        private const string rule102 = "Rule102";
        private const string rule103 = "Rule103";
        private const string rule104 = "Rule104";
        private const string rule105 = "Rule105";
        private const string rule106 = "Rule106";
        private const string rule107 = "Rule107";
        private const string rule108 = "Rule108";
        private const string rule109 = "Rule109";
        private const string rule110 = "Rule110";
        private const string rule111 = "Rule111";

        public Rules()
        {
            m_RuleIDs = new ArrayList();
            m_RuleIDEx = new ArrayList();
            isValid = false;
        }

        private string LookupMap(string sKey)
        {
            return DoLookupMap(sKey, m_RuleIDs, m_RuleIDEx);
        }

        private string LookupMapEx(string sRule)
        {
            return DoLookupMap(sRule, m_RuleIDEx, m_RuleIDs);
        }

        private string DoLookupMap(string sKey, ArrayList arrValues, ArrayList arrKeys)
        {
            if (arrKeys.Contains(sKey))
                return arrValues[arrKeys.IndexOf(sKey)].ToString();
            else
                return "";
        }

        private void AddToMap(string sRuleID, string sKey)
        {
            m_RuleIDs.Add(sRuleID);
            m_RuleIDEx.Add(sKey);
        }

        private string GetRuleStr(string sRuleID)
        {
            switch (sRuleID)
            {
                case rule01:
                    return "The model must be composed of at least 3 views (BusinessEntities, Domain and UseCases views)!";
                case rule02:
                    return "The model must be composed of at least 4 views (Architectural, BusinessEntities, Domain and UseCases views)!";
                case rule03:
                    return "The model must be composed of at least 4 views (BusinessEntities, Domain, InteractionSpace and NavigationSpace views)!";
                case rule04:
                    return "The model must be composed of 5 views (Architectural, BusinessEntities, Domain, InteractionSpace and NavigationSpace views)!";
                case rule05:
                    return "A XisEntity must have at least 1 XisEntityAttribute!";
                case rule06:
                    return "A XisEntity must only have attributes with stereotype «XisEntityAttribute»!";
                case rule07:
                    return "XisEntities must be connected only by XisEntityAssociation or XisEntityInheritance!";
                case rule08:
                    return "A XisEntityAssociation must only connect XisEntities!";
                case rule09:
                    return "A XisEntityInheritance must only connect XisEntities!";
                case rule10:
                    return "A XisEnumeration must have at least 1 XisEnumerationValue!";
                case rule11:
                    return "A XisEnumeration must only have attributes with stereotype «XisEnumerationValue»!";
                case rule12:
                    return "A XisEntityAttribute must have a valid type!";
                case rule13:
                    return "A XisBusinessEntity must be connected to a XisEntity by a XisBE-EntityMasterAssociation, XisBE-EntityDetailAssociation or XisBE-EntityReferenceAssociation!";
                case rule14:
                    return "A XisBE-EntityMasterAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule15:
                    return "A XisBE-EntityDetailAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule16:
                    return "A XisBE-EntityReferenceAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule17:
                    return "XisBusinessEntities must have 1 XisBE-EntityMasterAssociation!";
                case rule18:
                    return "There must be 1 starting XisUseCase!";
                case rule19:
                    return "XisEntityUseCases must be connected to XisBusinessEntities by XisEntityUC-BEAssociations!";
                case rule20:
                    return "XisServiceUseCases must be connected to XisBusinessEntities and/or XisProviders by XisServiceUC-BEAssociations and XisServiceUC-ProviderAssociations, respectively!";
                case rule21:
                    return "A XisEntityUC-BEAssociation must connect a XisEntityUseCase (source) to a XisBusinessEntity (target)!";
                case rule22:
                    return "A XisServiceUC-BEAssociation must connect a XisServiceUseCase (source) to a XisBusinessEntity (target)!";
                case rule23:
                    return "A XisServiceUC-ProviderAssociation must connect a XisServiceUseCase (source) to a XisProvider (target)!";
                case rule24:
                    return "XisWebApp must be connected to XisServices by XisWebApp-ServiceAssociations!";
                case rule25:
                    return "A XisWebApp-ServiceAssociation must connect XisWebApp (source) to a XisService (target)!";
                case rule26:
                    return "A XisService must have at least 1 XisServiceMethod!";
                case rule27:
                    return "A XisService must only have methods with stereotype «XisServiceMethod»!";
                case rule28:
                    return "A XisInternalProvider must realize a XisInternalService!";
                case rule29:
                    return "A XisServer must realize a XisRemoteService!";
                case rule30:
                    return "A XisClientWebApp must realize a XisRemoteService!";
                case rule31:
                    return "A XisInteractionSpaceAssociation must only connect XisInteractionSpaces!";
                case rule32:
                    return "There must be 1 XisInteractionSpace that is the main screen!";
                case rule33:
                    return "A XisInteractionSpace must have at least 1 XisWidget!";
                case rule34:
                    return "All XisInteractionSpace elements must be XisWidgets!";
                case rule35:
                    return "A XisIS-BEAssociation must connect a XisInteractionSpace (source) to a XisBusinessEntity (target)!";
                case rule36:
                    return "A XisIS-MenuAssociation must connect a XisInteractionSpace (source) to a XisMenu (target)!";
                case rule37:
                    return "A XisIS-DialogAssociation must connect a XisInteractionSpace (source) to a XisDialog (target)!";
                case rule38:
                    return "A XisGesture must have 1 XisAction!";
                case rule39:
                    return "A XisWidget-GestureAssociation must connect a XisWidget (source) to a XisGesture (target)!";
                case rule40:
                    return "A XisList must contain 1 XisListItem or XisListGroup!";
                case rule41:
                    return "A XisList can only contain XisListItems or XisListGroups!";
                case rule42:
                    return "A XisListGroup must contain 1 XisListItem!";
                case rule43:
                    return "A XisListGroup can only contain 1 XisListItem!";
                case rule44:
                    return "A XisListItem can only contain XisWidgets!";
                case rule45:
                    return "A XisMenu must contain at least 1 XisMenuItem or XisMenuGroup!";
                case rule46:
                    return "A XisMenu can only contain XisMenuItems, XisMenuGroups or XisVisibilityBoundaries!";
                case rule47:
                    return "A XisMenuGroup must contain at least 1 XisMenuItem!";
                case rule48:
                    return "A XisMenuGroup can only contain XisMenuItems!";
                case rule49:
                    return "A XisMenuItem cannot contain other elements!";
                case rule50:
                    return "A XisMenu of type 'OptionsMenu' must be associated to a XisInteractionSpace!";
                case rule51:
                    return "A XisButton with a XisAction must have a corresponding 'onTap' value!";
                case rule52:
                    return "A XisButton with 'onTap' value filled must have a corresponding XisAction!";
                case rule53:
                    return "A XisButton can only have 1 XisAction!";
                case rule54:
                    return "A XisLink with a XisAction must have a corresponding 'onTap' value!";
                case rule55:
                    return "A XisLink with 'onTap' value filled must have a corresponding XisAction!";
                case rule56:
                    return "A XisLink can only have 1 XisAction!";
                case rule57:
                    return "A XisMenuItem with a XisAction must have a corresponding 'onTap' value!";
                case rule58:
                    return "A XisMenuItem with 'onTap' value filled must have a corresponding XisAction!";
                case rule59:
                    return "A XisMenuItem can only have at most 1 XisAction!";
                case rule60:
                    return "A XisListItem with a XisAction must have a corresponding 'onTap' or 'onLongTap' value!";
                case rule61:
                    return "A XisListItem with 'onTap' value filled must have a corresponding XisAction!";
                case rule62:
                    return "A XisListItem with 'onLongTap' value filled must have a corresponding XisAction!";
                case rule63:
                    return "A XisListItem can only have at most 2 XisActions!";
                case rule64:
                    return "A XisDialog can only contain XisButtons (up to 3)!";
                case rule65:
                    return "A XisMapView can only contain XisMapMarkers!";
                case rule66:
                    return "A XisLabel cannot contain other elements!";
                case rule67:
                    return "A XisTextBox cannot contain other elements!";
                case rule68:
                    return "A XisCheckBox cannot contain other elements!";
                case rule69:
                    return "A XisButton cannot contain other elements!";
                case rule70:
                    return "A XisLink cannot contain other elements!";
                case rule71:
                    return "A XisImage cannot contain other elements!";
                case rule72:
                    return "A XisDatePicker cannot contain other elements!";
                case rule73:
                    return "A XisTimePicker cannot contain other elements!";
                case rule74:
                    return "A XisWebView cannot contain other elements!";
                case rule75:
                    return "A XisDropdown cannot contain other elements!";
                case rule76:
                    return "A XisAction must be owned only by a XisGesture, XisListItem, XisButton, XisLink or XisMenuItem!";
                case rule77:
                    return "A XisAction must have the 'type' value filled!";
                case rule78:
                    return "A XisAction can only have the 'navigation' value filled with a XisInteractionSpace name!";
                case rule79:
                    return "Since it has no entity context, this XisAction of type 'Create' must have a parameter named 'entityName' with a default value equals to a XisEntity!";
                case rule80:
                    return "Since it has no entity context, this XisAction of type 'Read' must have a parameter named 'entityName' with a default value equals to a XisEntity!";
                case rule81:
                    return "Since it has no entity context, this XisAction of type 'Update' must have a parameter named 'entityName' with a default value equals to a XisEntity!";
                case rule82:
                    return "Since it has no entity context, this XisAction of type 'Delete' must have a parameter named 'entityName' with a default value equals to a XisEntity!";
                case rule83:
                    return "Since it has no entity context, this XisAction of type 'DeleteAll' must have a parameter named 'entityName' with a default value equals to a XisEntity!";
                case rule84:
                    return "A XisAction of type 'OpenBrowser' must have a parameter named 'url' with a default value!";
                case rule85:
                    return "A XisAction of type 'WebService' must have a name in the format <XisService.name>.<XisServiceMethod.name>!";
                case rule86:
                    return "A XisAction of type 'WebService' must have the same parameters of the associated XisServiceMethod!";
                case rule87:
                    return "A XisAction of type 'Navigate' must have the 'navigation' value filled with a XisInteractionSpace name!";
                case rule88:
                    return "A XisForm must have the 'entityName' value filled!";
                case rule89:
                    return "A XisForm must have the 'entityName' value filled with a valid XisEntity name!";
                case rule90:
                    return "A XisList must have the 'entityName' value filled with a valid XisEntity name!";
                case rule91:
                    return "A XisListGroup can only have the 'entityName' value filled with a valid XisEntity name!";
                case rule92:
                    return "A XisVisibilityBoundary must have the 'entityName' value filled with a valid XisEntity name!";
                case rule93:
                    return "A XisMenu can only have the 'entityName' value filled with a valid XisEntity name!";
                case rule94:
                    return "A XisList can only have the 'searchBy' value filled in the format <XisEntity.name>.<XisEntityAttribute.name>!";
                case rule95:
                    return "A XisList can only have the 'orderBy' value filled in the format <XisEntity.name>.<XisEntityAttribute.name>!";
                case rule96:
                    return "A XisList can only have the 'searchBy' value filled with a valid XisEntityAttribute name!";
                case rule97:
                    return "A XisList can only have the 'orderBy' value filled with a valid XisEntityAttribute name!";
                case rule98:
                    return "A XisLabel must have a value assigned!";
                case rule99:
                    return "A XisTextBox must have a value assigned!";
                case rule100:
                    return "A XisCheckBox must have a value assigned!";
                case rule101:
                    return "A XisButton must have a value assigned!";
                case rule102:
                    return "A XisLink must have a value assigned!";
                case rule103:
                    return "A XisDatePicker must have a value assigned!";
                case rule104:
                    return "A XisTimePicker must have a value assigned!";
                case rule105:
                    return "A XisDropdown must have a value assigned!";
                case rule106:
                    return "An Architectural View package can only contain a Architectural View Diagram!";
                case rule107:
                    return "A Domain View package can only contain a Domain View Diagram!";
                case rule108:
                    return "A BusinessEntities View package can only contain a BusinessEntities View Diagram!";
                case rule109:
                    return "A UseCases View package can only contain a UseCases View Diagram!";
                case rule110:
                    return "An InteractionSpace View package can only contain InteractionSpace View Diagrams!";
                case rule111:
                    return "A NavigationSpace View package can only contain a NavigationSpace View Diagram!";
                // TODO: Add Gesture validation rules
                //case rule08:
                //    return "All XisActions parameters must be XisArguments!";
                default:
                    return "";
            }
        }

        public void ConfigureCategories(EA.Repository Repository)
        {
            m_sCategoryID = Repository.GetProjectInterface().DefineRuleCategory("XIS-Web Rules");
        }

        public void ConfigureRules(EA.Repository Repository)
        {
            EA.Project Project = Repository.GetProjectInterface();
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule01)), rule01);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule05)), rule05);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule06)), rule06);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule07)), rule07);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule08)), rule08);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule09)), rule09);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule10)), rule10);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule11)), rule11);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule12)), rule12);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule13)), rule13);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule14)), rule14);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule15)), rule15);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule16)), rule16);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule17)), rule17);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule18)), rule18);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule19)), rule19);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule20)), rule20);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule21)), rule21);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule22)), rule22);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule23)), rule23);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule24)), rule24);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule25)), rule25);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule26)), rule26);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule27)), rule27);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule28)), rule28);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule29)), rule29);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule30)), rule30);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule31)), rule31);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule32)), rule32);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule33)), rule33);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule34)), rule34);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule35)), rule35);
            //AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule36)), rule36);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule37)), rule37);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule38)), rule38);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule39)), rule39);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule40)), rule40);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule41)), rule41);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule42)), rule42);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule43)), rule43);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule44)), rule44);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule45)), rule45);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule46)), rule46);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule47)), rule47);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule48)), rule48);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule49)), rule49);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule50)), rule50);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule51)), rule51);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule52)), rule52);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule53)), rule53);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule54)), rule54);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule55)), rule55);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule56)), rule56);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule57)), rule57);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule58)), rule58);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule59)), rule59);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule60)), rule60);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule61)), rule61);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule62)), rule62);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule63)), rule63);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule64)), rule64);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule65)), rule65);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule66)), rule66);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule67)), rule67);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule68)), rule68);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule69)), rule69);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule70)), rule70);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule71)), rule71);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule72)), rule72);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule73)), rule73);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule74)), rule74);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule75)), rule75);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule76)), rule76);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule77)), rule77);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule78)), rule78);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule79)), rule79);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule80)), rule80);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule81)), rule81);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule82)), rule82);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule83)), rule83);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule84)), rule84);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule85)), rule85);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule86)), rule86);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule87)), rule87);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule88)), rule88);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule89)), rule89);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule90)), rule90);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule91)), rule91);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule92)), rule92);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule93)), rule93);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule94)), rule94);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule95)), rule95);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule96)), rule96);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule97)), rule97);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule98)), rule98);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule99)), rule99);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule100)), rule100);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule101)), rule101);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule102)), rule102);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule103)), rule103);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule104)), rule104);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule105)), rule105);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule106)), rule106);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule107)), rule107);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule108)), rule108);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule109)), rule109);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule110)), rule110);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule111)), rule111);
            // TODO: expand this list
        }

        public void RunPackageRule(EA.Repository Repository, string sRuleID, long PackageID)
        {
            EA.Package Package = Repository.GetPackageByID((int)PackageID);
            if (Package != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule01:
                        DoRule01_to_04(Repository, Package);
                        break;
                    case rule18:
                        DoRule18(Repository, Package);
                        break;
                    case rule32:
                        DoRule32(Repository, Package);
                        break;
                    case rule106:
                        DoRule106(Repository, Package);
                        break;
                    case rule107:
                        DoRule107(Repository, Package);
                        break;
                    case rule108:
                        DoRule108(Repository, Package);
                        break;
                    case rule109:
                        DoRule109(Repository, Package);
                        break;
                    case rule110:
                        DoRule110(Repository, Package);
                        break;
                    case rule111:
                        DoRule111(Repository, Package);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunDiagramRule(EA.Repository Repository, string sRuleID, long lDiagramID)
        {
            EA.Diagram Diagram = Repository.GetDiagramByID((int)lDiagramID);
            if (Diagram != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    //case rule05:
                    //    DoRule05(Repository, lDiagramID);
                    //    break;
                    default:
                        break;
                }
            }
        }

        public void RunElementRule(EA.Repository Repository, string sRuleID, EA.Element Element)
        {
            if (Element != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule05:
                        DoRule05(Repository, Element);
                        break;
                    case rule06:
                        DoRule06(Repository, Element);
                        break;
                    case rule10:
                        DoRule10(Repository, Element);
                        break;
                    case rule11:
                        DoRule11(Repository, Element);
                        break;
                    case rule13:
                        DoRule13(Repository, Element);
                        break;
                    case rule17:
                        DoRule17(Repository, Element);
                        break;
                    case rule19:
                        DoRule19(Repository, Element);
                        break;
                    case rule20:
                        DoRule20(Repository, Element);
                        break;
                    case rule24:
                        DoRule24(Repository, Element);
                        break;
                    case rule26:
                        DoRule26(Repository, Element);
                        break;
                    case rule27:
                        DoRule27(Repository, Element);
                        break;
                    case rule28:
                        DoRule28(Repository, Element);
                        break;
                    case rule29:
                        DoRule29(Repository, Element);
                        break;
                    case rule33:
                        DoRule33(Repository, Element);
                        break;
                    case rule34:
                        //DoRule34(Repository, Element);
                        break;
                    case rule38:
                        DoRule38(Repository, Element);
                        break;
                    case rule40:
                        DoRule40(Repository, Element);
                        break;
                    case rule41:
                        DoRule41(Repository, Element);
                        break;
                    case rule42:
                        DoRule42(Repository, Element);
                        break;
                    case rule43:
                        DoRule43(Repository, Element);
                        break;
                    case rule44:
                        DoRule44(Repository, Element);
                        break;
                    case rule45:
                        DoRule45(Repository, Element);
                        break;
                    case rule46:
                        DoRule46(Repository, Element);
                        break;
                    case rule47:
                        DoRule47(Repository, Element);
                        break;
                    case rule48:
                        DoRule48(Repository, Element);
                        break;
                    case rule49:
                        DoRule49(Repository, Element);
                        break;
                    case rule50:
                        //DoRule50(Repository, Element);
                        break;
                    case rule51:
                        DoRule51_54_57(Repository, Element, "XisButton");
                        break;
                    case rule52:
                        DoRule52_55_58(Repository, Element, "XisButton");
                        break;
                    case rule53:
                        DoRule53_56_59(Repository, Element, "XisButton");
                        break;
                    case rule54:
                        DoRule51_54_57(Repository, Element, "XisLink");
                        break;
                    case rule55:
                        DoRule52_55_58(Repository, Element, "XisLink");
                        break;
                    case rule56:
                        DoRule53_56_59(Repository, Element, "XisLink");
                        break;
                    case rule57:
                        DoRule51_54_57(Repository, Element, "XisMenuItem");
                        break;
                    case rule58:
                        DoRule52_55_58(Repository, Element, "XisMenuItem");
                        break;
                    case rule59:
                        DoRule53_56_59(Repository, Element, "XisMenuItem");
                        break;
                    case rule60:
                        DoRule60(Repository, Element);
                        break;
                    case rule61:
                        DoRule61(Repository, Element);
                        break;
                    case rule62:
                        DoRule62(Repository, Element);
                        break;
                    case rule63:
                        DoRule63(Repository, Element);
                        break;
                    case rule64:
                        DoRule64(Repository, Element);
                        break;
                    case rule65:
                        DoRule65(Repository, Element);
                        break;
                    case rule66:
                        DoRule66_to_75(Repository, Element, "XisLabel");
                        break;
                    case rule67:
                        DoRule66_to_75(Repository, Element, "XisTextBox");
                        break;
                    case rule68:
                        DoRule66_to_75(Repository, Element, "XisTextBox");
                        break;
                    case rule69:
                        DoRule66_to_75(Repository, Element, "XisButton");
                        break;
                    case rule70:
                        DoRule66_to_75(Repository, Element, "XisLink");
                        break;
                    case rule71:
                        DoRule66_to_75(Repository, Element, "XisImage");
                        break;
                    case rule72:
                        DoRule66_to_75(Repository, Element, "XisDatePicker");
                        break;
                    case rule73:
                        DoRule66_to_75(Repository, Element, "XisTimePicker");
                        break;
                    case rule74:
                        DoRule66_to_75(Repository, Element, "XisWebView");
                        break;
                    case rule75:
                        DoRule66_to_75(Repository, Element, "XisDropdown");
                        break;
                    case rule88:
                        DoRule88(Repository, Element);
                        break;
                    case rule89:
                        DoRule89_to_93(Repository, Element, "XisForm");
                        break;
                    case rule90:
                        DoRule89_to_93(Repository, Element, "XisList");
                        break;
                    case rule91:
                        DoRule89_to_93(Repository, Element, "XisListGroup");
                        break;
                    case rule92:
                        DoRule89_to_93(Repository, Element, "XisVisibilityBoundary");
                        break;
                    case rule93:
                        DoRule89_to_93(Repository, Element, "XisMenu");
                        break;
                    case rule94:
                        DoRule94_95(Repository, Element, "searchBy");
                        break;
                    case rule95:
                        DoRule94_95(Repository, Element, "orderBy");
                        break;
                    case rule96:
                        DoRule96_97(Repository, Element, "searchBy");
                        break;
                    case rule97:
                        DoRule96_97(Repository, Element, "orderBy");
                        break;
                    case rule98:
                        DoRule98_to_105(Repository, Element, "XisLabel");
                        break;
                    case rule99:
                        DoRule98_to_105(Repository, Element, "XisTextBox");
                        break;
                    case rule100:
                        DoRule98_to_105(Repository, Element, "XisCheckBox");
                        break;
                    case rule101:
                        DoRule98_to_105(Repository, Element, "XisButton");
                        break;
                    case rule102:
                        DoRule98_to_105(Repository, Element, "XisLink");
                        break;
                    case rule103:
                        DoRule98_to_105(Repository, Element, "XisDatePicker");
                        break;
                    case rule104:
                        DoRule98_to_105(Repository, Element, "XisTimePicker");
                        break;
                    case rule105:
                        DoRule98_to_105(Repository, Element, "XisDropdown");
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunAttributeRule(EA.Repository Repository, string sRuleID, string AttributeGUID, long ObjectID)
        {
            EA.Attribute Attribute = Repository.GetAttributeByGuid(AttributeGUID);
            if (Attribute != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule12:
                        DoRule12(Repository, Attribute);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunConnectorRule(EA.Repository Repository, string sRuleID, long lConnectorID)
        {
            EA.Connector Connector = Repository.GetConnectorByID((int)lConnectorID);
            if (Connector != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule07:
                        DoRule07(Repository, Connector);
                        break;
                    case rule08:
                        DoRule08(Repository, Connector);
                        break;
                    case rule09:
                        DoRule09(Repository, Connector);
                        break;
                    case rule15:
                        DoRule15(Repository, Connector);
                        break;
                    case rule16:
                        DoRule16(Repository, Connector);
                        break;
                    case rule21:
                        DoRule21(Repository, Connector);
                        break;
                    case rule22:
                        DoRule22(Repository, Connector);
                        break;
                    case rule23:
                        DoRule23(Repository, Connector);
                        break;
                    case rule25:
                        DoRule25(Repository, Connector);
                        break;
                    case rule31:
                        DoRule31(Repository, Connector);
                        break;
                    case rule35:
                        DoRule35(Repository, Connector);
                        break;
                    case rule36:
                        DoRule36(Repository, Connector);
                        break;
                    case rule37:
                        DoRule37(Repository, Connector);
                        break;
                    case rule39:
                        DoRule39(Repository, Connector);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunMethodRule(EA.Repository Repository, string sRuleID, string MethodGUID, long ObjectID)
        {
            EA.Method method = Repository.GetMethodByGuid(MethodGUID);
            if (method != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule76:
                        DoRule76(Repository, method);
                        break;
                    case rule77:
                        DoRule77(Repository, method);
                        break;
                    case rule78:
                        DoRule78(Repository, method);
                        break;
                    case rule79:
                        DoRule79_to_83(Repository, method, "Create");
                        break;
                    case rule80:
                        DoRule79_to_83(Repository, method, "Read");
                        break;
                    case rule81:
                        DoRule79_to_83(Repository, method, "Update");
                        break;
                    case rule82:
                        DoRule79_to_83(Repository, method, "Delete");
                        break;
                    case rule83:
                        DoRule79_to_83(Repository, method, "DeleteAll");
                        break;
                    case rule84:
                        DoRule84(Repository, method);
                        break;
                    case rule85:
                        DoRule85(Repository, method);
                        break;
                    case rule86:
                        DoRule86(Repository, method);
                        break;
                    case rule87:
                        DoRule87(Repository, method);
                        break;
                    default:
                        break;
                }
            }
        }

        // Validate number of views
        private void DoRule01_to_04(EA.Repository Repository, EA.Package Package)
        {
            EA.Project Project = Repository.GetProjectInterface();
            EA.Package model = (EA.Package)Repository.Models.GetAt(0);
            EA.Package view = (EA.Package)model.Packages.GetAt(0);

            if (view.PackageID == Package.PackageID)
            {
                if (view.Packages.Count < 3)
                {
                    Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule01));
                    isValid = false;
                }
                else if (view.Packages.Count == 3)
                {
                    EA.Package p = null;
                    bool arch = false;
                    Dictionary<string, int> smart = new Dictionary<string, int>(3);

                    for (short i = 0; i < view.Packages.Count; i++)
                    {
                        p = view.Packages.GetAt(i);

                        if (p.StereotypeEx == "Architectural View")
                        {
                            arch = true;
                            break;
                        }
                        else if (p.StereotypeEx == "BusinessEntities View" || p.StereotypeEx == "Domain View"
                                 || p.StereotypeEx == "UseCases View")
                        {
                            if (smart.ContainsKey(p.StereotypeEx))
                            {
                                smart[p.StereotypeEx] += 1;
                            }
                            else
                            {
                                smart.Add(p.StereotypeEx, 1);
                            }
                        }
                    }

                    if (arch)
                    {
                        Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule02));
                        isValid = false;
                    }
                    else
                    {
                        if (!smart.ContainsKey("BusinessEntities View") || !smart.ContainsKey("Domain View")
                            || !smart.ContainsKey("UseCases View")
                            || smart["BusinessEntities View"] != 1 || smart["Domain View"] != 1 || smart["UseCases View"] != 1)
                        {
                            Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule01));
                            isValid = false;
                        }
                    }
                }
                else
                {
                    EA.Package p = null;
                    bool arch = false;
                    Dictionary<string, int> dummy = new Dictionary<string, int>(5);

                    for (short i = 0; i < view.Packages.Count; i++)
                    {
                        p = view.Packages.GetAt(i);

                        if (p.StereotypeEx == "Architectural View")
                        {
                            arch = true;
                            if (dummy.ContainsKey(p.StereotypeEx))
                            {
                                dummy[p.StereotypeEx] += 1;
                            }
                            else
                            {
                                dummy.Add(p.StereotypeEx, 1);
                            }
                        }
                        else if (p.StereotypeEx == "BusinessEntities View" || p.StereotypeEx == "Domain View"
                                 || p.StereotypeEx == "InteractionSpace View" || p.StereotypeEx == "NavigationSpace View")
                        {
                            if (dummy.ContainsKey(p.StereotypeEx))
                            {
                                dummy[p.StereotypeEx] += 1;
                            }
                            else
                            {
                                dummy.Add(p.StereotypeEx, 1);
                            }
                        }
                    }

                    if (arch && (!dummy.ContainsKey("BusinessEntities View")
                        || !dummy.ContainsKey("Domain View") || !dummy.ContainsKey("InteractionSpace View")
                        || !dummy.ContainsKey("NavigationSpace View") || dummy["Architectural View"] != 1
                        || dummy["BusinessEntities View"] != 1 || dummy["Domain View"] != 1
                        || dummy["InteractionSpace View"] != 1 || dummy["NavigationSpace View"] != 1))
                    {
                        Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule04));
                        isValid = false;
                    }
                    else if (!dummy.ContainsKey("BusinessEntities View") || !dummy.ContainsKey("Domain View")
                            || !dummy.ContainsKey("InteractionSpace View") || !dummy.ContainsKey("NavigationSpace View")
                            || dummy["BusinessEntities View"] != 1 || dummy["Domain View"] != 1
                            || dummy["InteractionSpace View"] != 1 || dummy["NavigationSpace View"] != 1)
                    {
                        Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule03));
                        isValid = false;
                    }
                }
            }
        }

        // XisEntities with XisEntityAttributes
        private void DoRule05(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisEntity")
            {
                if (Element.Attributes.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule05), EA.EnumMVErrorType.mvError, GetRuleStr(rule05));
                    isValid = false;
                }
            }
        }

        private void DoRule06(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisEntity")
            {
                if (Element.Attributes.Count > 0)
                {
                    EA.Attribute attr = null;

                    for (short i = 0; i < Element.Attributes.Count; i++)
                    {
                        attr = Element.Attributes.GetAt(i);

                        if (attr.Stereotype != "XisEntityAttribute")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule06), EA.EnumMVErrorType.mvError, GetRuleStr(rule06));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule07(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Project Project = Repository.GetProjectInterface();
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype != "XisEntityAssociation" && Connector.Stereotype != "XisEntityInheritance"
                && client.Stereotype == "XisEntity" && supplier.Stereotype == "XisEntity")
            {
                Project.PublishResult(LookupMap(rule07), EA.EnumMVErrorType.mvError, GetRuleStr(rule07));
                isValid = false;
            }
        }

        private void DoRule08(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Project Project = Repository.GetProjectInterface();
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisEntityAssociation"
                && (client.Stereotype != "XisEntity" || supplier.Stereotype != "XisEntity"))
            {
                Project.PublishResult(LookupMap(rule08), EA.EnumMVErrorType.mvError, GetRuleStr(rule08));
                isValid = false;
            }
        }

        private void DoRule09(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Project Project = Repository.GetProjectInterface();
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisEntityInheritance"
                && (client.Stereotype != "XisEntity" || supplier.Stereotype != "XisEntity"))
            {
                Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09));
                isValid = false;
            }
        }

        private void DoRule10(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisEnumeration" && Element.Attributes.Count == 0)
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule10), EA.EnumMVErrorType.mvError, GetRuleStr(rule10));
                isValid = false;
            }
        }

        private void DoRule11(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisEnumeration" && Element.Attributes.Count > 0)
            {
                EA.Attribute attr = null;
                for (short i = 0; i < Element.Attributes.Count; i++)
                {
                    attr = Element.Attributes.GetAt(i);

                    if (attr.Stereotype != "XisEnumerationValue")
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule11), EA.EnumMVErrorType.mvError, GetRuleStr(rule11));
                        isValid = false;
                        break;
                    }
                }
            }
        }

        private void DoRule12(EA.Repository Repository, EA.Attribute Attribute)
        {
            if (Attribute.Stereotype == "XisEntityAttribute")
            {
                if (string.IsNullOrEmpty(Attribute.Type))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule12), EA.EnumMVErrorType.mvError, GetRuleStr(rule12));
                    isValid = false;
                }
                else
                {
                    bool primitive = false;
                    switch (Attribute.Type.ToLower())
                    {
                        case "int":
                        case "integer":
                        case "double":
                        case "float":
                        case "long":
                        case "short":
                        case "char":
                        case "string":
                        case "bool":
                        case "boolean":
                        case "byte":
                        case "date":
                        case "time":
                        case "image":
                        case "url":
                            primitive = true;
                            break;
                        default:
                            break;
                    }

                    if (!primitive)
                    {
                        EA.Package package = Repository.GetPackageByID(Repository.GetElementByID(Attribute.ParentID).PackageID);
                        EA.Element el = null;
                        bool exists = false;

                        for (short i = 0; i < package.Elements.Count; i++)
                        {
                            el = package.Elements.GetAt(i);

                            if (el.Name == Attribute.Type && (el.Stereotype == "XisEntity" || el.Stereotype == "XisEnumeration"))
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule12), EA.EnumMVErrorType.mvError, GetRuleStr(rule12));
                            isValid = false;
                        }
                    }
                }
            }
        }

        private void DoRule13(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisBusinessEntity")
            {
                if (Element.Connectors.Count > 0)
                {
                    bool hasAssociation = false;
                    EA.Connector conn = null;
                    EA.Element supplier = null;

                    for (short i = 0; i < Element.Connectors.Count; i++)
                    {
                        conn = Element.Connectors.GetAt(i);
                        supplier = Repository.GetElementByID(conn.SupplierID);

                        if ((conn.Stereotype == "XisBE-EntityMasterAssociation"
                            || conn.Stereotype == "XisBE-EntityDetailAssociation"
                            || conn.Stereotype == "XisBE-EntityReferenceAssociation")
                            && supplier.Stereotype == "XisEntity")
                        {
                            hasAssociation = true;
                            break;
                        }
                    }

                    if (!hasAssociation)
	                {
		                EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule13), EA.EnumMVErrorType.mvError, GetRuleStr(rule13));
                        isValid = false;
	                }
                }
                else
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule13), EA.EnumMVErrorType.mvError, GetRuleStr(rule13));
                    isValid = false;
                }
            }
        }

        private void DoRule14(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisBE-EntityMasterAssociation"
                && (client.Stereotype != "XisBusinessEntity" || supplier.Stereotype != "XisEntity"))
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule14), EA.EnumMVErrorType.mvError, GetRuleStr(rule14));
                isValid = false;
            }
        }

        private void DoRule15(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisBE-EntityDetailAssociation"
                && (client.Stereotype != "XisBusinessEntity" || supplier.Stereotype != "XisEntity"))
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule15), EA.EnumMVErrorType.mvError, GetRuleStr(rule15));
                isValid = false;
            }
        }

        private void DoRule16(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisBE-EntityReferenceAssociation"
                && (client.Stereotype != "XisBusinessEntity" || supplier.Stereotype != "XisEntity"))
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule16), EA.EnumMVErrorType.mvError, GetRuleStr(rule16));
                isValid = false;
            }
        }

        private void DoRule17(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisBusinessEntity")
            {
                bool hasMaster = false;
                EA.Connector conn = null;

                for (short i = 0; i < Element.Connectors.Count; i++)
                {
                    conn = Element.Connectors.GetAt(i);

                    if (conn.Stereotype == "XisBE-EntityMasterAssociation")
                    {
                        hasMaster = true;
                        break;
                    }
                }

                if (!hasMaster)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule17), EA.EnumMVErrorType.mvError, GetRuleStr(rule17));
                    isValid = false;
                }
            }
        }

        private void DoRule18(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "UseCases View")
            {
                List<EA.Element> useCases = new List<EA.Element>();

                foreach (EA.Element el in Package.Elements)
                {
                    if (el.Type == "UseCase" && (el.Stereotype == "XisEntityUseCase" || el.Stereotype == "XisServiceUseCase"))
                    {
                        useCases.Add(el);
                    }
                }

                if (useCases.Count > 1)
                {
                    bool isStartingUseCase = false;
                    int startingCounter = 0;

                    foreach (EA.Element uc in useCases)
                    {
                        isStartingUseCase = bool.Parse(M2MTransformer.GetTaggedValue(uc.TaggedValues, "isStartingUseCase").Value);
                        if (isStartingUseCase)
                        {
                            startingCounter++;
                        }
                    }

                    if (startingCounter != 1)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule18), EA.EnumMVErrorType.mvError, GetRuleStr(rule18));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule19(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisEntityUseCase")
            {
                EA.Project Project = Repository.GetProjectInterface();

                if (Element.Connectors.Count > 0)
                {
                    EA.Connector conn = null;
                    EA.Element supplier = null;

                    for (short i = 0; i < Element.Connectors.Count; i++)
                    {
                        conn = Element.Connectors.GetAt(i);
                        supplier = Repository.GetElementByID(conn.SupplierID);

                        if (conn.Stereotype != "XisEntityUC-BEAssociation" && supplier.Stereotype == "XisBusinessEntity")
                        {
                            Project.PublishResult(LookupMap(rule19), EA.EnumMVErrorType.mvError, GetRuleStr(rule19));
                            isValid = false;
                        }
                    }
                }
                else
                {
                    Project.PublishResult(LookupMap(rule19), EA.EnumMVErrorType.mvError, GetRuleStr(rule19));
                    isValid = false;
                }
            }
        }

        private void DoRule20(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisServiceUseCase")
            {
                EA.Project Project = Repository.GetProjectInterface();

                if (Element.Connectors.Count > 0)
                {
                    EA.Connector conn = null;
                    EA.Element supplier = null;

                    for (short i = 0; i < Element.Connectors.Count; i++)
                    {
                        conn = Element.Connectors.GetAt(i);
                        supplier = Repository.GetElementByID(conn.SupplierID);

                        if ((conn.Stereotype != "XisServiceUC-BEAssociation" && supplier.Stereotype == "XisBusinessEntity")
                            || (conn.Stereotype != "XisServiceUC-ProviderAssociation"
                            && (supplier.Stereotype == "XisInternalProvider" || supplier.Stereotype == "XisServer"
                                || supplier.Stereotype == "XisClientWebApp"
                            )))
                        {
                            Project.PublishResult(LookupMap(rule19), EA.EnumMVErrorType.mvError, GetRuleStr(rule19));
                            isValid = false;
                        }
                    }
                }
                else
                {
                    Project.PublishResult(LookupMap(rule20), EA.EnumMVErrorType.mvError, GetRuleStr(rule20));
                    isValid = false;
                }
            }
        }

        private void DoRule21(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisEntityUC-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisEntityUseCase" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule21), EA.EnumMVErrorType.mvError, GetRuleStr(rule21));
                    isValid = false;
                }
            }
        }

        private void DoRule22(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisServiceUC-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisServiceUseCase" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule22), EA.EnumMVErrorType.mvError, GetRuleStr(rule22));
                    isValid = false;
                }
            }
        }

        private void DoRule23(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisServiceUC-ProviderAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisServiceUseCase" || (supplier.Stereotype != "XisInternalProvider"
                    && supplier.Stereotype != "XisServer" && supplier.Stereotype != "XisClientWebApp"))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule23), EA.EnumMVErrorType.mvError, GetRuleStr(rule23));
                    isValid = false;
                }
            }
        }

        private void DoRule24(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisWebApp")
            {
                EA.Project Project = Repository.GetProjectInterface();

                if (Element.Connectors.Count > 0)
                {
                    EA.Connector conn = null;

                    for (short i = 0; i < Element.Connectors.Count; i++)
                    {
                        conn = Element.Connectors.GetAt(i);

                        if (conn.Stereotype != "XisWebApp-ServiceAssociation")
                        {
                            Project.PublishResult(LookupMap(rule24), EA.EnumMVErrorType.mvError, GetRuleStr(rule24));
                            isValid = false;
                            break;
                        }
                    }
                }
                else
                {
                    Project.PublishResult(LookupMap(rule24), EA.EnumMVErrorType.mvError, GetRuleStr(rule24));
                    isValid = false;
                }
            }
        }

        private void DoRule25(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisWebApp-ServiceAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisWebApp"
                    || (supplier.Stereotype != "XisInternalService" && supplier.Stereotype != "XisRemoteService"))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule25), EA.EnumMVErrorType.mvError, GetRuleStr(rule25));
                    isValid = false;
                }
            }
        }

        private void DoRule26(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Interface" && (Element.Stereotype == "XisInternalService" || Element.Stereotype == "XisRemoteService"))
            {
                if (Element.Methods.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule26), EA.EnumMVErrorType.mvError, GetRuleStr(rule26));
                    isValid = false;
                }
            }
        }

        private void DoRule27(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Interface" && (Element.Stereotype == "XisInternalService" || Element.Stereotype == "XisRemoteService"))
            {
                if (Element.Methods.Count > 0)
                {
                    foreach (EA.Method method in Element.Methods)
                    {
                        if (method.Stereotype != "XisServiceMethod")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule27), EA.EnumMVErrorType.mvError, GetRuleStr(rule27));
                            isValid = false;
                        }
                    }
                }
            }
        }

        private void DoRule28(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInternalProvider")
            {
                if (Element.Connectors.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule28), EA.EnumMVErrorType.mvError, GetRuleStr(rule28));
                    isValid = false;
                }
            }
        }

        private void DoRule29(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisServer")
            {
                if (Element.Connectors.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule29), EA.EnumMVErrorType.mvError, GetRuleStr(rule29));
                    isValid = false;
                }
            }
        }

        private void DoRule30(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisClientWebApp")
            {
                if (Element.Connectors.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule30), EA.EnumMVErrorType.mvError, GetRuleStr(rule30));
                    isValid = false;
                }
            }
        }

        private void DoRule31(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisInteractionSpaceAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisInteractionSpace")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule31), EA.EnumMVErrorType.mvError, GetRuleStr(rule31));
                    isValid = false;
                }
            }
        }

        private void DoRule32(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "InteractionSpace View" && Package.Elements.Count > 0)
            {
                int mainScreenCounter = 0;
                bool isMainScreen = false;

                foreach (EA.Element el in Package.Elements)
                {
                    if (el.Stereotype == "XisInteractionSpace")
                    {
                        isMainScreen = bool.Parse(M2MTransformer.GetTaggedValue(el.TaggedValues, "isMainScreen").Value);
                        if (isMainScreen)
                        {
                            mainScreenCounter++;
                        }
                    }
                }

                if (mainScreenCounter != 1)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule32), EA.EnumMVErrorType.mvError, GetRuleStr(rule32));
                    isValid = false;
                }
            }
        }

        private void DoRule33(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInteractionSpace")
            {
                if (Element.Elements.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule33), EA.EnumMVErrorType.mvError, GetRuleStr(rule33));
                    isValid = false;
                }
            }
        }

        private void DoRule34(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInteractionSpace")
            {
                if (Element.Elements.Count > 0)
                {
                    bool valid = true;
                    EA.Element widget = null;

                    for (short i = 0; i < Element.Elements.Count && valid; i++)
                    {
                        widget = Element.Elements.GetAt(i);

                        switch (widget.Stereotype)
                        {
                            case "XisLabel":
                            case "XisTextBox":
                            case "XisCheckBox":
                            case "XisButton":
                            case "XisLink":
                            case "XisImage":
                            case "XisDatePicker":
                            case "XisTimePicker":
                            case "XisWebView":
                            case "XisMapView":
                            case "XisDropDown":
                            case "XisList":
                            case "XisForm":
                            case "XisVisibilityBoundary":
                            case "XisMenu":
                            case "XisCollapsible":
                                break;
                            default:
                                valid = false;
                                break;
                        }
                    }

                    if (!valid)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule34), EA.EnumMVErrorType.mvError, GetRuleStr(rule34));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule35(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule35), EA.EnumMVErrorType.mvError, GetRuleStr(rule35));
                    isValid = false;
                }
            }
        }

        private void DoRule36(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-MenuAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisMenu")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule36), EA.EnumMVErrorType.mvError, GetRuleStr(rule36));
                    isValid = false;
                }
            }
        }

        private void DoRule37(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-DialogAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisDialog")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule37), EA.EnumMVErrorType.mvError, GetRuleStr(rule37));
                    isValid = false;
                }
            }
        }

        private void DoRule38(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisGesture")
            {
                if (Element.Methods.Count == 1)
                {
                    EA.Method method = Element.Methods.GetAt(0);

                    if (method.Stereotype != "XisAction")
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule38), EA.EnumMVErrorType.mvError, GetRuleStr(rule38));
                        isValid = false;
                    }
                }
                else
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule38), EA.EnumMVErrorType.mvError, GetRuleStr(rule38));
                    isValid = false;
                }
            }
        }

        private void DoRule39(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisWidget-GestureAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (supplier.Stereotype != "XisGesture" ||
                    (client.Stereotype != "XisLabel" && client.Stereotype != "XisTextBox" && client.Stereotype != "XisCheckBox"
                     && client.Stereotype != "Button" && client.Stereotype != "Link" && client.Stereotype != "XisImage"
                     && client.Stereotype != "XisDatePicker" && client.Stereotype != "XisTimePicker" && client.Stereotype != "XisDropdown"
                     && client.Stereotype != "XisListItem" && client.Stereotype != "XisMenuItem"))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule39), EA.EnumMVErrorType.mvError, GetRuleStr(rule39));
                    isValid = false;
                }
            }
        }

        private void DoRule40(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisList")
            {
                if (Element.Elements.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule40), EA.EnumMVErrorType.mvError, GetRuleStr(rule40));
                    isValid = false;
                }
            }
        }

        private void DoRule41(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisList")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "XisListItem" && el.Stereotype != "XisListGroup")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule41), EA.EnumMVErrorType.mvError, GetRuleStr(rule41));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule42(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListGroup")
            {
                if (Element.Elements.Count > 0)
                {
                    bool hasItem = false;
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype == "XisListItem")
                        {
                            hasItem = true;
                            break;
                        }
                    }

                    if (!hasItem)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule42), EA.EnumMVErrorType.mvError, GetRuleStr(rule42));
                        isValid = false;
                    }
                }
                else
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule42), EA.EnumMVErrorType.mvError, GetRuleStr(rule42));
                    isValid = false;
                }
            }
        }

        private void DoRule43(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListGroup")
            {
                if (Element.Elements.Count > 0)
                {
                    int itemCounter = 0;
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype == "XisListItem")
                        {
                            itemCounter++;
                        }

                        if (itemCounter > 1)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule43), EA.EnumMVErrorType.mvError, GetRuleStr(rule43));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule44(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "XisLabel" && el.Stereotype != "XisTextBox" && el.Stereotype != "XisCheckBox"
                            && el.Stereotype != "Button" && el.Stereotype != "Link" && el.Stereotype != "XisImage"
                            && el.Stereotype != "XisDatePicker" && el.Stereotype != "XisTimePicker" && el.Stereotype != "XisWebView"
                            && el.Stereotype != "XisMapView" && el.Stereotype != "XisDropdown")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule44), EA.EnumMVErrorType.mvError, GetRuleStr(rule44));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule45(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenu")
            {
                if (Element.Elements.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule45), EA.EnumMVErrorType.mvError, GetRuleStr(rule45));
                    isValid = false;
                }
            }
        }

        private void DoRule46(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenu")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "XisMenuItem" && el.Stereotype != "XisMenuGroup" && el.Stereotype != "XisVisibilityBoundary")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule46), EA.EnumMVErrorType.mvError, GetRuleStr(rule46));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule47(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenuGroup")
            {
                if (Element.Elements.Count > 0)
                {
                    bool hasItem = false;
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype == "XisMenuItem")
                        {
                            hasItem = true;
                            break;
                        }
                    }

                    if (!hasItem)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule47), EA.EnumMVErrorType.mvError, GetRuleStr(rule47));
                        isValid = false;
                    }
                }
                else
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule47), EA.EnumMVErrorType.mvError, GetRuleStr(rule47));
                    isValid = false;
                }
            }
        }

        private void DoRule48(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenuGroup")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "XisListItem")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule48), EA.EnumMVErrorType.mvError, GetRuleStr(rule48));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule49(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenuItem")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule49), EA.EnumMVErrorType.mvError, GetRuleStr(rule49));
                    isValid = false;
                }
            }
        }

        private void DoRule50(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenu")
            {
                EA.TaggedValue menuType = M2MTransformer.GetTaggedValue(Element.TaggedValues, "type");

                if (menuType != null && menuType.Value == "OptionsMenu")
                {
                    if (Element.ParentID > 0)
                    {
                        EA.Element parent = Repository.GetElementByID(Element.ParentID);

                        if (parent.Stereotype != "XisInteractionSpace")
                        {
                            if (parent.Stereotype == "XisVisibilityBoundary" && parent.ParentID > 0)
                            {
                                parent = Repository.GetElementByID(parent.ParentID);

                                if (parent.Stereotype != "XisInteractionSpace")
                                {
                                    EA.Project Project = Repository.GetProjectInterface();
                                    Project.PublishResult(LookupMap(rule50), EA.EnumMVErrorType.mvError, GetRuleStr(rule50));
                                    isValid = false;    
                                }
                            }
                            else
                            {
                                EA.Project Project = Repository.GetProjectInterface();
                                Project.PublishResult(LookupMap(rule50), EA.EnumMVErrorType.mvError, GetRuleStr(rule50));
                                isValid = false;
                            }
                        }
                    }
                    else if (Element.Connectors.Count > 0)
                    {
                        EA.Connector conn = null;
                        EA.Element end = null;

                        for (short i = 0; i < Element.Connectors.Count; i++)
                        {
                            conn = Element.Connectors.GetAt(i);

                            if (conn.ClientID != Element.ElementID)
                            {
                                end = Repository.GetElementByID(conn.ClientID);
                            }
                            else
                            {
                                end = Repository.GetElementByID(conn.SupplierID);
                            }

                            if ((conn.Stereotype == "XisIS-MenuAssociation" && end.Stereotype != "XisInteractionSpace")
                                || (conn.Stereotype != "XisIS-MenuAssociation" && end.Stereotype == "XisInteractionSpace"))
                            {
                                EA.Project Project = Repository.GetProjectInterface();
                                Project.PublishResult(LookupMap(rule50), EA.EnumMVErrorType.mvError, GetRuleStr(rule50));
                                isValid = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule50), EA.EnumMVErrorType.mvError, GetRuleStr(rule50));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule51_54_57(EA.Repository Repository, EA.Element Element, String stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                String onTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onTap").Value;

                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;
                    bool noOnTap = false;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype == "XisAction" && method.Name != onTap)
                        {
                            noOnTap = true;
                            break;
                        }
                    }

                    if (noOnTap)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        switch (stereotype)
                        {
                            case "XisButton":
                                Project.PublishResult(LookupMap(rule51), EA.EnumMVErrorType.mvError, GetRuleStr(rule51));
                                isValid = false;
                                break;
                            case "XisLink":
                                Project.PublishResult(LookupMap(rule54), EA.EnumMVErrorType.mvError, GetRuleStr(rule54));
                                isValid = false;
                                break;
                            case "XisMenuItem":
                                Project.PublishResult(LookupMap(rule57), EA.EnumMVErrorType.mvError, GetRuleStr(rule57));
                                isValid = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void DoRule52_55_58(EA.Repository Repository, EA.Element Element, String stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                String onTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onTap").Value;

                if (!string.IsNullOrEmpty(onTap))
                {
                    if (Element.Methods.Count > 0)
                    {
                        EA.Method method = null;
                        bool exists = false;

                        for (short i = 0; i < Element.Methods.Count; i++)
                        {
                            method = Element.Methods.GetAt(i);

                            if (method.Stereotype == "XisAction" && method.Name == onTap)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            switch (stereotype)
                            {
                                case "XisButton":
                                    Project.PublishResult(LookupMap(rule52), EA.EnumMVErrorType.mvError, GetRuleStr(rule52));
                                    isValid = false;
                                    break;
                                case "XisLink":
                                    Project.PublishResult(LookupMap(rule55), EA.EnumMVErrorType.mvError, GetRuleStr(rule55));
                                    isValid = false;
                                    break;
                                case "XisMenuItem":
                                    Project.PublishResult(LookupMap(rule58), EA.EnumMVErrorType.mvError, GetRuleStr(rule58));
                                    isValid = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        switch (stereotype)
                        {
                            case "XisButton":
                                Project.PublishResult(LookupMap(rule52), EA.EnumMVErrorType.mvError, GetRuleStr(rule52));
                                isValid = false;
                                break;
                            case "XisLink":
                                Project.PublishResult(LookupMap(rule55), EA.EnumMVErrorType.mvError, GetRuleStr(rule55));
                                isValid = false;
                                break;
                            case "XisMenuItem":
                                Project.PublishResult(LookupMap(rule58), EA.EnumMVErrorType.mvError, GetRuleStr(rule58));
                                isValid = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void DoRule53_56_59(EA.Repository Repository, EA.Element Element, String stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;
                    int actionCounter = 0;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype == "XisAction")
                        {
                            actionCounter++;
                        }
                    }

                    if (actionCounter > 1)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        switch (stereotype)
                        {
                            case "XisButton":
                                Project.PublishResult(LookupMap(rule53), EA.EnumMVErrorType.mvError, GetRuleStr(rule53));
                                isValid = false;
                                break;
                            case "XisLink":
                                Project.PublishResult(LookupMap(rule56), EA.EnumMVErrorType.mvError, GetRuleStr(rule56));
                                isValid = false;
                                break;
                            case "XisMenuItem":
                                Project.PublishResult(LookupMap(rule59), EA.EnumMVErrorType.mvError, GetRuleStr(rule59));
                                isValid = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void DoRule60(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                String onTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onTap").Value;
                String onLongTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onLongTap").Value;

                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;
                    bool noDefaultGestures = false;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype == "XisAction" && method.Name != onTap && method.Name != onLongTap)
                        {
                            noDefaultGestures = true;
                            break;
                        }
                    }

                    if (noDefaultGestures)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule60), EA.EnumMVErrorType.mvError, GetRuleStr(rule60));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule61(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                String onTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onTap").Value;

                if (!string.IsNullOrEmpty(onTap))
                {
                    if (Element.Methods.Count > 0)
                    {
                        EA.Method method = null;
                        bool exists = false;

                        for (short i = 0; i < Element.Methods.Count; i++)
                        {
                            method = Element.Methods.GetAt(i);

                            if (method.Stereotype == "XisAction" && method.Name == onTap)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule61), EA.EnumMVErrorType.mvError, GetRuleStr(rule61));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule61), EA.EnumMVErrorType.mvError, GetRuleStr(rule61));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule62(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                String onLongTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onLongTap").Value;

                if (!string.IsNullOrEmpty(onLongTap))
                {
                    if (Element.Methods.Count > 0)
                    {
                        EA.Method method = null;
                        bool exists = false;

                        for (short i = 0; i < Element.Methods.Count; i++)
                        {
                            method = Element.Methods.GetAt(i);

                            if (method.Stereotype == "XisAction" && method.Name == onLongTap)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule62), EA.EnumMVErrorType.mvError, GetRuleStr(rule62));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule62), EA.EnumMVErrorType.mvError, GetRuleStr(rule62));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule63(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;
                    int actionCounter = 0;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype == "XisAction")
                        {
                            actionCounter++;
                        }
                    }

                    if (actionCounter > 2)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule63), EA.EnumMVErrorType.mvError, GetRuleStr(rule63));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule64(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisDialog")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;
                    int buttonCounter = 0;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype == "Class" && el.Stereotype == "XisButton")
                        {
                            buttonCounter++;
                        }
                        else
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule64), EA.EnumMVErrorType.mvError, GetRuleStr(rule64));
                            isValid = false;
                            break;
                        }
                    }

                    if (buttonCounter > 3)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule64), EA.EnumMVErrorType.mvError, GetRuleStr(rule64));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule65(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMapView")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "Class" || el.Stereotype != "XisMapMarker")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule65), EA.EnumMVErrorType.mvError, GetRuleStr(rule65));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule66_to_75(EA.Repository Repository, EA.Element Element, string stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    switch (stereotype)
                    {
                        case "XisLabel":
                            Project.PublishResult(LookupMap(rule66), EA.EnumMVErrorType.mvError, GetRuleStr(rule66));
                            isValid = false;
                            break;
                        case "XisTextBox":
                            Project.PublishResult(LookupMap(rule67), EA.EnumMVErrorType.mvError, GetRuleStr(rule67));
                            isValid = false;
                            break;
                        case "XisCheckBox":
                            Project.PublishResult(LookupMap(rule68), EA.EnumMVErrorType.mvError, GetRuleStr(rule68));
                            isValid = false;
                            break;
                        case "XisButton":
                            Project.PublishResult(LookupMap(rule69), EA.EnumMVErrorType.mvError, GetRuleStr(rule69));
                            isValid = false;
                            break;
                        case "XisLink":
                            Project.PublishResult(LookupMap(rule70), EA.EnumMVErrorType.mvError, GetRuleStr(rule70));
                            isValid = false;
                            break;
                        case "XisImage":
                            Project.PublishResult(LookupMap(rule71), EA.EnumMVErrorType.mvError, GetRuleStr(rule71));
                            isValid = false;
                            break;
                        case "XisDatePicker":
                            Project.PublishResult(LookupMap(rule72), EA.EnumMVErrorType.mvError, GetRuleStr(rule72));
                            isValid = false;
                            break;
                        case "XisTimePicker":
                            Project.PublishResult(LookupMap(rule73), EA.EnumMVErrorType.mvError, GetRuleStr(rule73));
                            isValid = false;
                            break;
                        case "XisWebView":
                            Project.PublishResult(LookupMap(rule74), EA.EnumMVErrorType.mvError, GetRuleStr(rule74));
                            isValid = false;
                            break;
                        case "XisDropdown":
                            Project.PublishResult(LookupMap(rule75), EA.EnumMVErrorType.mvError, GetRuleStr(rule75));
                            isValid = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void DoRule76(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction")
            {
                EA.Element element = Repository.GetElementByID(method.ParentID);

                if (element.Stereotype != "XisGesture" && element.Stereotype != "XisListItem"
                    && element.Stereotype != "XisButton" && element.Stereotype != "XisLink" && element.Stereotype != "XisMenuItem")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule76), EA.EnumMVErrorType.mvError, GetRuleStr(rule76));
                    isValid = false;
                }
            }
        }

        private void DoRule77(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction")
            {
                string type = M2MTransformer.GetMethodTag(method.TaggedValues, "type").Value;

                if (string.IsNullOrEmpty(type))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule77), EA.EnumMVErrorType.mvError, GetRuleStr(rule77));
                    isValid = false;
                }
            }
        }

        private void DoRule78(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction")
            {
                string navigation = M2MTransformer.GetMethodTag(method.TaggedValues, "navigation").Value;

                if (!string.IsNullOrEmpty(navigation))
                {
                    EA.Package model = Repository.GetPackageByID(Repository.GetElementByID(method.ParentID).PackageID);
                    EA.Package interactionSpaceView = null;

                    if (model.StereotypeEx == "InteractionSpace View")
                    {
                        interactionSpaceView = model;
                    }

                    if (interactionSpaceView != null)
                    {
                        EA.Element el = null;
                        bool hasIS = false;

                        for (short i = 0; i < interactionSpaceView.Elements.Count; i++)
                        {
                            el = interactionSpaceView.Elements.GetAt(i);

                            if (el.Type == "Class" && el.Stereotype == "XisInteractionSpace" && el.Name == navigation)
                            {
                                hasIS = true;
                                break;
                            }
                        }

                        if (!hasIS)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule78), EA.EnumMVErrorType.mvError, GetRuleStr(rule78));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule78), EA.EnumMVErrorType.mvError, GetRuleStr(rule78));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule79_to_83(EA.Repository Repository, EA.Method method, string crud)
        {
            if (method.Stereotype == "XisAction")
            {
                string type = M2MTransformer.GetMethodTag(method.TaggedValues, "type").Value;

                if (type == crud)
                {
                    EA.Element parent = Repository.GetElementByID(method.ParentID);
                    bool needsParam = false;
                    bool publishResult = false;

                    while (parent != null)
                    {
                        if (parent.Stereotype == "XisForm" || parent.Stereotype == "XisList"
                            || parent.Stereotype == "XisListGroup" || parent.Stereotype == "XisMenu")
                        {
                            break;
                        }
                        else if (parent.Stereotype == "XisInteractionSpace")
                        {
                            needsParam = true;
                            break;
                        }

                        if (parent.ParentID > 0)
                        {
                            try
                            {
                                parent = Repository.GetElementByID(parent.ParentID);
                            }
                            catch (Exception)
                            {
                                break;
                            } 
                        }
                    }

                    if (needsParam)
                    {
                        if (method.Parameters.Count == 1)
                        {
                            EA.Parameter p = method.Parameters.GetAt(0);

                            if (parent.Connectors.Count > 0 && p.Name == "entityName" && !string.IsNullOrEmpty(p.Default))
                            {
                                EA.Connector conn = null;
                                EA.Connector assoc = null;

                                for (short i = 0; i < parent.Connectors.Count; i++)
                                {
                                    conn = parent.Connectors.GetAt(i);

                                    if (conn.Stereotype == "XisIS-BEAssociation")
                                    {
                                        assoc = conn;
                                        break;
                                    }
                                }

                                if (assoc != null)
                                {
                                    EA.Element be = Repository.GetElementByID(assoc.SupplierID);
                                    bool hasEntity = false;

                                    if (be.Stereotype == "XisBusinessEntity" && be.Connectors.Count > 0)
                                    {
                                        EA.Element entity = null;

                                        for (short i = 0; i < be.Connectors.Count; i++)
                                        {
                                            conn = be.Connectors.GetAt(i);

                                            if (conn.Stereotype == "XisBE-EntityMasterAssociation"
                                                || conn.Stereotype == "XisBE-EntityDetailAssociation"
                                                || conn.Stereotype == "XisBE-EntityReferenceAssociation")
                                            {
                                                entity = Repository.GetElementByID(conn.SupplierID);

                                                if (entity.Type == "Class" && entity.Stereotype == "XisEntity"
                                                    && entity.Name == p.Default)
                                                {
                                                    hasEntity = true;
                                                    break;
                                                }
                                            }
                                        }

                                        if (!hasEntity)
                                        {
                                            publishResult = true;
                                        }
                                    }
                                    else
                                    {
                                        publishResult = true;
                                    }
                                }
                                else
                                {
                                    publishResult = true;
                                }
                            }
                            else
                            {
                                publishResult = true;
                            }
                        }
                        else
                        {
                            publishResult = true;
                        }
                    }

                    if (publishResult)
                    {
                        EA.Project Project = Repository.GetProjectInterface();

                        switch (crud)
                        {
                            case "Create":
                                Project.PublishResult(LookupMap(rule79), EA.EnumMVErrorType.mvError, GetRuleStr(rule79));
                                isValid = false;
                                break;
                            case "Read":
                                Project.PublishResult(LookupMap(rule80), EA.EnumMVErrorType.mvError, GetRuleStr(rule80));
                                isValid = false;
                                break;
                            case "Update":
                                Project.PublishResult(LookupMap(rule81), EA.EnumMVErrorType.mvError, GetRuleStr(rule81));
                                isValid = false;
                                break;
                            case "Delete":
                                Project.PublishResult(LookupMap(rule82), EA.EnumMVErrorType.mvError, GetRuleStr(rule82));
                                isValid = false;
                                break;
                            case "DeleteAll":
                                Project.PublishResult(LookupMap(rule83), EA.EnumMVErrorType.mvError, GetRuleStr(rule83));
                                isValid = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void DoRule84(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction")
            {
                string type = M2MTransformer.GetMethodTag(method.TaggedValues, "type").Value;

                if (type == "OpenBrowser")
                {
                    if (method.Parameters.Count == 1)
                    {
                        EA.Parameter p = method.Parameters.GetAt(0);

                        if (p.Name != "url" || string.IsNullOrEmpty(p.Default))
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule84), EA.EnumMVErrorType.mvError, GetRuleStr(rule84));
                            isValid = false;    
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule84), EA.EnumMVErrorType.mvError, GetRuleStr(rule84));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule85(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction")
            {
                string type = M2MTransformer.GetMethodTag(method.TaggedValues, "type").Value;

                if (type == "WebService")
                {
                    if (method.Name.Contains('.'))
                    {
                        string[] serviceName = method.Name.Split('.');

                        if (serviceName.Length == 2)
                        {
                            EA.Package model = Repository.GetPackageByID(Repository.GetPackageByID(
                                Repository.GetElementByID(method.ParentID).PackageID).ParentID);
                            EA.Package architectural = null;

                            foreach (EA.Package package in model.Packages)
                            {
                                if (package.StereotypeEx == "Architectural View")
                                {
                                    architectural = package;
                                    break;
                                }
                            }

                            if (architectural != null)
                            {
                                EA.Element service = null;

                                foreach (EA.Element el in architectural.Elements)
                                {
                                    if (el.Type == "Interface"
                                        && (el.Stereotype == "XisInternalService" || el.Stereotype == "XisRemoteService")
                                        && el.Name == serviceName[0])
                                    {
                                        service = el;
                                        break;
                                    }
                                }

                                if (service != null && service.Methods.Count > 0)
                                {
                                    bool hasMethod = false;

                                    foreach (EA.Method m in service.Methods)
                                    {
                                        if (m.Stereotype == "XisServiceMethod" && m.Name == serviceName[1])
                                        {
                                            hasMethod = true;
                                            break;
                                        }
                                    }

                                    if (!hasMethod)
                                    {
                                        EA.Project Project = Repository.GetProjectInterface();
                                        Project.PublishResult(LookupMap(rule85), EA.EnumMVErrorType.mvError, GetRuleStr(rule85));
                                        isValid = false;
                                    }
                                }
                                else
                                {
                                    EA.Project Project = Repository.GetProjectInterface();
                                    Project.PublishResult(LookupMap(rule85), EA.EnumMVErrorType.mvError, GetRuleStr(rule85));
                                    isValid = false;
                                }
                            }
                            else
                            {
                                EA.Project Project = Repository.GetProjectInterface();
                                Project.PublishResult(LookupMap(rule85), EA.EnumMVErrorType.mvError, GetRuleStr(rule85));
                                isValid = false;
                            }
                        }
                        else
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule85), EA.EnumMVErrorType.mvError, GetRuleStr(rule85));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule85), EA.EnumMVErrorType.mvError, GetRuleStr(rule85));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule86(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction")
            {
                string type = M2MTransformer.GetMethodTag(method.TaggedValues, "type").Value;

                if (type == "WebService")
                {
                    if (method.Name.Contains('.'))
                    {
                        string[] serviceName = method.Name.Split('.');

                        if (serviceName.Length == 2)
                        {
                            EA.Package model = Repository.GetPackageByID(Repository.GetElementByID(method.ParentID).PackageID);
                            EA.Package package = null;
                            EA.Package architectural = null;

                            for (short i = 0; i < model.Packages.Count; i++)
                            {
                                package = model.Packages.GetAt(i);

                                if (package.StereotypeEx == "Architectural View")
                                {
                                    architectural = package;
                                    break;
                                }
                            }

                            if (architectural != null)
                            {
                                EA.Element el = null;
                                EA.Element service = null;

                                for (short i = 0; i < architectural.Elements.Count; i++)
                                {
                                    el = architectural.Elements.GetAt(i);

                                    if (el.Type == "Interface"
                                        && (el.Stereotype == "XisInternalService" || el.Stereotype == "XisRemoteService")
                                        && el.Name == serviceName[0])
                                    {
                                        service = el;
                                        break;
                                    }
                                }

                                if (service != null && service.Methods.Count > 0)
                                {
                                    EA.Method m = null;
                                    bool hasMethod = false;

                                    for (short i = 0; i < service.Methods.Count; i++)
                                    {
                                        m = service.Methods.GetAt(i);

                                        if (m.Stereotype == "XisServiceMethod" && m.Name == serviceName[1])
                                        {
                                            hasMethod = true;
                                            break;
                                        }
                                    }

                                    if (hasMethod)
                                    {
                                        if (m.Parameters.Count == method.Parameters.Count)
                                        {
                                            EA.Parameter p1 = null;
                                            EA.Parameter p2 = null;

                                            for (short i = 0; i < m.Parameters.Count; i++)
                                            {
                                                p1 = m.Parameters.GetAt(i);
                                                p2 = method.Parameters.GetAt(i);

                                                if (p1.Name == p2.Name)
                                                {
                                                    EA.Project Project = Repository.GetProjectInterface();
                                                    Project.PublishResult(LookupMap(rule86), EA.EnumMVErrorType.mvError, GetRuleStr(rule86));
                                                    isValid = false;
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            EA.Project Project = Repository.GetProjectInterface();
                                            Project.PublishResult(LookupMap(rule86), EA.EnumMVErrorType.mvError, GetRuleStr(rule86));
                                            isValid = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DoRule87(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction")
            {
                string type = M2MTransformer.GetMethodTag(method.TaggedValues, "type").Value;

                if (type == "Navigate")
                {
                    string navigation = M2MTransformer.GetMethodTag(method.TaggedValues, "navigation").Value;

                    if (!string.IsNullOrEmpty(navigation))
                    {
                        EA.Package model = Repository.GetPackageByID(Repository.GetElementByID(method.ParentID).PackageID);
                        EA.Package interaction = null;

                        if (model.StereotypeEx == "InteractionSpace View")
                        {
                            interaction = model;
                        }

                        if (interaction != null)
                        {
                            EA.Element el = null;
                            bool exists = false;

                            for (short i = 0; i < interaction.Elements.Count; i++)
                            {
                                el = interaction.Elements.GetAt(i);

                                if (el.Type == "Class" && el.Stereotype == "XisInteractionSpace" && el.Name == navigation)
                                {
                                    exists = true;
                                    break;
                                }
                            }

                            if (!exists)
	                        {
		                        EA.Project Project = Repository.GetProjectInterface();
                                Project.PublishResult(LookupMap(rule87), EA.EnumMVErrorType.mvError, GetRuleStr(rule87));
                                isValid = false;
	                        }
                        }
                        else
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule87), EA.EnumMVErrorType.mvError, GetRuleStr(rule87));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule87), EA.EnumMVErrorType.mvError, GetRuleStr(rule87));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule88(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisForm")
            {
                string entityName = M2MTransformer.GetTaggedValue(Element.TaggedValues, "entityName").Value;

                if (string.IsNullOrEmpty(entityName))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule88), EA.EnumMVErrorType.mvError, GetRuleStr(rule88));
                    isValid = false;
                }
            }
        }

        private void DoRule89_to_93(EA.Repository Repository, EA.Element Element, string stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                string entityName = M2MTransformer.GetTaggedValue(Element.TaggedValues, "entityName").Value;

                if (!string.IsNullOrEmpty(entityName))
                {
                    EA.Element space = null;
                    EA.Element el = null;
                    EA.Connector conn = null;
                    EA.Connector assoc = null;
                    bool publishResult = false;
                    int parentID = Element.ParentID;

                    while (parentID > 0)
                    {
                        el = Repository.GetElementByID(parentID);

                        if (el.Type == "Class" && el.Stereotype == "XisInteractionSpace")
                        {
                            space = el;
                            break;
                        }
                        parentID = el.ParentID;
                    }

                    if (space == null && stereotype == "XisMenu")
                    {
                        EA.Element end = null;

                        for (short i = 0; i < Element.Connectors.Count; i++)
                        {
                            conn = Element.Connectors.GetAt(i);

                            if (conn.ClientID != Element.ElementID)
                            {
                                end = Repository.GetElementByID(conn.ClientID);
                            }
                            else
                            {
                                end = Repository.GetElementByID(conn.SupplierID);
                            }

                            if (conn.Stereotype == "XisIS-MenuAssociation")
                            {
                                if (end.Stereotype == "XisInteractionSpace")
                                {
                                    space = end;
                                    break;
                                }
                                else
                                {
                                    parentID = end.ParentID;

                                    while (parentID > 0)
                                    {
                                        el = Repository.GetElementByID(parentID);

                                        if (el.Type == "Class" && el.Stereotype == "XisInteractionSpace")
                                        {
                                            space = el;
                                            break;
                                        }
                                        parentID = el.ParentID;
                                    }
                                }
                            }
                        }
                    }

                    if (space != null)
                    {
                        for (short i = 0; i < space.Connectors.Count; i++)
                        {
                            conn = space.Connectors.GetAt(i);

                            if (conn.Stereotype == "XisIS-BEAssociation")
                            {
                                assoc = conn;
                                break;
                            }
                        }

                        if (assoc != null)
                        {
                            EA.Element be = Repository.GetElementByID(assoc.SupplierID);
                            bool hasEntity = false;

                            if (be.Stereotype == "XisBusinessEntity" && be.Connectors.Count > 0)
                            {
                                EA.Element entity = null;

                                for (short i = 0; i < be.Connectors.Count; i++)
                                {
                                    conn = be.Connectors.GetAt(i);

                                    if (conn.Stereotype == "XisBE-EntityMasterAssociation"
                                        || conn.Stereotype == "XisBE-EntityDetailAssociation"
                                        || conn.Stereotype == "XisBE-EntityReferenceAssociation")
                                    {
                                        entity = Repository.GetElementByID(conn.SupplierID);

                                        if (entity.Type == "Class" && entity.Stereotype == "XisEntity"
                                            && entity.Name == entityName)
                                        {
                                            hasEntity = true;
                                            break;
                                        }
                                    }
                                }

                                if (!hasEntity)
                                {
                                    publishResult = true;
                                }
                            }
                            else
                            {
                                publishResult = true;
                            }
                        }
                        else
                        {
                            publishResult = true;
                        }
                    }
                    else
                    {
                        publishResult = true;
                    }

                    if (publishResult)
                    {
                        EA.Project Project = Repository.GetProjectInterface();

                        switch (stereotype)
                        {
                            case "XisForm":
                                Project.PublishResult(LookupMap(rule89), EA.EnumMVErrorType.mvError, GetRuleStr(rule89));
                                isValid = false;
                                break;
                            case "XisList":
                                Project.PublishResult(LookupMap(rule90), EA.EnumMVErrorType.mvError, GetRuleStr(rule90));
                                isValid = false;
                                break;
                            case "XisListGroup":
                                Project.PublishResult(LookupMap(rule91), EA.EnumMVErrorType.mvError, GetRuleStr(rule91));
                                isValid = false;
                                break;
                            case "XisVisibilityBoundary":
                                Project.PublishResult(LookupMap(rule92), EA.EnumMVErrorType.mvError, GetRuleStr(rule92));
                                isValid = false;
                                break;
                            case "XisMenu":
                                Project.PublishResult(LookupMap(rule93), EA.EnumMVErrorType.mvError, GetRuleStr(rule93));
                                isValid = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void DoRule94_95(EA.Repository Repository, EA.Element Element, string filter)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisList")
            {
                string entityName = M2MTransformer.GetTaggedValue(Element.TaggedValues, filter).Value;

                if (!string.IsNullOrEmpty(entityName) && !entityName.Contains('.'))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                        
                    switch (filter)
                    {
                        case "searchBy":
                            Project.PublishResult(LookupMap(rule94), EA.EnumMVErrorType.mvError, GetRuleStr(rule94));
                            isValid = false;
                            break;
                        case "orderBy":
                            Project.PublishResult(LookupMap(rule95), EA.EnumMVErrorType.mvError, GetRuleStr(rule95));
                            isValid = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void DoRule96_97(EA.Repository Repository, EA.Element Element, string filter)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisList")
            {
                string domainRef = M2MTransformer.GetTaggedValue(Element.TaggedValues, filter).Value;

                if (!string.IsNullOrEmpty(domainRef) && domainRef.Contains('.'))
                {
                    bool publishResult = false;
                    string[] values = domainRef.Split('.');

                    if (values.Length == 2)
                    {
                        EA.Element space = null;
                        EA.Element el = null;
                        EA.Connector conn = null;
                        EA.Connector assoc = null;
                        int parentID = Element.ParentID;

                        while (parentID > 0)
                        {
                            el = Repository.GetElementByID(parentID);

                            if (el.Type == "Class" && el.Stereotype == "XisInteractionSpace")
                            {
                                space = el;
                                break;
                            }
                            parentID = el.ParentID;
                        }

                        if (space != null)
                        {
                            for (short i = 0; i < space.Connectors.Count; i++)
                            {
                                conn = space.Connectors.GetAt(i);

                                if (conn.Stereotype == "XisIS-BEAssociation")
                                {
                                    assoc = conn;
                                    break;
                                }
                            }

                            if (assoc != null)
                            {
                                EA.Element be = Repository.GetElementByID(assoc.SupplierID);
                                bool hasEntity = false;

                                if (be.Stereotype == "XisBusinessEntity" && be.Connectors.Count > 0)
                                {
                                    EA.Element entity = null;

                                    for (short i = 0; i < be.Connectors.Count; i++)
                                    {
                                        conn = be.Connectors.GetAt(i);

                                        if (conn.Stereotype == "XisBE-EntityMasterAssociation"
                                            || conn.Stereotype == "XisBE-EntityDetailAssociation"
                                            || conn.Stereotype == "XisBE-EntityReferenceAssociation")
                                        {
                                            entity = Repository.GetElementByID(conn.SupplierID);

                                            if (entity.Type == "Class" && entity.Stereotype == "XisEntity"
                                                && entity.Name == values[0])
                                            {
                                                hasEntity = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (hasEntity && entity.Attributes.Count > 0)
                                    {
                                        bool attrExists = false;
                                        EA.Attribute attr = null;

                                        for (short i = 0; i < entity.Attributes.Count; i++)
                                        {
                                            attr = entity.Attributes.GetAt(i);

                                            if (attr.Stereotype == "XisEntityAttribute" && attr.Name == values[1])
                                            {
                                                attrExists = true;
                                                break;
                                            }
                                        }

                                        if (!attrExists)
                                        {
                                            publishResult = true;
                                        }
                                    }
                                    else
                                    {
                                        publishResult = true;
                                    }
                                }
                                else
                                {
                                    publishResult = true;
                                }
                            }
                            else
                            {
                                publishResult = true;
                            }
                        }
                        else
                        {
                            publishResult = true;
                        }
                    }
                    else
                    {
                        publishResult = true;
                    }

                    if (publishResult)
                    {
                        EA.Project Project = Repository.GetProjectInterface();

                        switch (filter)
                        {
                            case "searchBy":
                                Project.PublishResult(LookupMap(rule96), EA.EnumMVErrorType.mvError, GetRuleStr(rule96));
                                isValid = false;
                                break;
                            case "orderBy":
                                Project.PublishResult(LookupMap(rule97), EA.EnumMVErrorType.mvError, GetRuleStr(rule97));
                                isValid = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void DoRule98_to_105(EA.Repository Repository, EA.Element Element, string stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                string value = M2MTransformer.GetTaggedValue(Element.TaggedValues, "value").Value;
                string valueFromExpression = M2MTransformer.GetTaggedValue(Element.TaggedValues, "valueFromExpression").Value;
                string entityAttributeName = M2MTransformer.GetTaggedValue(Element.TaggedValues, "entityAttributeName").Value;

                if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(valueFromExpression) && string.IsNullOrEmpty(entityAttributeName))
                {
                    EA.Project Project = Repository.GetProjectInterface();

                    switch (stereotype)
                    {
                        case "XisLabel":
                            Project.PublishResult(LookupMap(rule98), EA.EnumMVErrorType.mvError, GetRuleStr(rule98));
                            isValid = false;
                            break;
                        case "XisTextBox":
                            Project.PublishResult(LookupMap(rule99), EA.EnumMVErrorType.mvError, GetRuleStr(rule99));
                            isValid = false;
                            break;
                        case "XisCheckBox":
                            Project.PublishResult(LookupMap(rule100), EA.EnumMVErrorType.mvError, GetRuleStr(rule100));
                            isValid = false;
                            break;
                        case "XisButton":
                            Project.PublishResult(LookupMap(rule101), EA.EnumMVErrorType.mvError, GetRuleStr(rule101));
                            isValid = false;
                            break;
                        case "XisLink":
                            Project.PublishResult(LookupMap(rule102), EA.EnumMVErrorType.mvError, GetRuleStr(rule102));
                            isValid = false;
                            break;
                        case "XisDatePicker":
                            Project.PublishResult(LookupMap(rule103), EA.EnumMVErrorType.mvError, GetRuleStr(rule103));
                            isValid = false;
                            break;
                        case "XisTimePicker":
                            Project.PublishResult(LookupMap(rule104), EA.EnumMVErrorType.mvError, GetRuleStr(rule104));
                            isValid = false;
                            break;
                        case "XisDropdown":
                            Project.PublishResult(LookupMap(rule105), EA.EnumMVErrorType.mvError, GetRuleStr(rule105));
                            isValid = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void DoRule106(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "Architectural View")
            {
                if (Package.Diagrams.Count > 0 )
                {
                    if (Package.Diagrams.Count == 1)
                    {
                        EA.Diagram diagram = Package.Diagrams.GetAt(0);

                        if (diagram.MetaType != "XIS-Web_Diagrams::ArchitecturalViewModel")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule106), EA.EnumMVErrorType.mvError, GetRuleStr(rule106));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule106), EA.EnumMVErrorType.mvError, GetRuleStr(rule106));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule107(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "Domain View")
            {
                if (Package.Diagrams.Count > 0)
                {
                    if (Package.Diagrams.Count == 1)
                    {
                        EA.Diagram diagram = Package.Diagrams.GetAt(0);

                        if (diagram.MetaType != "XIS-Web_Diagrams::DomainViewModel")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule107), EA.EnumMVErrorType.mvError, GetRuleStr(rule107));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule107), EA.EnumMVErrorType.mvError, GetRuleStr(rule107));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule108(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "BusinessEntities View")
            {
                if (Package.Diagrams.Count > 0)
                {
                    if (Package.Diagrams.Count == 1)
                    {
                        EA.Diagram diagram = Package.Diagrams.GetAt(0);

                        if (diagram.MetaType != "XIS-Web_Diagrams::BusinessEntitiesViewModel")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule108), EA.EnumMVErrorType.mvError, GetRuleStr(rule108));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule108), EA.EnumMVErrorType.mvError, GetRuleStr(rule108));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule109(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "UseCases View")
            {
                if (Package.Diagrams.Count > 0)
                {
                    if (Package.Diagrams.Count == 1)
                    {
                        EA.Diagram diagram = Package.Diagrams.GetAt(0);

                        if (diagram.MetaType != "XIS-Web_Diagrams::UseCasesViewModel")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule109), EA.EnumMVErrorType.mvError, GetRuleStr(rule109));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule109), EA.EnumMVErrorType.mvError, GetRuleStr(rule109));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule110(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "InteractionSpace View")
            {
                if (Package.Diagrams.Count > 0)
                {
                    EA.Diagram diagram = null;

                    for (short i = 0; i < Package.Diagrams.Count; i++)
                    {
                        diagram = Package.Diagrams.GetAt(i);

                        if (diagram.MetaType != "XIS-Web_Diagrams::InteractionSpaceViewModel")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule110), EA.EnumMVErrorType.mvError, GetRuleStr(rule110));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule111(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "NavigationSpace View")
            {
                if (Package.Diagrams.Count > 0)
                {
                    if (Package.Diagrams.Count == 1)
                    {
                        EA.Diagram diagram = Package.Diagrams.GetAt(0);

                        if (diagram.MetaType != "XIS-Web_Diagrams::NavigationSpaceViewModel")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule111), EA.EnumMVErrorType.mvError, GetRuleStr(rule111));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule111), EA.EnumMVErrorType.mvError, GetRuleStr(rule111));
                        isValid = false;
                    }
                }
            }
        }

        //// XisAction with XisArguments
        //private void DoRule08(EA.Repository Repository, EA.Method method)
        //{
        //    if (method.Stereotype == "XisAction" && method.Parameters.Count > 0)
        //    {
        //        EA.Parameter parameter = null;
        //        bool valid = true;

        //        for (short i = 0; i < method.Parameters.Count; i++)
        //        {
        //            parameter = method.Parameters.GetAt(i);
        //            if (parameter.Stereotype != "XisArgument")
        //            {
        //                valid = false;
        //                break;
        //            }
        //        }

        //        if (!valid)
        //        {
        //            EA.Project Project = Repository.GetProjectInterface();
        //            Project.PublishResult(LookupMap(rule08), EA.EnumMVErrorType.mvError, GetRuleStr(rule08));
        //            isValid = false;
        //        }
        //    }
        //}
    }
}
