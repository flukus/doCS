using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using doCS.Extractor.FormatHelper;

namespace doCS.Extractor.Test.FormatHelper {

	[TestFixture]
	public class ReferenceNameTest {

		[Test]
		public void IsTypeWithType() {
			//arrange
			//act
			bool result = ReferenceName.IsType("T:NHibernate.Type.SpecialOneToOneType");
			//assert
			Assert.IsTrue(result);
		}

		[Test]
		public void IsTypeWithProperty() {
			//arrange
			//act
			bool result = ReferenceName.IsType("M:NHibernate.Type.ICacheAssembler.Disassemble(System.Object,NHibernate.Engine.ISessionImplementor,System.Object)");
			//assert
			Assert.IsFalse(result);
		}

		[Test]
		public void IsPropertyWithProperty() {
			//arrange
			//act
			bool result = ReferenceName.IsProperty("P:NHibernate.Type.IType.Name");
			//assert
			Assert.IsTrue(result);
		}

		[Test]
		public void IsPropertyWithType() {
			//arrange
			//act
			bool result = ReferenceName.IsProperty("T:NHibernate.Type.SpecialOneToOneType");
			//assert
			Assert.IsFalse(result);
		}

		[Test]
		public void GetFullNameFromReferenceTest() {
			//arrange
			//act
			string fullname = ReferenceName.GetFullName("T:NHibernate.Type.SpecialOneToOneType");
			//assert
			Assert.AreEqual("NHibernate.Type.SpecialOneToOneType", fullname);
		}

		[Test]
		[ExpectedException(typeof(FormatException), ExpectedMessage = "The provided referenceName is not a valid type reference name")]
		public void GetFullNameFromIncorrectReferenceTest() {
			//if the provided name is not a type reference then an exception should be thrown
			//arrange
			//act
			string fullname = ReferenceName.GetFullName("P:NHibernate.Type.SpecialOneToOneType");
			//assert
		}

		[Test]
		public void GetPropertyNameTest() {
			//arrange
			//act
			string result = ReferenceName.GetPropertyName("P:NHibernate.Type.IType.Name");
			//assert
			Assert.AreEqual("Name", result);
		}

		[Test]
		public void GetFullNameFromProperty() {
			//test that the method returns the full name of the class that the property belongs to
			//arrange
			//act
			string result = ReferenceName.GetFullNameFromPropertyReference("P:NHibernate.Type.IType.Name");
			//assert
			Assert.AreEqual("NHibernate.Type.IType", result);
		}

	}

}
