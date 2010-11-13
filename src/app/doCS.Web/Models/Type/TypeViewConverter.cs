using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using doCS.Web.Helpers;
using doCS.Models;

namespace doCS.Web.Models.Type {
	public class TypeViewConverter : TypeConverter<doCS.Models.Type, doCS.Web.Models.TypeView> {

		protected override TypeView ConvertCore(doCS.Models.Type source) {
			var properties = new List<Property>();
			//add the basic types
			var destination = new TypeView() {
				Id = source.Id,
				Name = NameHelper.GetNameFor(source),
				NamespaceId = source.Namespace.Id,
				NamespaceName = source.Namespace.Name,
				XmlDocumentation = (source.XmlDocumentation != null && source.XmlDocumentation.XmlComments != null) ? source.XmlDocumentation.XmlComments : "",
				AssemblyId = source.Assembly.Id,
				AssemblyName = source.Assembly.Name
			};
			//add the interfaces
			foreach (var interfac in source.Interfaces) {
				destination.Interfaces.Add(new TypeViewBase {
					Id = interfac.Id,
					Name = interfac.Name
				});
			}
			//add the inheritence heirarchy
			var baseType = source.BaseType;
			while(baseType != null) {
				TypeViewBase typeViewBase = new TypeViewBase() {
					Id = baseType.Id,
					Name = baseType.Name,
					NamespaceId = baseType.Namespace.Id,
					NamespaceName = baseType.Namespace.Name
				};
				//add base type interfaces
				foreach (var interfac in baseType.Interfaces) {
					typeViewBase.Interfaces.Add(new TypeViewBase {
						Id = interfac.Id,
						Name = interfac.Name
					});
				}
				destination.BaseClasses.Add(typeViewBase);
				baseType = baseType.BaseType;
			}
			destination.Properties = AddProperties(source);

			return destination;
		}

		private List<TypeView.Property> AddProperties(doCS.Models.Type type) {
			List<TypeView.Property> result = new List<TypeView.Property>();
			while (type != null) {
				foreach (var property in type.Properties) {
					result.Add(new TypeView.Property() {
						Id = property.Id,
						Name = property.Name,
						TypeId = property.Type.Id,
						TypeName = NameHelper.GetNameFor(property.Type),
						DeclaringTypeId = property.DeclaringType.Id,
						DeclaringTypeName = NameHelper.GetNameFor(property.DeclaringType),
						GetAccessor = NameHelper.GetNameFor(property.GetAccessType),
						SetAccessor = NameHelper.GetNameFor(property.SetAccessType),
						XmlDocumentation = property.XmlDocumentation != null ? property.XmlDocumentation.XmlComments : ""
					});
				}
				type = type.BaseType;
			}
			return result.OrderBy(x => x.Name).ToList();
		}

		class TypeInfo {
			List<Property> Properties { get; set; }
			List<string> Methods { get; set; }
			List<string> Members { get; set; }
		}
	}
}