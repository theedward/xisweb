package xismobile.pim.uml2.gen.html5.services;

import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

import org.eclipse.uml2.uml.Association;
import org.eclipse.uml2.uml.Class;
import org.eclipse.uml2.uml.Element;
import org.eclipse.uml2.uml.EnumerationLiteral;
import org.eclipse.uml2.uml.Generalization;
import org.eclipse.uml2.uml.Interface;
import org.eclipse.uml2.uml.InterfaceRealization;
import org.eclipse.uml2.uml.Model;
import org.eclipse.uml2.uml.Operation;
import org.eclipse.uml2.uml.Parameter;
import org.eclipse.uml2.uml.Property;
import org.eclipse.uml2.uml.Realization;
import org.eclipse.uml2.uml.Relationship;
import org.eclipse.uml2.uml.Stereotype;
import org.eclipse.uml2.uml.Type;

import xismobile.pim.uml2.gen.html5.main.Uml2Html5;
import xismobile.pim.uml2.gen.html5.services.ServiceUtils.MenuType;

public class Services {

	public List<String> getReferencedEntities(Class c) {
		List<String> entities = new ArrayList<String>();

		for (Association a : c.getAssociations()) {
			entities.add(a.getEndTypes().get(1).getName());
		}
		return entities;
	}

	public List<Property> getClassAssociationAttributes(Class c) {
		List<Type> endTypes = null;
		Property first = null;
		Property second = null;
		List<Property> memberEnds = null;
		List<Property> result = new ArrayList<Property>();

		for (Association a : c.getAssociations()) {
			endTypes = a.getEndTypes();
			memberEnds = a.getMemberEnds();
			first = memberEnds.get(0);
			second = memberEnds.get(1);
			if (!first.isNavigable() && !second.isNavigable()
					|| first.isNavigable() && second.isNavigable()) {
				if (endTypes.get(0).getName().equals(c.getName())) {
					result.add(second);
				} else {
					result.add(first);
				}
			} else if (first.isNavigable()) {
				if (endTypes.get(0).getName().equals(c.getName())) {
					result.add(first);
				}
			} else if (second.isNavigable()) {
				if (endTypes.get(1).getName().equals(c.getName())) {
					result.add(second);
				}
			}
		}
		return result;
	}

	public String getExtensionAndImplementations(Class c) {
		StringBuilder builder = new StringBuilder();
		List<Generalization> gens = c.getGeneralizations(); 

		if (gens.size() == 1 && ServiceUtils.isXisEntityInheritance(gens.get(0))) {
			Class cl = (Class) gens.get(0).getTargets()
					.get(0);
			builder.append(" extends ").append(cl.getName());
		}

		if (c.getInterfaceRealizations().size() > 0) {
			if (builder.length() > 0) {
				builder.append(" ");
			}
			builder.append("implements ");
			Interface i;
			for (InterfaceRealization it : c.getInterfaceRealizations()) {
				i = (Interface) it.getTargets().get(0);
				builder.append(i.getName()).append(", ");
			}
			builder.delete(builder.length() - 2, builder.length());
		}
		return builder.toString();
	}

	public List<String> getManyToManyAssociations(Class c) {
		List<String> joinEntities = new ArrayList<String>();
		Property first = null;
		Property second = null;

		for (Association a : c.getAssociations()) {
			first = a.getMemberEnds().get(0);
			second = a.getMemberEnds().get(1);
			if (first.upperBound() == -1 && second.upperBound() == -1) {
				if (a.getEndTypes().get(0).getName().equals(c.getName())) {
					joinEntities.add(ServiceUtils.toUpperFirst(a.getEndTypes().get(0)
							.getName())
							+ ServiceUtils.toUpperFirst(a.getEndTypes().get(1).getName()));
				}
			}
		}
		return joinEntities;
	}
	
