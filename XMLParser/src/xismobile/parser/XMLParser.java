package xismobile.parser;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathExpression;
import javax.xml.xpath.XPathExpressionException;
import javax.xml.xpath.XPathFactory;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

public class XMLParser {

	private static String jarPath;
	
	public static void main(String[] args) {
		if (args.length < 3) {
			System.out.println("Arguments not valid : {jarPath, model, converted model}.");
		}
		else {
			jarPath = args[0];
			File f = new File(args[1]);
			try {
				DocumentBuilder builder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
				Document docEA = builder.parse(f);
				Element xmiElement = docEA.getDocumentElement();
				xmiElement.normalize();
	
				addAttributesToXMI(xmiElement);
	
				NodeList model = docEA.getElementsByTagName("uml:Model");
				
				if (model.getLength() == 1) {
					Element m = (Element) model.item(0);
					addProfileApplicationTag(builder, docEA, m);
					
					if (m.hasChildNodes()) {
						Element el = (Element) m.getChildNodes().item(1);
						m.setAttribute("name", el.getAttribute("name"));
					}
				}
	
				XPath path = XPathFactory.newInstance().newXPath();
//				XPathExpression expr = path.compile("/*/*/*/packagedElement[@name='EA_CommonTypes_Package']/packagedElement/generalization/general");
				XPathExpression expr = path.compile("/*/*/*/*/packagedElement[@name='EA_Java_Types_Package']/packagedElement");
				NodeList lst = (NodeList) expr.evaluate(docEA, XPathConstants.NODESET);
	
				if (lst.getLength() > 0) {
					for (int i = 0; i < lst.getLength(); i++) {
						Element aux = (Element) lst.item(i);
						String name = aux.getAttribute("name");
						
						if (aux.hasChildNodes()) {
							Element generalization = (Element) aux.getChildNodes().item(1);
							Element general = (Element) generalization.getChildNodes().item(1);
							general.setAttribute("xmi:type", "uml:PrimitiveType");
							setHref(general, name);
						} else {
							Element generalization = docEA.createElement("generalization");							
							generalization.setAttribute("xmi:type", "uml:Generalization");
							generalization.setAttribute("xmi:id", "EAJava_" + name + "_General");
							Element general = docEA.createElement("general");
							general.setAttribute("xmi:type", "uml:PrimitiveType");
							setHref(general, name);
							generalization.appendChild(general);
							aux.appendChild(generalization);
						}
					}
				}
				
				Element m = (Element) model.item(0);
				// get Types
				expr = path.compile("/*/*[local-name()='Extension']/primitivetypes/packagedElement");
				lst = (NodeList) expr.evaluate(docEA, XPathConstants.NODESET);
				Node n = lst.item(0);
				m.appendChild(n.cloneNode(true));

				computeWidgetPositionsAndSize(docEA);
	
				lst = docEA.getElementsByTagName("xmi:Extension");
				Node aux = lst.item(0);
				aux.getParentNode().removeChild(aux);
				lst = docEA.getElementsByTagName("uml:Profile");
				aux = lst.item(0);
				
				if (aux != null) {
					aux.getParentNode().removeChild(aux);
				}
	
				path = XPathFactory.newInstance().newXPath();
				expr = path.compile("/*/*/*[starts-with(name(), 'XIS')]");
				lst = (NodeList) expr.evaluate(docEA, XPathConstants.NODESET);
	
				if (lst.getLength() > 0) {
					Node parent = lst.item(0).getParentNode();
					for (int i = 0; i < lst.getLength(); i++) {
						aux = lst.item(i);
						if (aux.getNodeName().equals("XIS-Web:XisEntityAttribute")) {
							Element el = (Element) aux;
							if (el.hasAttribute("base_Attribute"))
							{
								el.setAttribute("base_Property", el.getAttribute("base_Attribute"));
								el.removeAttribute("base_Attribute");
	//							System.out.println(el.getAttribute("base_Property"));
							}
						}
						parent.removeChild(aux);
						xmiElement.appendChild(aux.cloneNode(true));
					}
				}
	
				File nFile = new File(args[2] + ".uml");
	
				Transformer transformer = TransformerFactory.newInstance().newTransformer();
				DOMSource source = new DOMSource(docEA);
				StreamResult result = new StreamResult(nFile);
	
				// StreamResult result2 = new StreamResult(System.out);
	
				transformer.setOutputProperty(OutputKeys.INDENT, "yes");
				transformer.setOutputProperty("{http://xml.apache.org/xslt}indent-amount", "2");
				transformer.transform(source, result);
				// transformer.transform(source, result2);
				System.out.println("Document successfully parsed!");
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}

	private static void addAttributesToXMI(Element xmiElement) {
		xmiElement.setAttribute("xmlns:uml", "http://www.eclipse.org/uml2/4.0.0/UML");
		xmiElement.setAttribute("xmlns:XIS-Web", "http:///schemas/XISWeb/_z1OHoP_wEeWfedY1jUNsBQ/15");
		xmiElement.setAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
		xmiElement.setAttribute("xmlns:ecore", "http://www.eclipse.org/emf/2002/Ecore");
		String schemaLocation = "http:///schemas/XISWeb/_z1OHoP_wEeWfedY1jUNsBQ/15";
		jarPath = jarPath.replace(" ", "%20");
		schemaLocation += " file:/" + jarPath + "/XIS-Web/model.profile.uml#_C0djsPv3EeWhDONV5HFNPQ";
		xmiElement.setAttribute("xsi:schemaLocation", schemaLocation);
	}

	private static void addProfileApplicationTag(DocumentBuilder builder,
			Document document, Element element) throws SAXException,
			IOException {
		InputStream f = XMLParser.class.getResourceAsStream("ProfileApplication.xml");
		Document doc = builder.parse(f);
		Element profile = doc.getDocumentElement();
		profile.normalize();
		element.appendChild(document.importNode(profile, true));
	}
	
	private static void setHref(Element el, String name) {
		String pathMap = "pathmap://UML_LIBRARIES/UMLPrimitiveTypes.library.uml#";
		
		if (name.toLowerCase().contains("int")) {
			el.setAttribute("href", pathMap + "Integer");
		} else if (name.toLowerCase().contains("double")) {
			el.setAttribute("href", pathMap + "UnlimitedNatural");
		} else if (name.toLowerCase().contains("float")) {
			el.setAttribute("href", pathMap + "UnlimitedNatural");
		} else if (name.toLowerCase().contains("short")) {
			el.setAttribute("href", pathMap + "UnlimitedNatural");
		} else if (name.toLowerCase().contains("long")) {
			el.setAttribute("href", pathMap + "UnlimitedNatural");
		} else if (name.toLowerCase().contains("boolean")) {
			el.setAttribute("href", pathMap + "Boolean");
		} else if (name.toLowerCase().contains("byte")) {
			el.setAttribute("href", pathMap + "Boolean");
		} else if (name.toLowerCase().contains("char")) {
			el.setAttribute("href", pathMap + "String");
		} else if (name.toLowerCase().contains("string")) {
			el.setAttribute("href", pathMap + "String");
		} else if (name.toLowerCase().contains("image")) {
			el.setAttribute("href", pathMap + "String");
		} else if (name.toLowerCase().contains("url")) {
			el.setAttribute("href", pathMap + "String");
		}
	}
	
	/*private static void addImportPrimitiveTypes(DocumentBuilder builder,
			Document document, Element element) throws SAXException,
			IOException {
		File f = new File("src/example/PrimitiveTypes.xml");
		Document doc = builder.parse(f);
		Element types = doc.getDocumentElement();
		types.normalize();
		element.appendChild(document.importNode(types, true));
	}*/

	private static Map<String, Element> getWidgetsMap(Document document) {
		Map<String, Element> widgets = new HashMap<String, Element>();

		addToMap(document.getElementsByTagName("XIS-Web:XisInteractionSpace"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisLabel"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisTextBox"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisCheckBox"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisRadioButton"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisInputCustom"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisImage"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisAudio"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisVideo"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisEmbed"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisIFrame"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisLabel"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisDropdownListItem"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisImage"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisButton"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisLink"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisDatePicker"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisTimePicker"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisMap"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisSiteMap"), widgets);
		addToMap(document.getElementsByTagName("XIS-Web:XisCompositeWidget"), widgets);
		
		return widgets.size() > 0 ? widgets : null;
	}

	private static void addToMap(NodeList lst, Map<String, Element> map) {
		if (lst != null && lst.getLength() > 0) {
			Element aux = null;
			for (int i = 0; i < lst.getLength(); i++) {
				aux = (Element) lst.item(i);
				map.put(aux.getAttribute("base_Class"), aux);
			}
		}
	}

	private static void computeWidgetPositionsAndSize(Document document) {
		Map<String, Element> widgets = getWidgetsMap(document);

		if (widgets != null) {
			XPath path = XPathFactory.newInstance().newXPath();
			try {
				XPathExpression expr = path
						.compile("/*[local-name()='XMI']/*[local-name()='Extension']/diagrams/diagram/elements/element");
				NodeList lst = (NodeList) expr.evaluate(document,
						XPathConstants.NODESET);
				
				if (lst != null) {
					Element aux = null;
					Element style2 = null;
					String id = null;
					String geometry = null;
					
					for (int i = 0; i < lst.getLength(); i++) {
						aux = (Element) lst.item(i);
						id = aux.getAttribute("subject");
						// Ignore NavigationSpace DiagramObjects
						style2 = (Element) aux.getParentNode().getParentNode().getChildNodes().item(9);
						
						if (!style2.getAttribute("value").contains("NavigationSpace")) {
							if (widgets.containsKey(id)) {
								geometry = aux.getAttribute("geometry");
								String[] pos = geometry.split("=|;");
								int left = Math.abs(Integer.parseInt(pos[1]));
								int top = Math.abs(Integer.parseInt(pos[3]));
								int right = Math.abs(Integer.parseInt(pos[5]));
								int bottom = Math.abs(Integer.parseInt(pos[7]));
								
								Integer posX = (left + right) / 2;
								Integer posY = (bottom + top) / 2;
								Integer width = right - left;
								Integer height = bottom - top;
								
								Element widget = widgets.get(id);
								widget.setAttribute("posX", posX.toString());
								widget.setAttribute("posY", posY.toString());
								widget.setAttribute("width", width.toString());
								widget.setAttribute("height", height.toString());
								// System.out.println(id+"-"+geometry+widgets.containsKey(id));
							}
						}
					}
				}
			} catch (XPathExpressionException e) {
				e.printStackTrace();
			}
		}
	}

}
