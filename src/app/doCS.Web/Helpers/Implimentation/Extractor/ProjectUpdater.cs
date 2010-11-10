using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using doCS.Models;
using NHibernate;

namespace doCS.Web.Helpers.Implimentation.Extractor {
	public class ProjectUpdater {

		private List<Namespace> AllNamespaces { get; set; }
		private readonly List<Namespace> _CurrentNamespaces;
		public IEnumerable<Namespace> CurrentNamespaces { get { return _CurrentNamespaces; } }

		private List<Assembly> AllAssemblies { get; set; }
		private readonly List<Assembly> _CurrentAssemblies;
		public IEnumerable<Assembly> CurrentAssemblies { get { return _CurrentAssemblies; } }

		private List<doCS.Models.Type> AllTypes { get; set; }
		private readonly List<doCS.Models.Type> _CurrentTypes;
		public IEnumerable<doCS.Models.Type> CurrentTypes { get { return _CurrentTypes; } }

		private List<Property> AllProperties { get; set; }
		private readonly List<Property> _CurrentProperties;
		public IEnumerable<Property> CurrentProperties { get { return _CurrentProperties; } }

		public ProjectUpdater(IEnumerable<Namespace> allNamespaces, IEnumerable<Assembly> assemblies, IEnumerable<doCS.Models.Type> types, IEnumerable<Property> properties) {
			AllNamespaces = new List<Namespace>(allNamespaces);
			_CurrentNamespaces = new List<Namespace>((int)(allNamespaces.Count() * 1.1));

			AllAssemblies = new List<Assembly>(assemblies);
			_CurrentAssemblies = new List<Assembly>((int)(assemblies.Count() * 1.1));

			AllTypes = new List<doCS.Models.Type>(types);
			_CurrentTypes = new List<doCS.Models.Type>((int)(types.Count() * 1.1));

			AllProperties = new List<Property>(properties);
			_CurrentProperties = new List<Property>((int)(properties.Count() * 1.1));
		}

		public Namespace FindOrCreateNamespace(string namespaceName, Action<Namespace> namespaceAction) {
			//if the namespace is already current then return it, skipping the action
			Namespace ns = _CurrentNamespaces.FirstOrDefault(x => x.Name == namespaceName);
			if (ns != null)
				return ns;

			ns = AllNamespaces.FirstOrDefault(x => x.Name == namespaceName);
			if (ns == null)
				ns = new Namespace();

			namespaceAction(ns);
			_CurrentNamespaces.Add(ns);
			return ns;
		}

		public IEnumerable<Namespace> GetRemovedNamespaces() {
			return AllNamespaces.Except(CurrentNamespaces);
		}

		public Assembly FindOrCreateAssembly(string assemblyName, Action<Assembly> assemblyAction) {
			//if the assembly is already current then return it, skipping the action
			Assembly assembly = _CurrentAssemblies.FirstOrDefault(x => x.Name == assemblyName);
			if (assembly != null)
				return assembly;

			assembly = AllAssemblies.FirstOrDefault(x => x.Name == assemblyName);
			if (assembly == null)
				assembly = new Assembly();

			assemblyAction(assembly);
			_CurrentAssemblies.Add(assembly);
			return assembly;
		}

		public IEnumerable<Assembly> GetRemovedAssemblies() {
			return AllAssemblies.Except(CurrentAssemblies);
		}

		public doCS.Models.Type FindOrCreateType(string name, string assemblyName, string namespaceName, Action<doCS.Models.Type> typeAction) {
			doCS.Models.Type type = CurrentTypes.FirstOrDefault(x => x.Name == name && x.Assembly.Name == assemblyName && x.Namespace.Name == namespaceName);
			//if the type is already in the CurrentTypes list then it has been created/updated and typeAction will not be called
			if (type != null)
				return type;
			//get the existing type or create a new one
			type = AllTypes.FirstOrDefault(x => x.Name == name && x.Assembly.Name == assemblyName && x.Namespace.Name == namespaceName);
			if (type == null) {
				type = new doCS.Models.Type();
			}
			typeAction(type);

			//this is a new or updated type so add it to _CurrentTypes
			_CurrentTypes.Add(type);
			return type;
		} 

		public IEnumerable<doCS.Models.Type> GetRemovedTypes() {
			return AllTypes.Except(CurrentTypes);
		}

		public Property FindOrCreateProperty(string propertyName, doCS.Models.Type type, Action<Property> propertyAction) {
			//if the property is current then return it
			var property = _CurrentProperties.FirstOrDefault(x => x.Type == type && x.Name == propertyName);
			if (property != null)
				return property;

			property = type.Properties.FirstOrDefault(x => x.Name == propertyName);
			if (property == null)
				property = new Property();
			propertyAction(property);
			_CurrentProperties.Add(property);
			return property;
		}

		public IEnumerable<Property> GetRemovedProperties() {
			return AllProperties.Except(CurrentProperties);
		}

	}
}