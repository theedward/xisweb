package xismobile.pim.uml2.gen.html5.services;

import org.eclipse.uml2.uml.Association;
import org.eclipse.uml2.uml.Class;
import org.eclipse.uml2.uml.Element;
import org.eclipse.uml2.uml.EnumerationLiteral;
import org.eclipse.uml2.uml.Generalization;
import org.eclipse.uml2.uml.Interface;
import org.eclipse.uml2.uml.Operation;
import org.eclipse.uml2.uml.Property;
import org.eclipse.uml2.uml.Stereotype;
import org.eclipse.uml2.uml.Type;

public final class ServiceUtils {

	// Suppress default constructor for non-instantiability
	private ServiceUtils() {
		throw new AssertionError();
	}
	
	public static boolean isXisInteractionSpace(Class c) {
		return getXisInteractionSpace(c) != null;
	}
	
	public static Stereotype getXisInteractionSpace(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisInteractionSpace");
	}
	
	public static boolean xisInteractionSpaceHasTitle(Class c) {
		Stereotype space = c.getAppliedStereotype("XIS-Mobile::XisInteractionSpace");
		if (space != null) {
			String title = (String) c.getValue(space, "title");
			return title != null && !title.isEmpty();
		} else {
			return false;
		}
	}
	
	public static Class geXisISOptionsMenu(Class c) {
		Class menu = null;
		for (Element el : c.getOwnedElements()) {
			if (el instanceof Class) {
				Class aux = (Class) el;
				if (getOptionsMenu(aux) != null) {
					menu = aux;
					break;
				}
			}
		}
		return menu;
	}
	
	public static boolean isXisAction(Operation o) {
		return getXisAction(o) != null;
	}
	
	public static Stereotype getXisAction(Operation o) {
		return o.getAppliedStereotype("XIS-Mobile::XisAction");
	}
	
	public static boolean isCrudAction(Operation o) {
		Stereotype action = getXisAction(o);
		EnumerationLiteral type = (EnumerationLiteral) o.getValue(action, "type");
		
		return type.getName().equals("Create") || type.getName().equals("Read")
			|| type.getName().equals("Update") || type.getName().equals("Delete")
			|| type.getName().equals("DeleteAll");
	}
	
	public static boolean isXisLabel(Class c) {
		return getXisLabel(c) != null;
	}
	
	public static Stereotype getXisLabel(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisLabel");
	}
	
	public static boolean isXisTextBox(Class c) {
		return getXisTextBox(c) != null;
	}

	public static Stereotype getXisTextBox(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisTextBox");
	}
	
	public static boolean isXisCheckBox(Class c) {
		return getXisCheckBox(c) != null;
	}
	
	public static Stereotype getXisCheckBox(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCheckBox");
	}
	
	public static boolean isXisImage(Class c) {
		return getXisImage(c) != null;
	}
	
	public static Stereotype getXisImage(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisImage");
	}
	
	public static boolean isXisButton(Class c) {
		return getXisButton(c) != null;
	}
	
	public static Stereotype getXisButton(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisButton");
	}
	
	public static boolean isXisLink(Class c) {
		return getXisLink(c) != null;
	}
	
	public static Stereotype getXisLink(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisLink");
	}
	
	public static boolean isXisDatePicker(Class c) {
		return getXisDatePicker(c) != null;
	}
	
	public static Stereotype getXisDatePicker(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisDatePicker");
	}
	
	public static boolean isXisTimePicker(Class c) {
		return getXisTimePicker(c) != null;
	}
	
	public static Stereotype getXisTimePicker(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisTimePicker");
	}
	
	public static boolean isXisWebView(Class c) {
		return getXisWebView(c) != null;
	}
	
	public static Stereotype getXisWebView(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisWebView");
	}
	
	public static boolean isXisMapView(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisMapView") != null;
	}
	
	public static Stereotype getXisMapView(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisMapView");
	}
	
	public static boolean isXisDropdown(Class c) {
		return getXisDropdown(c) != null;
	}
	
	public static Stereotype getXisDropdown(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisDropdown");
	}
	
	public static boolean isXisRadioButton(Class c) {
		return getXisRadioButton(c) != null;
	}
	
	public static Stereotype getXisRadioButton(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisRadioButton");
	}
	
	public static String getXisSimpleWidgetEntityAttributeName(Class c, Stereotype s) {
		String entityAttributeName = (String) c.getValue(s, "entityAttributeName");
		return entityAttributeName;
	}
	
	public static boolean isXisCompositeWidget(Class c) {
		return getXisCompositeWidget(c) != null;
	}
	
