﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using doCS.Models;
using NHibernate;

namespace doCS.Web.Helpers.Implimentation.Extractor {
	public class ProjectUpdater {

		private List<Namespace> AllNamespaces { get; set; }
		private List<Namespace> _CurrentNamespaces;
		public IEnumerable<Namespace> CurrentNamespaces { get { return _CurrentNamespaces; } }

		private List<Assembly> AllAssemblies { get; set; }
		private List<Assembly> _CurrentAssemblies;
		public IEnumerable<Assembly> CurrentAssemblies { get { return _CurrentAssemblies; } }

		private List<doCS.Models.Type> AllTypes { get; set; }
		private List<doCS.Models.Type> _CurrentTypes;
		public IEnumerable<doCS.Models.Type> CurrentTypes { get { return _CurrentTypes; } }

		public ProjectUpdater() {
		}

		public void Initialize(IEnumerable<Namespace> allNamespaces, IEnumerable<Assembly> assemblies, IEnumerable<doCS.Models.Type> types) {
			AllNamespaces = new List<Namespace>(allNamespaces);
			_CurrentNamespaces = new List<Namespace>((int)(allNamespaces.Count() * 1.1));

			AllAssemblies = new List<Assembly>(assemblies);
			_CurrentAssemblies = new List<Assembly>((int)(assemblies.Count() * 1.1));

			AllTypes = new List<doCS.Models.Type>(types);
			_CurrentTypes = new List<doCS.Models.Type>((int)(types.Count() * 1.1));
		}

		public Namespace FindNamespaceByName(string name) {
			Namespace ns = _CurrentNamespaces.FirstOrDefault(x => x.Name == name);
			if (ns != null)
				return ns;
			ns = AllNamespaces.FirstOrDefault(x => x.Name == name);
			if (ns != null)
				_CurrentNamespaces.Add(ns);
			return ns;
		}

		public void AddNamespace(Namespace ns) {
			_CurrentNamespaces.Add(ns);
		}

		public IEnumerable<Namespace> GetRemovedNamespaces() {
			return AllNamespaces.Except(CurrentNamespaces);
		}

		public Assembly FindAssemblyByName(string name) {
			Assembly assembly = CurrentAssemblies.FirstOrDefault(x => x.Name == name);
			if (assembly != null)
				return assembly;
			assembly = AllAssemblies.FirstOrDefault(x => x.Name == name);
			if (assembly != null)
				_CurrentAssemblies.Add(assembly);
			return assembly;
		}

		public void AddAssembly(Assembly assembly) {
			_CurrentAssemblies.Add(assembly);
		}

		public IEnumerable<Assembly> GetRemovedAssemblies() {
			return AllAssemblies.Except(CurrentAssemblies);
		}

		public doCS.Models.Type FindTypeByName(string name, string assemblyName, string namespaceName) {
			doCS.Models.Type type = CurrentTypes.FirstOrDefault(x => x.Name == name && x.Assembly.Name == assemblyName && x.Namespace.Name == namespaceName);
			if (type != null)
				return type;
			type = AllTypes.FirstOrDefault(x => x.Name == name && x.Assembly.Name == assemblyName && x.Namespace.Name == namespaceName);
			if (type != null)
				_CurrentTypes.Add(type);
			return type;
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

		public void AddType(doCS.Models.Type type) {
			_CurrentTypes.Add(type);
		}

		public IEnumerable<doCS.Models.Type> GetRemovedTypes() {
			return AllTypes.Except(CurrentTypes);
		}

	}
}