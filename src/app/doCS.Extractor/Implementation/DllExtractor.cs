﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace doCS.Extractor.Implementation {

	/// <summary>
	/// Extracts type/property/method information from a .Net dll and notifies the ExtractorContext
	/// </summary>
	public class DllExtractor {

		/// <summary>
		/// Extracts type/property/method information from a .Net dll and notifies the ExtractorContext
		/// </summary>
		public void Extract(ExtractorContext context) {
			//TODO: Either these assemblies or the the entire library should be loaded into a seperate AppDomain
			foreach (var dll in context.DependantAssemblies) {
				System.Reflection.Assembly.LoadFrom(dll.FullName);
			}
			foreach (var dll in context.Assemblies) {
				var assembly = System.Reflection.Assembly.LoadFrom(dll.FullName);
				ExtractTypes(context, assembly);
			}
		}

		/// <summary>
		/// Extracts all type information from the assembly. This method drills down and calls ExtractProperties etc.
		/// </summary>
		/// <param name="context">The context to send parameter information to</param>
		/// <param name="assembly">the assembly to scan</param>
		private void ExtractTypes(ExtractorContext context, Assembly assembly) {
			foreach (Type type in assembly.GetTypes()) {
				if (!type.FullName.StartsWith("<>") && !type.FullName.StartsWith("<PrivateImplementationDetails")) {
					TypeData typeInfo = new TypeData(type.AssemblyQualifiedName);
					//load the parent type
					if (type.BaseType != null && type.BaseType != typeof(Object))
						typeInfo.BaseTypeName = type.BaseType.AssemblyQualifiedName;

					foreach (var i in type.GetInterfaces()) {
						//only add interfaces that directly apply to the type, not base types
						if (type.BaseType != null && type.BaseType.GetInterface(i.Name) == null)
							typeInfo.Interfaces.Add(i.AssemblyQualifiedName);
					}

					ExtractGenericArguments(context, typeInfo, type);
					ExtractProperties(context, typeInfo, type);

					context.AddType(typeInfo);
				}
			}
		}

		/// <summary>
		/// extract the generic parameters for a type
		/// </summary>
		/// <param name="context"></param>
		/// <param name="typeInfo"></param>
		/// <param name="type"></param>
		private void ExtractGenericArguments(ExtractorContext context, TypeData typeInfo, Type type) {
			if (!type.IsGenericTypeDefinition)
				return;
			foreach (Type parameter in type.GetGenericArguments()) {
				typeInfo.GenericArguments.Add(new GenericArgumentData {
					Name = parameter.Name
				});
			}
		}

		/// <summary>
		/// Extract the properties for a type.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="typeInfo"></param>
		/// <param name="type"></param>
		private void ExtractProperties(ExtractorContext context, TypeData typeInfo, Type type) {
			//this should only get properties either directly on the type or ovverriden on the type. Properties of base classes should not be added
			PropertyInfo[] properties = type.GetProperties( BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			foreach (PropertyInfo property in properties) {
				PropertyData propertyData = new PropertyData() { 
					Name = property.Name,
					TypeName = property.PropertyType.AssemblyQualifiedName
				};
				context.AddProperty(typeInfo, propertyData);
			}
		}

		private void ExtractMethods(TypeData typeInfo, Type type) {
		}

	}

}