	public static Stereotype getXisCompositeWidget(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget");
	}
	
	public static String getXisCompositeWidgetEntityName(Class c, Stereotype s) {
		String entityName = (String) c.getValue(s, "entityName");
		return entityName;
	}
	
	public static Stereotype getXisForm(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisForm");
	}
	
	public static boolean isXisForm(Class c) {
		return getXisForm(c) != null;
	}
	
	public static Stereotype getXisMenu(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisMenu");
	}
	
	public static boolean isXisMenu(Class c) {
		return getXisMenu(c) != null;
	}
	
	public static Stereotype getXisMenuItem(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisMenuItem");
	}
	
	public static boolean isXisMenuItem(Class c) {
		return getXisMenuItem(c) != null;
	}
	
	public static Stereotype getOptionsMenu(Class c) {
		Stereotype menu = c.getAppliedStereotype("XIS-Mobile::XisMenu");
		if (menu != null) {
			EnumerationLiteral type = (EnumerationLiteral) c.getValue(menu, "type");
			if (type.getName().equals("OptionsMenu")) {
				return menu;
			} else {
				return null; 
			}
		} else {
			return null;
		}
	}
	
	public static Stereotype getContextMenu(Class c) {
		Stereotype menu = c.getAppliedStereotype("XIS-Mobile::XisMenu");
		if (menu != null) {
			EnumerationLiteral type = (EnumerationLiteral) c.getValue(menu, "type");
			if (type.getName().equals("ContextMenu")) {
				return menu;
			} else {
				return null; 
			}
		} else {
			return null;
		}
	}
	