	public List<Class> getMasterEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (ServiceUtils.isXisIS_BEAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (ServiceUtils.isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (ServiceUtils.isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (ServiceUtils.isXisBE_EntityMasterAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (ServiceUtils.isXisEntity((Class) first)) {
						entities.add((Class) first);
					} else if (ServiceUtils.isXisEntity((Class) second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}

	public List<Class> getDetailEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (ServiceUtils.isXisIS_BEAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (ServiceUtils.isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (ServiceUtils.isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (ServiceUtils.isXisBE_EntityDetailAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (ServiceUtils.isXisEntity((Class) first)) {
						entities.add((Class) first);
					} else if (ServiceUtils.isXisEntity((Class) second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}
	
	public List<Class> getReferenceEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (ServiceUtils.isXisIS_BEAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (ServiceUtils.isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (ServiceUtils.isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (ServiceUtils.isXisBE_EntityReferenceAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (ServiceUtils.isXisEntity((Class) first)) {
						entities.add((Class) first);
					} else if (ServiceUtils.isXisEntity((Class) second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}
	
	public List<Class> getInteractionSpaceReferencedEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		List<Association> assocs = new ArrayList<Association>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (ServiceUtils.isXisIS_BEAssociation(a)) {
				assocs.add(a);
			}
		}

		for (Association a : assocs) {
			first = a.getEndTypes().get(0);
			second = a.getEndTypes().get(1);
			if (ServiceUtils.isXisBusinessEntity(first)) {
				bes.add((Class) first);
			} else if (ServiceUtils.isXisBusinessEntity(second)) {
				bes.add((Class) second);
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (ServiceUtils.isXisBE_EntityMasterAssociation(a) || ServiceUtils.isXisBE_EntityDetailAssociation(a)
						|| ServiceUtils.isXisBE_EntityReferenceAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (ServiceUtils.isXisEntity((Class) first)) {
						entities.add((Class) first);
					} else if (ServiceUtils.isXisEntity((Class) second)) {
						entities.add((Class) second);
					}
				}
			}
		}

		return entities;
	}
	
	public List<Association> getInboundNavigationAssociations(Class c) {
		List<Association> associations = new ArrayList<Association>();

		for (Association a : c.getAssociations()) {
			if (ServiceUtils.isXisInteractionSpaceAssociation(a)) {
				Property first = a.getMemberEnds().get(0);
				Property second = a.getMemberEnds().get(1);
				if (first.isNavigable()) {
					if (a.getEndTypes().get(0).getName().equals(c.getName())) {
						associations.add(a);
					}
				}
				else if (second.isNavigable()) {
					if (a.getEndTypes().get(1).getName().equals(c.getName())) {
						associations.add(a);
					}
				}
			}
		}
		return associations;
	}

	public List<Operation> getInboundCrudOperations(Class space, String entity) {
		List<Operation> allOperations = new ArrayList<Operation>();
		List<Operation> operations = new ArrayList<Operation>();
		
		for (Association a : space.getAssociations()) {
			if (ServiceUtils.isXisInteractionSpaceAssociation(a)) {
				Property first = a.getMemberEnds().get(0);
				Property second = a.getMemberEnds().get(1);
				
				if (first.isNavigable()) {
					if (a.getEndTypes().get(0).getName().equals(space.getName())) {
						for (Element el : space.getModel().allOwnedElements()) {
							if (el instanceof Operation) {
								Operation o = (Operation) el;
								if (a.getName().equals(o.getName())
									&& ServiceUtils.isXisAction(o)
									&& ServiceUtils.isCrudAction(o)) {
									if (!allOperations.contains(o)) {
										allOperations.add(o);
									}
								}
							}
						}
					}
				} else if (second.isNavigable()) {
					if (a.getEndTypes().get(1).getName().equals(space.getName())) {
						for (Element el : space.getModel().allOwnedElements()) {
							if (el instanceof Operation) {
								Operation o = (Operation) el;
								if (a.getName().equals(o.getName())
									&& ServiceUtils.isXisAction(o)
									&& ServiceUtils.isCrudAction(o)) {
									if (!allOperations.contains(o)) {
										allOperations.add(o);
									}
								}
							}
						}
					}
				}
			}
		}
		
		for (Operation o : allOperations) {
			Class owner = (Class) o.getOwner();
			
			while (owner != null) {
				if (ServiceUtils.isXisForm(owner)
					&& ServiceUtils.getXisCompositeWidgetEntityName(owner,
							ServiceUtils.getXisForm(owner)) != null
				    && ServiceUtils.getXisCompositeWidgetEntityName(owner,
				    		ServiceUtils.getXisForm(owner))
				    		.equalsIgnoreCase(entity)) {
					operations.add(o);
					break;
				} else if (ServiceUtils.isXisList(owner)
						&& ServiceUtils.getXisCompositeWidgetEntityName(owner,
								ServiceUtils.getXisList(owner)) != null
						&& ServiceUtils.getXisCompositeWidgetEntityName(owner,
								ServiceUtils.getXisList(owner))
								.equalsIgnoreCase(entity)) {
					operations.add(o);
					break;
				} else if (ServiceUtils.isXisListGroup(owner)
						&& ServiceUtils.getXisCompositeWidgetEntityName(owner,
								ServiceUtils.getXisListGroup(owner)) != null
						&& ServiceUtils.getXisCompositeWidgetEntityName(owner,
								ServiceUtils.getXisListGroup(owner))
								.equalsIgnoreCase(entity)) {
					operations.add(o);
					break;
				} else if (ServiceUtils.isXisMenu(owner)
						&& ServiceUtils.getXisCompositeWidgetEntityName(owner,
								ServiceUtils.getXisMenu(owner)) != null
						&& ServiceUtils.getXisCompositeWidgetEntityName(owner,
								ServiceUtils.getXisMenu(owner))
								.equalsIgnoreCase(entity)) {
					operations.add(o);
					break;
				} else if (ServiceUtils.isXisInteractionSpace(owner)) {
					for (Parameter p : o.getOwnedParameters()) {
						if (p.getName().equalsIgnoreCase("entityName") &&
							!p.getDefault().isEmpty() && p.getDefault().equalsIgnoreCase(entity)) {
							operations.add(o);
							break;
						}
					}
				}

				if (owner.getOwner() instanceof Class) {
					owner = (Class) owner.getOwner();
				} else {
					break;
				}
			}
		}
		return operations;
	}

	public List<Operation> getInboundFilteredCrudOperations(Class space, String entity) {
		List<Operation> cruds = getInboundCrudOperations(space, entity);
		List<Operation> filtered = new ArrayList<Operation>();
		Map<String, Operation> map = new HashMap<String, Operation>();
		
		for (Operation o : cruds) { 
			Stereotype s = ServiceUtils.getXisAction(o);
			String type = ((EnumerationLiteral) o.getValue(s, "type")).getName();
			
			if (!map.containsKey(type)) {
				map.put(type, o);
			}
		}
		
		if (map.size() > 0) {
			filtered.addAll(map.values());
		}
		return filtered;
	}
	
	public List<Class> getInboundCrudOperationsEntities(Class c) {
		List<Operation> operations = new ArrayList<Operation>();
		List<String> entNames = new ArrayList<String>();
		List<Class> entities = new ArrayList<Class>();

		for (Association a : c.getAssociations()) {
			if (ServiceUtils.isXisInteractionSpaceAssociation(a)) {
				Property first = a.getMemberEnds().get(0);
				Property second = a.getMemberEnds().get(1);
				if (first.isNavigable()) {
					if (a.getEndTypes().get(0).getName().equals(c.getName())) {
						for (Element el : c.getModel().allOwnedElements()) {
							if (el instanceof Operation) {
								Operation o = (Operation) el;
								if (a.getName().equals(o.getName())
									&& ServiceUtils.isXisAction(o)
									&& ServiceUtils.isCrudAction(o)) {
									operations.add(o);
								}
							}
						}
					}
				}
				else if (second.isNavigable()) {
					if (a.getEndTypes().get(1).getName().equals(c.getName())) {
						for (Element el : c.getModel().allOwnedElements()) {
							if (el instanceof Operation) {
								Operation o = (Operation) el;
								if (a.getName().equals(o.getName())
									&& ServiceUtils.isXisAction(o)
									&& ServiceUtils.isCrudAction(o)) {
									operations.add(o);
								}
							}
						}
					}
				}
			}
		}
		
		for (Operation o : operations) {
			Class owner = (Class) o.getOwner();
			String entityName;
			
			while (owner != null) {
				if (ServiceUtils.isXisForm(owner) &&
				    ServiceUtils.getXisCompositeWidgetEntityName(owner,
						ServiceUtils.getXisForm(owner)) != null) {
					entityName = ServiceUtils.getXisCompositeWidgetEntityName(
							owner, ServiceUtils.getXisForm(owner));
					if (!entNames.contains(entityName)) {
						entNames.add(entityName);
						break;
					}
				} else if (ServiceUtils.isXisList(owner) &&
						   ServiceUtils.getXisCompositeWidgetEntityName(owner,
							   ServiceUtils.getXisList(owner)) != null) {
					entityName = ServiceUtils.getXisCompositeWidgetEntityName(
							owner, ServiceUtils.getXisList(owner));
					if (!entNames.contains(entityName)) {
						entNames.add(entityName);
						break;
					}
				} else if (ServiceUtils.isXisListGroup(owner) &&
						   ServiceUtils.getXisCompositeWidgetEntityName(owner,
								ServiceUtils.getXisListGroup(owner)) != null) {
					entityName = ServiceUtils.getXisCompositeWidgetEntityName(
							owner, ServiceUtils.getXisListGroup(owner));
					if (!entNames.contains(entityName)) {
						entNames.add(entityName);
						break;
					}
				} else if (ServiceUtils.isXisMenu(owner) &&
						   ServiceUtils.getXisCompositeWidgetEntityName(owner,
								ServiceUtils.getXisMenu(owner)) != null) {
					entityName = ServiceUtils.getXisCompositeWidgetEntityName(
							owner, ServiceUtils.getXisMenu(owner));
					if (!entNames.contains(entityName)) {
						entNames.add(entityName);
						break;
					}
				} else if (ServiceUtils.isXisInteractionSpace(owner)) {
					for (Parameter p : o.getOwnedParameters()) {
						if (p.getName().equalsIgnoreCase("entityName") &&
							!p.getDefault().isEmpty()) {
							if (!entNames.contains(p.getDefault())) {
								entNames.add(p.getDefault());
								break;
							}
						}
					}
				}

				if (owner.getOwner() instanceof Class) {
					owner = (Class) owner.getOwner();
				} else {
					break;
				}
			}
		}
		
		if (entNames.size() > 0) {
			for (String name : entNames) {
				for (Element el : c.getModel().allOwnedElements()) {
					if (el instanceof Class
						&& ((Class) el).getName().equalsIgnoreCase(name)
						&& ServiceUtils.isXisEntity((Class) el)) {
						entities.add((Class) el);
						break;
					}
				}
			}
		}
		return entities;
	}

	public boolean needsModes(Class space) {
		List<Operation> operations = new ArrayList<Operation>();
		boolean needsModes = false;
		
		for (Association a : space.getAssociations()) {
			if (ServiceUtils.isXisInteractionSpaceAssociation(a)) {
				Property first = a.getMemberEnds().get(0);
				Property second = a.getMemberEnds().get(1);
				
				if (first.isNavigable()) {
					if (a.getEndTypes().get(0).getName().equals(space.getName())) {
						for (Element el : space.getModel().allOwnedElements()) {
							if (el instanceof Operation) {
								Operation o = (Operation) el;
								if (a.getName().equals(o.getName())
									&& ServiceUtils.isXisAction(o)
									&& ServiceUtils.isCrudAction(o)) {
									if (!operations.contains(o)) {
										operations.add(o);
									}
								}
							}
						}
					}
				} else if (second.isNavigable()) {
					if (a.getEndTypes().get(1).getName().equals(space.getName())) {
						for (Element el : space.getModel().allOwnedElements()) {
							if (el instanceof Operation) {
								Operation o = (Operation) el;
								if (a.getName().equals(o.getName())
									&& ServiceUtils.isXisAction(o)
									&& ServiceUtils.isCrudAction(o)) {
									if (!operations.contains(o)) {
										operations.add(o);
									}
								}
							}
						}
					}
				}
			}
		}
		
		if (operations.size() > 0) {
			boolean hasCreate = false;
			boolean hasUpdate = false;
			
			for (Operation o : operations) {
				Stereotype s = ServiceUtils.getXisAction(o);
				String type = ((EnumerationLiteral) o.getValue(s, "type")).getName();
				
				if (type.equals("Create")) {
					hasCreate = true;
				} else if (type.equals("Update")) {
					hasUpdate = true;
				}
				
				if (hasCreate && hasUpdate) {
					needsModes = true;
					break;
				}
			}
		}
		return needsModes;
	}
	
	public Class getCrudOperationEntityContextWidget(Operation o) {
		Class widget = null;
		Class owner = (Class) o.getOwner();
		
		while (owner != null) {
			if (ServiceUtils.isXisForm(owner) &&
			    ServiceUtils.getXisCompositeWidgetEntityName(owner,
					ServiceUtils.getXisForm(owner)) != null) {
				widget = owner;
				break;
			} else if (ServiceUtils.isXisList(owner) &&
					   ServiceUtils.getXisCompositeWidgetEntityName(owner,
							ServiceUtils.getXisList(owner)) != null) {
				widget = owner;
				break;
			} else if (ServiceUtils.isXisListGroup(owner) &&
					   ServiceUtils.getXisCompositeWidgetEntityName(owner,
							ServiceUtils.getXisListGroup(owner)) != null) {
				widget = owner;
				break;
			} else if (ServiceUtils.isXisMenu(owner) &&
					   ServiceUtils.getXisCompositeWidgetEntityName(owner,
							ServiceUtils.getXisMenu(owner)) != null) {
				widget = owner;
				break;
			} else if (ServiceUtils.isXisInteractionSpace(owner)) {
				for (Parameter p : o.getOwnedParameters()) {
					if (p.getName().equalsIgnoreCase("entityName") &&
						!p.getDefault().isEmpty()) {
						widget = owner;
					}
				}
			}
			
			if (owner.getOwner() instanceof Class) {
				owner = (Class) owner.getOwner();
			} else {
				break;
			}
		}
		return widget;
	}

	public Class getCrudOperationEntity(Operation o, Class widget) {
		Class entity = null;
		String entityName = null;
		
		if (ServiceUtils.isXisForm(widget)) {
			entityName = ServiceUtils.getXisCompositeWidgetEntityName(widget,
					ServiceUtils.getXisForm(widget));
		} else if (ServiceUtils.isXisList(widget)) {
			entityName = ServiceUtils.getXisCompositeWidgetEntityName(widget,
					ServiceUtils.getXisList(widget));
		} else if (ServiceUtils.isXisListGroup(widget)) {
			entityName = ServiceUtils.getXisCompositeWidgetEntityName(widget,
					ServiceUtils.getXisListGroup(widget));
		} else if (ServiceUtils.isXisMenu(widget)) {
			entityName = ServiceUtils.getXisCompositeWidgetEntityName(widget,
					ServiceUtils.getXisMenu(widget));
		} else if (ServiceUtils.isXisInteractionSpace(widget)) {
			for (Parameter p : o.getOwnedParameters()) {
				if (p.getName().equalsIgnoreCase("entityName")) {
					entityName = p.getDefault();
					break;
				}
			}
		}
		
		if (entityName != null) {
			for (Element e : o.getModel().allOwnedElements()) {
				if (e instanceof Class
					&& ((Class) e).getName().equalsIgnoreCase(entityName)
					&& ServiceUtils.isXisEntity((Class) e)) {
					entity = (Class) e;
					break;
				}
			}
		}
		return entity;
	}
	
	public Class getXisListEntity(Class c) {
		Class entity = null;
		String entName = ServiceUtils.getXisCompositeWidgetEntityName(c, ServiceUtils.getXisList(c));
		
		for (Element el : c.getModel().allOwnedElements()) {
			if (el instanceof Class
				&& ((Class) el).getName().equalsIgnoreCase(entName)
				&& ServiceUtils.isXisEntity((Class) el)) {
				entity = (Class) el;
				break;
			}
		}
		return entity;
	}
	
	public List<Class> getAllOwnedListsEntities(Class c) {
		Map<String, Class> entNames = new HashMap<String, Class>();
		List<Class> entities = new ArrayList<Class>();
		
		for (Element el : c.allOwnedElements()) {
			if (el instanceof Class
				&& ServiceUtils.isXisList((Class) el)) {
				String name = ServiceUtils.getXisCompositeWidgetEntityName(
						(Class) el, ServiceUtils.getXisList((Class) el));
				if (name != null && !entNames.containsKey(name)) {
					entNames.put(name, (Class) el);
				}
			}
		}
		
		if (entNames.size() > 0) {
			Class entity = null;

			for (Class cl : entNames.values()) {
				entity = getXisListEntity(cl);
				if (entity != null) {
					entities.add(entity);
				}
			}
		}
		return entities;
	}
	
	public String getEntityAttributeOfWidget(String value) {
		return value.split("\\.")[1];
	}
	
	public String writeSetWidgetValue(Class c, Stereotype s, String name) {
		String ret = null;
		String attributeName = ServiceUtils.getXisSimpleWidgetEntityAttributeName(c, s);
		
		if (attributeName != null && attributeName.contains(".")) {
			if (ServiceUtils.isXisLabel(c) || ServiceUtils.isXisTextBox(c)
				|| ServiceUtils.isXisButton(c) || ServiceUtils.isXisLink(c)
				|| ServiceUtils.isXisDatePicker(c) || ServiceUtils.isXisTimePicker(c)
				|| ServiceUtils.isXisCheckBox(c)) {
				String[] attrArray = attributeName.split("\\.");
				ret = "if (";
				ret += ServiceUtils.toLowerFirst(attrArray[0]) + ".";
				ret += "get" + ServiceUtils.toUpperFirst(attrArray[1]) + "()";
				ret += " != null) {" + "\n\t";
				
				if (ServiceUtils.isXisCheckBox(c)) {
					ret += name + ".setChecked(Boolean.valueOf(";
					ret += ServiceUtils.toLowerFirst(attrArray[0]) + ".";
					ret += "get" + ServiceUtils.toUpperFirst(attrArray[1]) + "()));";
				} else {
					ret += name + ".setText(String.valueOf(";
					ret += ServiceUtils.toLowerFirst(attrArray[0]) + ".";
					ret += "get" + ServiceUtils.toUpperFirst(attrArray[1]) + "()));";
				}
				
				ret += "\n}";
				
				if (ServiceUtils.isXisDatePicker(c) || ServiceUtils.isXisTimePicker(c)) {
					ret += " else {\n\t";
					ret += name + ".setText(\"";
					ret += ServiceUtils.isXisDatePicker(c) ? "Date" : "Time";
					ret += "\");\n}";
				}
			}
		}
		return ret;
	}
	
	/**
	 * Copies the desired library jar into the target generation folder.
	 * 
	 * @param jar the library jar name
	 */
	public void addLibrary(String jar) {
		try {
			boolean jarExists = false;
			String target = Uml2Html5.targetFolderPath + "/libs/";
			JarFile srcFile = new JarFile(Uml2Html5.jarPath + "/AndroidGenerator.jar");
			File destFolder = new File(target);
			File destFile = new File(target + jar);
			
			if (!destFolder.exists()) {
				destFolder.mkdir();
			}
			
			Enumeration<JarEntry> entries = srcFile.entries();
			JarEntry entry = null;
			
			while (entries.hasMoreElements()) {
				entry = (JarEntry) entries.nextElement();
				if (entry.getName().equals("libs/" + jar)) {
					jarExists = true;
					break;
	            }
			}
			
			if (jarExists) {
				InputStream is = null;
				FileOutputStream os = null;
				
				try {
					is = srcFile.getInputStream(entry);
		            os = new FileOutputStream(destFile);
		            byte[] buffer = new byte[1024];
		            int length;
		            while ((length = is.read(buffer)) > 0) {
		                os.write(buffer, 0, length);
		            }
				} catch (Exception e) {
					e.printStackTrace();
				} finally {
					if (is != null) {
						is.close();
					}
					if (os != null) {
						os.close();
					}
					if (srcFile != null) {
						srcFile.close();
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * Copies the desired file into the target generation folder.
	 * 
	 * @param fileName the file name
	 * @param resolution the icon resolution ('low', 'medium', 'high' or 'xhigh')
	 */
	public void addDrawable(String fileName, String resolution) {
		try {
			boolean fileExists = false;
			String folder = getResolutionFolder(resolution);
			
			String target = Uml2Html5.targetFolderPath + "/res/" + folder + "/";
			JarFile srcFile = new JarFile(Uml2Html5.jarPath + "/AndroidGenerator.jar");
			File destFolder = new File(target);
			File destFile = new File(target + fileName);
			
			if (!destFolder.exists()) {
				destFolder.mkdirs();
			}
			
			Enumeration<JarEntry> entries = srcFile.entries();
			JarEntry entry = null;
			
			while (entries.hasMoreElements()) {
				entry = (JarEntry) entries.nextElement();
				if (entry.getName().equals("icons/" + folder + "/" + fileName)) {
					fileExists = true;
					break;
	            }
			}
			
			if (fileExists) {
				InputStream is = null;
				FileOutputStream os = null;
				
				try {
					is = srcFile.getInputStream(entry);
		            os = new FileOutputStream(destFile);
		            byte[] buffer = new byte[1024];
		            int length;
		            while ((length = is.read(buffer)) > 0) {
		                os.write(buffer, 0, length);
		            }
				} catch (Exception e) {
					e.printStackTrace();
				} finally {
					if (is != null) {
						is.close();
					}
					if (os != null) {
						os.close();
					}
					if (srcFile != null) {
						srcFile.close();
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * Stores the value of a string resource to be included in res/values/strings.xml.
	 * 
	 * @param key the name of the string resource
	 * @param value the value of the string resource
	 */
	public void addStringResource(String key, String value) {
		Uml2Html5.stringResources.put(key, value);
	}
	
	/**
	 * Retrieves all the string resources to be included in res/values/strings.xml.
	 * 
	 * @param m
	 * @return the string resources in a XML string
	 */
	public String getStringResources(Model m) {
		StringBuilder sb = new StringBuilder();
		String resource = "<string name=\"%s\">%s</string>\n\t"; 
		
		for (String key : Uml2Html5.stringResources.keySet()) {
			sb.append(String.format(resource, key, Uml2Html5.stringResources.get(key)));
		}
		
		if (sb.length() > 0) {
			sb = sb.delete(sb.length()-2, sb.length()-1);
		}
		return sb.toString();
	}
	
	public Class getParentXisInteractionSpace(Class c) {
		Class space = null;
		Element p = c;

		if (ServiceUtils.isXisMenu(c) && c.getOwner() == null) {
			for (Association a : c.getAssociations()) {
				if (ServiceUtils.isXisIS_MenuAssociation(a)) {
					Property first = a.getMemberEnds().get(0);
					Property second = a.getMemberEnds().get(1);
					
					if (!first.isNavigable()) {
						space = (Class) a.getEndTypes().get(0);
						break;
					} else if (!second.isNavigable()){
						space = (Class) a.getEndTypes().get(1);
						break;
					}
				}
			}
		}
		
		while (space == null) {
			p = p.getOwner();
			
			if (p != null) {
				if (p instanceof Class && ServiceUtils.isXisInteractionSpace((Class) p)) {
					space = (Class) p;
				}
			} else {
				break;
			}
		}
		return space;
	}
	
	public List<Class> getXisInteractionSpaceWidgets(Class c) {
		List<Class> widgets = new ArrayList<Class>();
		
		for (Element el : c.getOwnedElements()) {
			if (el instanceof Class) {
				Class w = (Class) el;
				
				if (ServiceUtils.isXisVisibilityBoundary(w)) {
					for (Element j : w.allOwnedElements()) {
						if (j instanceof Class) {
							Class child = (Class) j;
							
							if (!ServiceUtils.isXisMenu(child)
							&& !ServiceUtils.isXisMenuItem(child)
							&& !ServiceUtils.isXisListGroup(child)
							&& !ServiceUtils.isXisListItem(child)) {
								widgets.add(child);
							}
						}
					}
				} else if (!ServiceUtils.isXisMenu(w) && !ServiceUtils.isXisMenuItem(w)
						&& !ServiceUtils.isXisListGroup(w) && !ServiceUtils.isXisListItem(w)) {
					widgets.add(w);
				}
			}
		}
		return widgets;
	}
	
	/**
	 * Orders the widgets of a screen according to their position.
	 * 
	 * @param space the interaction space
	 * @param widgets list of the widgets to be ordered
	 * @return ordered list of widgets
	 */
	public List<Class> orderWidgetsByPosition(List<Class> widgets) {
		Collections.sort(widgets, new WidgetComparator());
		return widgets;
	}
	
	public String writeWidgetRelativePositioning(List<Class> widgets, Class c) {
		final String newLine = "\n\t";
		final int TOP = 40;
		final int BORDER = 10;
		StringBuilder sb = new StringBuilder();
		Class space = (Class) c.getOwner();
		Class form = null;
		
		while (!ServiceUtils.isXisInteractionSpace(space)) {
			if (ServiceUtils.isXisForm(space)) {
				form = space;
			}
			space = (Class) space.getOwner();
		}
		
		Class menu = ServiceUtils.geXisISOptionsMenu(space);
		Stereotype s = ServiceUtils.getXisInteractionSpace(space);
		int spaceX = ServiceUtils.getPosX(space, s);
		int spaceY = ServiceUtils.getPosY(space, s);
		int spaceWidth = ServiceUtils.getWidth(space, s);
		int spaceHeight = ServiceUtils.getHeight(space, s);
		int spaceLeft = spaceX - spaceWidth/2;
		int spaceRight = spaceX + spaceWidth/2;
		int spaceTop = spaceY - spaceHeight/2;
		int spaceBottom = 0;
		
		if (menu != null) {
			Stereotype menuS = ServiceUtils.getOptionsMenu(menu);
			int menuY = ServiceUtils.getPosY(menu, menuS);
			int menuHeight = ServiceUtils.getHeight(menu, menuS);
			spaceBottom = menuY - menuHeight/2;
		} else {
			spaceBottom = spaceY + spaceHeight/2;
		}
		
		if (form != null) {
			s = ServiceUtils.getXisForm(form);
			spaceX = ServiceUtils.getPosX(form, s);
			spaceY = ServiceUtils.getPosY(form, s);
			int formWidth = ServiceUtils.getWidth(form, s);
			int formHeight = ServiceUtils.getHeight(form, s);
			spaceLeft = spaceX - formWidth/2;
			spaceRight = spaceX + formWidth/2;
			spaceTop = spaceY - formHeight/2;
			spaceBottom = spaceY + formHeight/2;
		}
		
		s = ServiceUtils.getWidgetStereotype(c);
		int posX = ServiceUtils.getPosX(c, s);
		int posY = ServiceUtils.getPosY(c, s);
		int width = ServiceUtils.getWidth(c, s);
		int height = ServiceUtils.getHeight(c, s);
		int left = posX - width/2;
		int right = posX + width/2;
		int top = posY - height/2;
		int bottom = posY + height/2;
		
		List<Class> predecessors = new ArrayList<Class>();
		
		for (Class w : widgets) {
			if (w.getName().equals(c.getName())) {
				break;
			} else {
				predecessors.add(w);
			}
		}
		
		if (posX == spaceX) {
			sb.append("html5:layout_centerHorizontal=\"true\"");
		}
		
		if (predecessors.size() > 0) {
			if (sb.length() > 0) {
				// only Y is missing
				int closerTop = spaceTop;
				int closerMargin = top - spaceTop - TOP;
				Class closer = null;
				
				for (Class w : predecessors) {
					Stereotype wStereo = ServiceUtils.getWidgetStereotype(w);
					int wTop = ServiceUtils.getPosY(w, wStereo) - ServiceUtils.getHeight(w, wStereo)/2;
					
					if (wTop == top) {
						closerTop = wTop;
						closerMargin = 0;
						closer = w;
						break;
					} else if (wTop < top) {
						int wMargin = top - wTop;
						if (wMargin <= closerMargin) {
							closerTop = wTop;
							closerMargin = wMargin;
							closer = w;
						}
					}
				}
				
				if (closerTop == spaceTop) {
					sb.append(newLine + "html5:layout_alignParentTop=\"true\"");
				} else if (closerTop == (spaceTop + TOP) && closer == null) {
					sb.append(newLine + "html5:layout_below=\"@+id/label" + ServiceUtils.toUpperFirst(space.getName()) + "Title\"");
				} else {
					sb.append(newLine + "html5:layout_below=\"@+id/" + ServiceUtils.getWidgetName(closer) + "\"");
				}
				
				if (closerMargin > 0) {
					sb.append(newLine + "html5:layout_marginTop=\"" + (closerMargin*120/spaceHeight) + "dp\"");
				}
			} else {
				int closerLeft = spaceLeft;
				int closerTop = spaceTop;
				int closerMarginX = left - spaceLeft - BORDER;
				int closerMarginY = top - spaceTop - TOP;
				Class closerX = null;
				Class closerY = null;
				
				for (Class w : predecessors) {
					Stereotype wStereo = ServiceUtils.getWidgetStereotype(w);
					int wLeft = ServiceUtils.getPosX(w, wStereo) - ServiceUtils.getWidth(w, wStereo)/2;
					int wTop = ServiceUtils.getPosY(w, wStereo) - ServiceUtils.getHeight(w, wStereo)/2;
					
					if (wLeft == left) {
						closerLeft = wLeft;
						closerMarginX = 0;
						closerX = w;
						break;
					} else if (wLeft < left) {
						if ((left - wLeft) < closerMarginX) {
							closerLeft = wLeft;
							closerMarginX = left - wLeft;
							closerX = w;
						}
					}
					
					if (wTop == top) {
						closerTop = wTop;
						closerMarginY = 0;
						closerY = w;
						break;
					} else if (wTop < top) {
						if ((top - wTop) < closerMarginY) {
							closerTop = wTop;
							closerMarginY = top - wTop;
							closerY = w;
						}
					}
				}
				
				if (closerLeft == spaceLeft) {
					sb.append("html5:layout_alignParentLeft=\"true\"");
				} else {
					sb.append("html5:layout_alignLeft=\"@+id/" + ServiceUtils.getWidgetName(closerX) + "\"");
				}
				
				if (closerTop == spaceTop) {
					sb.append(newLine + "html5:layout_alignParentTop=\"true\"");
				} else if (closerTop == (spaceTop + TOP) && closerY == null) {
					sb.append(newLine + "html5:layout_below=\"@+id/label" + ServiceUtils.toUpperFirst(space.getName()) + "Title\"");
				} else {
					sb.append(newLine + "html5:layout_below=\"@+id/" + ServiceUtils.getWidgetName(closerY) + "\"");
				}
				
				if (closerMarginX > 0) {
					sb.append(newLine + "html5:layout_marginLeft=\"" + (closerMarginX*120/spaceWidth) + "dp\"");
				}
				
				if (closerMarginY > 0) {
					sb.append(newLine + "html5:layout_marginTop=\"" + (closerMarginY*120/spaceHeight) + "dp\"");
				}
			}
		} else {
			// Align with parent
			if (sb.length() > 0) {
				// only Y remaining
				int distTop = top - spaceTop - TOP;
				int distBottom = spaceBottom - bottom - BORDER;
				
				if (distTop <= distBottom) {
					sb.append(newLine + "html5:layout_alignParentTop=\"true\"");
					if (distTop > 0) {
						sb.append(newLine + "html5:layout_marginTop=\"" + (distTop*120/spaceHeight) + "dp\"");
					}
				} else {
					sb.append(newLine + "html5:layout_alignParentBottom=\"true\"");
					if (distBottom > 0) {
						sb.append(newLine + "html5:layout_marginBottom=\"" + (distBottom*120/spaceHeight)  + "dp\"");
					}
				}
			} else {
				int distLeft = left - spaceLeft;
				int distRight = right -spaceRight;
				int distTop = top - spaceTop - TOP;
				int distBottom = spaceBottom - bottom - BORDER;

				// Set X positioning
				if (distLeft <= distRight) {
					sb.append("html5:layout_alignParentLeft=\"true\"");
					if (distLeft > 10) {
						int margin = distLeft - BORDER;
						sb.append(newLine + "html5:layout_marginLeft=\"" + (margin*120/spaceWidth) + "dp\"");
					}
				} else {
					sb.append("html5:layout_alignParentRight=\"true\"");
					if (distRight > 10) {
						int margin = distRight - BORDER;
						sb.append(newLine + "html5:layout_marginRight=\"" + (margin*120/spaceWidth) + "dp\"");
					}
				}
				// Set Y positioning
				if (distTop <= distBottom) {
					sb.append(newLine + "html5:layout_alignParentTop=\"true\"");
					if (distTop > 0) {
						sb.append(newLine + "html5:layout_marginTop=\"" + (distTop*120/spaceHeight) + "dp\"");
					}
				} else {
					sb.append(newLine + "html5:layout_alignParentBottom=\"true\"");
					if (distBottom > 0) {
						sb.append(newLine + "html5:layout_marginBottom=\"" + (distBottom*120/spaceHeight) + "dp\"");
					}
				}
			}
		}
		return sb.toString();
	}
	
	/**
	 * Checks if an options menu exists in the interaction space.
	 * 
	 * @param c the interaction space
	 * @return true if there is an options menu, false otherwise
	 */
	public boolean hasOptionsMenu(Class c) {
		for (Element el : c.allOwnedElements()) {
			if (el instanceof Class && ServiceUtils.isXisMenu((Class)el)
				&& ServiceUtils.getOptionsMenu((Class)el) != null) {
				return true;
			}
		}
		return ServiceUtils.hasMenuFromMenuAssociation(c, MenuType.OptionsMenu);
	}
	
	/**
	 * Gets the options menu of the interaction space.
	 * 
	 * @param c the interaction space
	 * @return The options menu
	 */
	public Class getOptionsMenu(Class c) {
		for (Element el : c.allOwnedElements()) {
			if (el instanceof Class && ServiceUtils.isXisMenu((Class)el)
				&& ServiceUtils.getOptionsMenu((Class)el) != null) {
				return (Class)el;
			}
		}
		return ServiceUtils.getMenuFromMenuAssociation(c, MenuType.OptionsMenu);
	}
	
	/**
	 * Checks if a context menu exists in the interaction space.
	 * 
	 * @param c the interaction space
	 * @return true if there is a context menu, false otherwise
	 */
	public boolean hasContextMenu(Class c) {
		for (Element el : c.allOwnedElements()) {
			if (el instanceof Class && ServiceUtils.isXisMenu((Class)el)
				&& ServiceUtils.getContextMenu((Class)el) != null) {
				return true;
			}
		}
		return ServiceUtils.hasMenuFromMenuAssociation(c, MenuType.ContextMenu);
	}
	
	/**
	 * Gets the context menu of the interaction space.
	 * 
	 * @param c the interaction space
	 * @return The context menu
	 */
	public Class getContextMenu(Class c) {
		for (Element el : c.allOwnedElements()) {
			if (el instanceof Class && ServiceUtils.isXisMenu((Class)el)
				&& ServiceUtils.getContextMenu((Class)el) != null) {
				return (Class)el;
			}
		}
		return ServiceUtils.getMenuFromMenuAssociation(c, MenuType.ContextMenu);
	}
	
	public boolean hasXisDialogs(Class c) {
		if (c.getAssociations().size() > 0) {
			for (Association a : c.getAssociations()) {
				if (ServiceUtils.isXisIS_DialogAssociation(a)) {
					return true;
				}
			}
		}
		
		if (c.allOwnedElements().size() > 0) {
			List<Class> children = new ArrayList<Class>();
			for (Element e : c.allOwnedElements()) {
				if (e instanceof Class) {
					children.add((Class) e);
				}
			}
			
			if (children.size() > 0) {
				for (Class cl : children) {
					hasXisDialogs(cl);
				}
			}
		}
		return false;
	}
	
	public boolean xisRemoteServiceExists(Operation o) {
		if (o.getName().contains(".")) {
			String[] data = o.getName().split("\\.");
			Interface service = ServiceUtils.getXisRemoteServiceByName(data[0], o);
			return service != null
				&& ServiceUtils.getXisServiceMethodByName(data[1], service) != null;
		} else {
			return false;
		}
	}
	
	public String writeXisRemoteServiceFullName(String name) {
		String[] data = name.split("\\.");
		String server = ServiceUtils.toUpperFirst(data[0]) + "ServiceStub";
		String service = ServiceUtils.toLowerFirst(data[1]);
		return server + "." + service;
	}
	
	public boolean xisInternalServiceExists(Operation o) {
		if (o.getName().contains(".")) {
			String[] data = o.getName().split("\\.");
			Interface service = ServiceUtils.getXisInternalServiceByName(data[0], o);
			return service != null
				&& ServiceUtils.getXisServiceMethodByName(data[1], service) != null;
		} else {
			return false;
		}
	}
	
	public String writeXisInternalServiceFullName(Operation o) {
		String[] data = o.getName().split("\\.");
		String provider = "";
		Interface service = ServiceUtils.getXisInternalServiceByName(data[0], o);
		Class ip = null;
		
		for (Relationship r : service.getRelationships()) {
			if (r instanceof Realization
				&& ((Realization) r).getSources().size() == 1) {
				Element el = ((Realization) r).getSources().get(0);
				
				if (el instanceof Class
					&& ServiceUtils.isXisInternalProvider((Class) el)) {
					ip = (Class) el;
					provider = ip.getName();
				}
			}
		}
		
		if (ip != null) {
			if (ServiceUtils.isXisLocationProvider(ip)) {
				provider += "LocationProvider" + ".getInstance(getApplicationContext())." + data[1];
			} else if (ServiceUtils.isXisContactsProvider(ip)) {
				provider += "ContactsProvider" + ".getInstance(getApplicationContext())." + data[1];
			} else if (ServiceUtils.isXisCalendarProvider(ip)) {
				provider += "CalendarProvider" + ".getInstance(getApplicationContext())." + data[1];
			} else if (ServiceUtils.isXisMediaProvider(ip)) {
				provider += "MediaProvider" + ".getInstance(getApplicationContext())." + data[1];
			} else if (ServiceUtils.isXisCustomProvider(ip)) {
				provider += "CustomProvider" + ".getInstance(getApplicationContext())." + data[1];
			}
		}
		return provider;
	}
	
	public List<Interface> getXisInternalServices(Class c) {
		List<Interface> services = new ArrayList<Interface>();
		
		for (Relationship r : c.getRelationships()) {
			if (r instanceof Realization
				&& ((Realization) r).getTargets().size() == 1) {
				Element el = ((Realization) r).getTargets().get(0);
				
				if (el instanceof Interface
					&& ServiceUtils.isXisInternalService((Interface) el)) {
					services.add((Interface) el);
				}
			}
		}
		return services;
	}
	
	/**
	 * Auxiliary Methods
	 */
	
	/**
	 * Gets the name of the drawable folder according to its resolution.
	 * 
	 * @param resolution the resolution of the drawable
	 * @return the drawable folder name
	 */
	private String getResolutionFolder(String resolution) {
		String folder = "drawable";
		
		if (resolution.equalsIgnoreCase("low")) {
			return folder + "-ldpi";
		} else if (resolution.equalsIgnoreCase("medium")) {
			return folder + "-mdpi";
		} else if (resolution.equalsIgnoreCase("high")) {
			return folder + "-hdpi";
		} else if (resolution.equalsIgnoreCase("xhigh")) {
			return folder + "-xhdpi";
		} else {
			return folder;
		}
	}
	
	/**
	 * Compares XisWidgets according to their positions.
	 * 
	 * @author André Ribeiro
	 * @see Comparator
	 */
	class WidgetComparator implements Comparator<Class> {

		@Override
		public int compare(Class c1, Class c2) {
			Stereotype s1 = ServiceUtils.getWidgetStereotype(c1);
			Stereotype s2 = ServiceUtils.getWidgetStereotype(c2);
			int x1 = ServiceUtils.getPosX(c1, s1);
			int x2 = ServiceUtils.getPosX(c2, s2);
			int y1 = ServiceUtils.getPosY(c1, s1);
			int y2 = ServiceUtils.getPosY(c2, s2);

			if (y1 < y2) {
				return -y2;
			} else if (y1 > y2) {
				return y1;
			} else if (x1 < x2) {
				return -x2;
			} else if (x1 >= x2) {
				return x1;
			} else {
				return -x2;
			}
		}
	}
}
