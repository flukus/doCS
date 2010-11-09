using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Extractor {

	/// <summary>
	/// Helper class for extracting various parts from an Assembly Qualifed Name
	/// </summary>
	public static class AssemblyQualifiedName {

		/// <summary>
		/// Get the Full name of the type including the namespace
		/// <example>
		/// <code>
		/// AssemblyQualifiedName.GetFullName("NHibernate.Engine.IBatcher, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
		/// </code>
		/// This will return "NHibernate.Engine.IBatcher".
		/// </example>
		/// </summary>
		/// <param name="assemblyQualifiedName">A valid assembly qualified name</param>
		/// <returns>The Full name of the type (including namespace)</returns>
		public static string GetFullName(string assemblyQualifiedName) {
			string[] parts = GetParts(assemblyQualifiedName);
			return parts[0];
		}

		/// <summary>
		/// Get the namespace name of the type from an assembly qualified name
		/// </summary>
		/// <param name="assemblyQualifiedName">A valid assembly qualified name</param>
		/// <returns>The namespace name</returns>
		public static string GetNamespaceName(string assemblyQualifiedName) {
			string[] parts = GetParts(assemblyQualifiedName);
			int lastSeperator = parts[0].LastIndexOf('.');
			return parts[0].Substring(0, lastSeperator);
		}

		/// <summary>
		/// Gets the assembly that the type was compiled to.
		/// </summary>
		/// <remarks>This return the name of the assembly which is not a file name</remarks>
		/// <param name="assemblyQualifiedName">A valid assembly qualified name</param>
		/// <returns>The assembly name</returns>
		public static string GetAssemblyName(string assemblyQualifiedName) {
			string[] parts = GetParts(assemblyQualifiedName);
			return parts[1].Trim();
		}

		/// <summary>
		/// Gets the name of the type minus any namespace, or declaring type
		/// </summary>
		/// <param name="assemblyQualifiedName">A valid assembly qualified name</param>
		/// <returns>The type name</returns>
		public static string GetTypeName(string assemblyQualifiedName) {
			string[] parts = GetParts(assemblyQualifiedName);
			int lastSeperator = parts[0].LastIndexOf('.');
			string typeName = parts[0].Substring(lastSeperator + 1, parts[0].Length - lastSeperator - 1);
			if (typeName.Contains('+'))
				typeName = typeName.Substring(typeName.IndexOf('+') + 1, typeName.Length - typeName.IndexOf('+') - 1);
			int genericStart = typeName.IndexOf('`');
			if (genericStart > 0)
				typeName = typeName.Substring(0, genericStart);

			return typeName;
		}

		/// <summary>
		/// Returns whether or not this type was defined inside another type.
		/// </summary>
		/// <param name="assemblyQualifiedName">A valid assembly qualified name</param>
		/// <returns>True if this type was declared inside another type</returns>
		public static bool IsNestedType(string assemblyQualifiedName) {
			string[] parts = GetParts(assemblyQualifiedName);
			return parts[0].Contains('+');
		}

		/// <summary>
		/// Gets that outer type that this type is declared inside of.
		/// </summary>
		/// <exception cref="FormatException">
		/// Will throw a format exception if <see cref="IsNestedType"/> returns false.
		/// </exception>
		/// <param name="assemblyQualifiedName">A valid assembly qualified name that is known to be a nested type</param>
		/// <returns>The outer type</returns>
		public static string GetDeclaringTypeName(string assemblyQualifiedName) {
			if (!IsNestedType(assemblyQualifiedName))
				throw new FormatException("Argument 1 'assemblyQualifiedName' is not a nested type");
			string[] parts = GetParts(assemblyQualifiedName);
			int typeEndIndex = parts[0].IndexOf('+');
			int typeStartIndex = parts[0].LastIndexOf('.', typeEndIndex) + 1;
			return parts[0].Substring(typeStartIndex, parts[0].Length - typeEndIndex - 3);
		}

		private static string[] GetParts(string assemblyQualifiedName) {
			//TODO: throw exception here if invalid format
			return assemblyQualifiedName.Split(new string[] {", "}, StringSplitOptions.None);
		}

	}
}