	public static Stereotype getXisIS_MenuAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisIS-MenuAssociation");
	}
	
	public static boolean isXisIS_MenuAssociation(Association a) {
		return getXisIS_MenuAssociation(a) != null;
	}
	
	public static boolean hasMenuFromMenuAssociation(Class c, MenuType type) {
		for (Association a : c.getAssociations()) {
			if (isXisIS_MenuAssociation(a)) {
				Property first = a.getMemberEnds().get(0);
				Property second = a.getMemberEnds().get(1);
				if (first.isNavigable()) {
					if (type == MenuType.OptionsMenu) {
						if (getOptionsMenu((Class) a.getEndTypes().get(0)) != null) {
							return true;
						}
					} else if (type == MenuType.ContextMenu) {
						if (getContextMenu((Class) a.getEndTypes().get(0)) != null) {
							return true;
						}
					}
				}
				else if (second.isNavigable()) {
					if (type == MenuType.OptionsMenu) {
						if (getOptionsMenu((Class) a.getEndTypes().get(1)) != null) {
							return true;
						}
					} else if (type == MenuType.ContextMenu) {
						if (getContextMenu((Class) a.getEndTypes().get(1)) != null) {
							return true;
						}
					}
				}
			}
		}

		for (Element e : c.allOwnedElements()) {
			if (e instanceof Class) {
				if (hasMenuFromMenuAssociation((Class) e, type))
				{
					return true;
				}
			}
		}
		return false;
	}
	
	public static Class getMenuFromMenuAssociation(Class c, MenuType type) {
		for (Association a : c.getAssociations()) {
			if (isXisIS_MenuAssociation(a)) {
				Property first = a.getMemberEnds().get(0);
				Property second = a.getMemberEnds().get(1);
				if (first.isNavigable()) {
					if (type == MenuType.OptionsMenu) {
						if (getOptionsMenu((Class) a.getEndTypes().get(0)) != null) {
							return (Class) a.getEndTypes().get(0);
						}
					} else if (type == MenuType.ContextMenu) {
						if (getContextMenu((Class) a.getEndTypes().get(0)) != null) {
							return (Class) a.getEndTypes().get(0);
						}
					}
				}
				else if (second.isNavigable()) {
					if (type == MenuType.OptionsMenu) {
						if (getOptionsMenu((Class) a.getEndTypes().get(1)) != null) {
							return (Class) a.getEndTypes().get(1);
						}
					} else if (type == MenuType.ContextMenu) {
						if (getContextMenu((Class) a.getEndTypes().get(1)) != null) {
							return (Class) a.getEndTypes().get(1);
						}
					}
				}
			}
		}

		for (Element e : c.allOwnedElements()) {
			if (e instanceof Class) {
				Class menu = getMenuFromMenuAssociation((Class) e, type);
				if (menu != null) {
					return menu;
				}
			}
		}
		return null;
	}
	
	public static Stereotype getXisList(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisList");
	}
	
	public static boolean isXisList(Class c) {
		return getXisList(c) != null;
	}
	
	public static Stereotype getXisListGroup(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisListGroup");
	}
	
	public static boolean isXisListGroup(Class c) {
		return getXisListGroup(c) != null;
	}
	
	public static Stereotype getXisListItem(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisListItem");
	}
	
	public static boolean isXisListItem(Class c) {
		return getXisListItem(c) != null;
	}
	
	public static boolean isXisVisibilityBoundary(Class c) {
		return getXisVisibilityBoundary(c) != null;
	}
	
	public static Stereotype getXisVisibilityBoundary(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisVisibilityBoundary");
	}
	
	public static boolean isXisDialog(Class c) {
		return getXisDialog(c) != null;
	}
	
	public static Stereotype getXisDialog(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisDialog");
	}
	
	public static boolean isXisIS_DialogAssociation(Association a) {
		return getXisIS_DialogAssociation(a) != null;
	}
	
	public static Stereotype getXisIS_DialogAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisIS-DialogAssociation");
	}
	
	public static Stereotype getWidgetStereotype(Class c) {
		Stereotype s = null;
		
		if (isXisLabel(c)) {
			s = getXisLabel(c);
		} else if (isXisTextBox(c)) {
			s = getXisTextBox(c);
		} else if (isXisCheckBox(c)) {
			s = getXisCheckBox(c);
		} else if (isXisLink(c)) {
			s = getXisLink(c);
		} else if (isXisButton(c)) {
			s = getXisButton(c);
		} else if (isXisImage(c)) {
			s = getXisImage(c);
		} else if (isXisDatePicker(c)) {
			s = getXisDatePicker(c);
		} else if (isXisTimePicker(c)) {
			s = getXisTimePicker(c);
		} else if (isXisWebView(c)) {
			s = getXisWebView(c);
		} else if (isXisMapView(c)) {
			s = getXisMapView(c);
		} else if (isXisDropdown(c)) {
			s = getXisDropdown(c);
		} else if (isXisRadioButton(c)) {
			s = getXisRadioButton(c);
		} else if (isXisList(c)) {
			s = getXisList(c);
		} else if (isXisVisibilityBoundary(c)) {
			s = getXisVisibilityBoundary(c);
		} else if (isXisForm(c)) {
			s = getXisForm(c);
		}
		return s;
	}
	
	public static int getPosX(Class c, Stereotype s) {
		int posX = (int) c.getValue(s, "posX"); 
		return posX;
	}
	
	public static int getPosY(Class c, Stereotype s) {
		int posY = (int) c.getValue(s, "posY");
		return posY;
	}
	
	public static int getWidth(Class c, Stereotype s) {
		int width = (int) c.getValue(s, "width");
		return width;
	}
	
	public static int getHeight(Class c, Stereotype s) {
		int height = (int) c.getValue(s, "height");
		return height;
	}
	
	public static String getWidgetName(Class c) {
		String name = null;
		if (isXisLabel(c)) {
			name = "label";
		} else if (isXisTextBox(c)) {
			name = "textBox";
		} else if (isXisCheckBox(c)) {
			name = "checkBox";
		} else if (isXisLink(c)) {
			name = "link";
		} else if (isXisButton(c)) {
			name = "button";
		} else if (isXisImage(c)) {
			name = "image";
		} else if (isXisDatePicker(c)) {
			name = "datePicker";
		} else if (isXisTimePicker(c)) {
			name = "timePicker";
		} else if (isXisWebView(c)) {
			name = "webView";
		} else if (isXisMapView(c)) {
			name = "mapView";
		} else if (isXisDropdown(c)) {
			name = "dropdown";
		} else if (isXisRadioButton(c)) {
			name = "radioButton";
		} else if (isXisForm(c)) {
			name = "form";
		} else if (isXisCompositeWidget(c)) {
			// TODO: Check what cases should be considered
		}
		if (name != null && name.length() > 0) {
			name += toUpperFirst(c.getName());
		}
		return name;
	}
	
	public static boolean isXisEntity(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisEntity") != null;
	}
	
	public static boolean isXisEntityAttribute(Property p) {
		return p.getAppliedStereotype("XIS-Mobile::XisEntityAttribute") != null;
	}
	
	public static boolean isXisIS_BEAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisIS-BEAssociation") != null;
	}
	
	public static boolean isXisInteractionSpaceAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisInteractionSpaceAssociation") != null;
	}

	public static boolean isXisBusinessEntity(Type t) {
		return t.getAppliedStereotype("XIS-Mobile::XisBusinessEntity") != null;
	}

	public static boolean isXisBE_EntityMasterAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisBE-EntityMasterAssociation") != null;
	}

	public static boolean isXisBE_EntityDetailAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisBE-EntityDetailAssociation") != null;
	}

	public static boolean isXisBE_EntityReferenceAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisBE-EntityReferenceAssociation") != null;
	}
	
	public static boolean isXisEntityInheritance(Generalization g) {
		return g.getAppliedStereotype("XIS-Mobile::XisEntityInheritance") != null;
	}
	
	public static Stereotype getXisRemoteService(Interface i) {
		return i.getAppliedStereotype("XIS-Mobile::XisRemoteService");
	}
	
	public static boolean isXisRemoteService(Interface i) {
		return getXisRemoteService(i) != null;
	}
	
	public static Stereotype getXisInternalService(Interface i) {
		return i.getAppliedStereotype("XIS-Mobile::XisInternalService");
	}
	
	public static boolean isXisInternalService(Interface i) {
		return getXisInternalService(i) != null;
	}
	
	public static Stereotype getXisInternalProvider(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisInternalProvider");
	}
	
	public static boolean isXisInternalProvider(Class c) {
		return getXisInternalProvider(c) != null;
	}
	
	public static boolean isXisLocationProvider(Class c) {
		if (isXisInternalProvider(c)) {
			EnumerationLiteral type = (EnumerationLiteral) c.getValue(
				getXisInternalProvider(c), "type");
			return type.getName().equals("Location");			
		} else {
			return false;
		}
	}
	
	public static boolean isXisContactsProvider(Class c) {
		if (isXisInternalProvider(c)) {
			EnumerationLiteral type = (EnumerationLiteral) c.getValue(
				getXisInternalProvider(c), "type");
			return type.getName().equals("Contacts");			
		} else {
			return false;
		}
	}
	
	public static boolean isXisCalendarProvider(Class c) {
		if (isXisInternalProvider(c)) {
			EnumerationLiteral type = (EnumerationLiteral) c.getValue(
				getXisInternalProvider(c), "type");
			return type.getName().equals("Calendar");			
		} else {
			return false;
		}
	}
	
	public static boolean isXisMediaProvider(Class c) {
		if (isXisInternalProvider(c)) {
			EnumerationLiteral type = (EnumerationLiteral) c.getValue(
				getXisInternalProvider(c), "type");
			return type.getName().equals("Media");			
		} else {
			return false;
		}
	}
	
	public static boolean isXisCustomProvider(Class c) {
		if (isXisInternalProvider(c)) {
			EnumerationLiteral type = (EnumerationLiteral) c.getValue(
				getXisInternalProvider(c), "type");
			return type.getName().equals("Custom");			
		} else {
			return false;
		}
	}
	
	public static Stereotype getXisServiceMethod(Operation o) {
		return o.getAppliedStereotype("XIS-Mobile::XisServiceMethod");
	}
	
	public static boolean isXisServiceMethod(Operation o) {
		return getXisServiceMethod(o) != null;
	}
	
	public static Interface getXisRemoteServiceByName(String name, Operation o) {
		Interface service = null;
		
		for (Element el : o.getModel().allOwnedElements()) {
			if (el instanceof Interface
				&& ((Interface) el).getName().equalsIgnoreCase(name)
				&& isXisRemoteService((Interface) el)) {
				service = (Interface) el;
				return service;
			}
		}
		return service;
	}
	
	public static Interface getXisInternalServiceByName(String name, Operation o) {
		Interface service = null;
		
		for (Element el : o.getModel().allOwnedElements()) {
			if (el instanceof Interface
				&& ((Interface) el).getName().equalsIgnoreCase(name)
				&& isXisInternalService((Interface) el)) {
				service = (Interface) el;
				return service;
			}
		}
		return service;
	}
	
	public static Operation getXisServiceMethodByName(String name, Interface i) {
		Operation method = null;
		
		for (Operation o : i.getOperations()) {
			if (o.getName().equalsIgnoreCase(name)
				&& isXisServiceMethod(o)) {
				method = o;
				return method;
			}
		}
		return method;
	}
	
	/**
	 * Puts the first letter of a string in upper case and returns it.
	 * 
	 * @param s The original string 
	 * @return The string with the first letter in upper case
	 */
	public static String toUpperFirst(String s) {
		return s.substring(0, 1).toUpperCase() + s.substring(1);
	}
	
	/**
	 * Puts the first letter of a string in lower case and returns it.
	 * 
	 * @param s The original string 
	 * @return The string with the first letter in lower case
	 */
	public static String toLowerFirst(String s) {
		return s.substring(0, 1).toLowerCase() + s.substring(1);
	}
	
	enum MenuType {
		OptionsMenu,
		ContextMenu
	}
}
