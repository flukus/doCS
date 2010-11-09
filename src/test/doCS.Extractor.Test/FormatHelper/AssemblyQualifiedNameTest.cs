using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace doCS.Extractor.Test.FormatHelper {

	[TestFixture]
	public class AssemblyQualifiedNameTest {

		[Test]
		public void GetFullNameTest() {
			//test that, if given a fully qualified name, the full name will be returned
			//arrange
			//act
			string fullName = AssemblyQualifiedName.GetFullName("NHibernate.Engine.IBatcher, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//assert
			Assert.AreEqual("NHibernate.Engine.IBatcher", fullName);
		}

		[Test]
		public void GetNamespaceNameTest() {
			//test that, if given a fully qualified name, the namespace will be returned
			//arrange
			//act
			string namespaceName = AssemblyQualifiedName.GetNamespaceName("NHibernate.Engine.IBatcher, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//assert
			Assert.AreEqual("NHibernate.Engine", namespaceName);
		}

		[Test]
		public void GetNamespaceNameWithNestedType() {
			//arrange
			//act
			string namespaceName = AssemblyQualifiedName.GetNamespaceName("NHibernate.Dialect.Dialect+QuotedAndParenthesisStringTokenizer, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//assert
			Assert.AreEqual("NHibernate.Dialect", namespaceName);
		}

		[Test]
		public void GetAssemblyNameTest() {
			//test that, if given a fully qualified name, the assembly name will be returned
			//arrange
			string assemblyName = AssemblyQualifiedName.GetAssemblyName("NHibernate.Engine.IBatcher, NHibernateAssemblyName, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//act
			//assert
			Assert.AreEqual("NHibernateAssemblyName", assemblyName);
		}

		[Test]
		public void GetTypeNameTest() {
			//test that, if given a fully qualified name, the type name will be returned
			//arrange
			//act
			string typeName = AssemblyQualifiedName.GetTypeName("NHibernate.Engine.IBatcher, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//assert
			Assert.AreEqual("IBatcher", typeName);
		}

		[Test]
		public void GetTypeNameWithNestedTest() {
			//test that, if given a fully qualified name of a nested type, the type name will be returned
			//arrange
			//act
			string typeName = AssemblyQualifiedName.GetTypeName("NHibernate.Engine.IBatcher+NestedType, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//assert
			Assert.AreEqual("NestedType", typeName);
		}

		[Test]
		public void GetTypeNameWithGenericType() {
			//if the type is a generic type then the name should be return minus the `1/`2 on the end
			//arrange
			//act
			string namespaceName = AssemblyQualifiedName.GetTypeName("NHibernate.Type.GenericIdentifierBagType`1, NHibernate, Version=3.0.0.2001, Culture=neutral, PublicKeyToken=null");
			//assert
			Assert.AreEqual("GenericIdentifierBagType", namespaceName);
		}

		[Test]
		public void IsNestedTypeWithNestedTest() {
			//test that, if given a fully qualified name of a nested type, will return true
			//arrange
			//act
			bool result = AssemblyQualifiedName.IsNestedType("NHibernate.Engine.IBatcher+NestedType, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//assert
			Assert.IsTrue(result);
		}

		[Test]
		public void IsNestedTypeWithoutNestedTest() {
			//test that, if given a fully qualified name of a normal type, will return false
			//arrange
			//act
			bool result = AssemblyQualifiedName.IsNestedType("NHibernate.Engine.IBatcher, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//assert
			Assert.IsFalse(result);
		}

		[Test]
		public void GetDeclaringTypeName() {
			//test that, if given a fully qualified name of a nested type, the declaring (parent) type name will be returned
			//arrange
			//act
			string declaringTypeName = AssemblyQualifiedName.GetDeclaringTypeName("NHibernate.Engine.IBatcher+NestedType, NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4");
			//assert
			Assert.AreEqual("IBatcher", declaringTypeName);
		}

	}
}
