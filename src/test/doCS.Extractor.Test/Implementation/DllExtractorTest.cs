using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Moq;

namespace doCS.Extractor.Implementation.Test {

	[TestFixture]
	public class DllExtractorTest {

		private DllExtractor DllExtractor { get; set; }

		private Mock<IExtractorCollector> MockExtractorCollector { get; set; }

		private TypeData BasicTypeData { get; set; }

		[SetUp]
		public void Setup() {
			DllExtractor = new DllExtractor();

			MockExtractorCollector = new Mock<IExtractorCollector>();
			MockExtractorCollector.Setup(x => x.AddProperty(It.IsAny<TypeData>(), It.IsAny<PropertyData>()));

			BasicTypeData = new TypeData("");
		}

		[Test]
		public void TestPropertyAdded() {
			//test Extractor collector is notified of a basic property
			//arrange
			//act
			DllExtractor.ExtractProperties(MockExtractorCollector.Object, BasicTypeData, typeof(doCS.TestLibrary.SimpleClasses.NormalFlatClass));
			//assert
			MockExtractorCollector.Verify(x => x.AddProperty(BasicTypeData, It.Is<PropertyData>(p => p.Name == "Id")), "Id property not added");
		}

		[Test]
		public void TestPropertyPublicAccessorSet() {
			//test that the get accessor of a public property is set
			//arrange
			//act
			DllExtractor.ExtractProperties(MockExtractorCollector.Object, BasicTypeData, typeof(doCS.TestLibrary.SimpleClasses.NormalFlatClass));
			//assert
			MockExtractorCollector.Verify(x => x.AddProperty(BasicTypeData, It.Is<PropertyData>(p => p.Name == "Id" && p.GetAccessType == AccessType.Public)), "GetAccessType not set to public");
		}

		[Test]
		public void TestPropertyProtectedAccessorSet() {
			//test that the get accessor of a protected property is set
			//arrange
			//act
			DllExtractor.ExtractProperties(MockExtractorCollector.Object, BasicTypeData, typeof(doCS.TestLibrary.SimpleClasses.ChildClass));
			//assert
			MockExtractorCollector.Verify(
				x => x.AddProperty(BasicTypeData, It.Is<PropertyData>(p => p.Name == "ProtectedProperty" && p.GetAccessType == AccessType.Protected)),
				"GetAccessType not set to Protected"
			);
		}

		[Test]
		public void TestPropertyPrivateAccessorSet() {
			//test that the get accessor of a private property is set
			//arrange
			//act
			DllExtractor.ExtractProperties(MockExtractorCollector.Object, BasicTypeData, typeof(doCS.TestLibrary.SimpleClasses.ChildClass));
			//assert
			MockExtractorCollector.Verify(
				x => x.AddProperty(BasicTypeData, It.Is<PropertyData>(p => p.Name == "PrivateProperty" && p.GetAccessType == AccessType.Private)),
				"GetAccessType not set to Private"
			);
		}


		[Test]
		public void TestPropertyInheritedPropertiesNotAdded() {
			//test that only properties on this type are returned, not inherited properties
			//arrange
			//act
			DllExtractor.ExtractProperties(MockExtractorCollector.Object, BasicTypeData, typeof(doCS.TestLibrary.SimpleClasses.ChildClass));
			//assert
			MockExtractorCollector.Verify(x => x.AddProperty(BasicTypeData, It.Is<PropertyData>(p => p.Name == "Name")), Times.Never(), "Name property from base type was added and it shouldn't be");
		}

		[Test]
		public void TestPropertyStaticSet() {
			//test that static gets set
			//arrange
			//act
			DllExtractor.ExtractProperties(MockExtractorCollector.Object, BasicTypeData, typeof(doCS.TestLibrary.SimpleClasses.ChildClass));
			//assert
			MockExtractorCollector.Verify(
				x => x.AddProperty(BasicTypeData, It.Is<PropertyData>(p => p.Name == "StaticProperty" && p.IsStatic)),
				Times.Once(),
				"static not set for static property, IsStatic should be true"
			);
		}


	}

}
