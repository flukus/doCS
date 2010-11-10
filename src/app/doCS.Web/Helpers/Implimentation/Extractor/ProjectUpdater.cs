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

		public ProjectUpdater(IEnumerable<Namespace> allNamespaces, IEnumerable<Assembly> assemblies, IEnumerable<doCS.Models.Type> types) {
			AllNamespaces = new List<Namespace>(allNamespaces);
			_CurrentNamespaces = new List<Namespace>((int)(allNamespaces.Count() * 1.1));

			AllAssemblies = new List<Assembly>(assemblies);
			_CurrentAssemblies = new List<Assembly>((int)(assemblies.Count() * 1.1));

			AllTypes = new List<doCS.Models.Type>(types);
			_CurrentTypes = new List<doCS.Models.Type>((int)(types.Count() * 1.1));
		}

		public Namespace FindOrCreateNamespace(string namespaceName, Action<Namespace> namespaceAction) {
			Namespace ns = _CurrentNamespaces.FirstOrDefault(x => x.Name == namespaceName);
			if (ns != null)
				return ns;
			ns = AllNamespaces.FirstOrDefault(x => x.Name == namespaceName);
			if (ns != null) {
				_CurrentNamespaces.Add(ns);
			} else {
				ns = new Namespace();
				namespaceAction(ns);
				_CurrentNamespaces.Add(ns);
			}
			return ns;
		}

		public IEnumerable<Namespace> GetRemovedNamespaces() {
			return AllNamespaces.Except(CurrentNamespaces);
		}

		public Assembly FindOrCreateAssembly(string assemblyName, Action<Assembly> assemblyAction) {
			Assembly assembly = _CurrentAssemblies.FirstOrDefault(x => x.Name == assemblyName);
			if (assembly != null)
				return assembly;
			assembly = AllAssemblies.FirstOrDefault(x => x.Name == assemblyName);
			if (assembly != null) {
				_CurrentAssemblies.Add(assembly);
			} else {
				assembly = new Assembly();
				assemblyAction(assembly);
				_CurrentAssemblies.Add(assembly);
			}
			return assembly;
		}

		public IEnumerable<Assembly> GetRemovedAssemblies() {
			return AllAssemblies.Except(CurrentAssemblies);
		}

		public doCS.Models.Type FindOrCreateType(string name, string assemblyName, string namespaceName, Action<doCS.Models.Type> typeAction) {
			bool isCurrent = false;
			doCS.Models.Type type = CurrentTypes.FirstOrDefault(x => x.Name == name && x.Assembly.Name == assemblyName && x.Namespace.Name == namespaceName);
			//if it's not already a current type then check AllTypes
			if (type == null) {
				type = AllTypes.FirstOrDefault(x => x.Name == name && x.Assembly.Name == assemblyName && x.Namespace.Name == namespaceName);
				if (type != null)
					_CurrentTypes.Add(type);
			} else
				isCurrent = true;
			//if we still don't have a type then create a new one
			if (type == null) {
				type = new doCS.Models.Type();
				_CurrentTypes.Add(type);
			}
			if (isCurrent == false)
				typeAction(type);
			return type;
		} 

		public IEnumerable<doCS.Models.Type> GetRemovedTypes() {
			return AllTypes.Except(CurrentTypes);
		}

	}
}