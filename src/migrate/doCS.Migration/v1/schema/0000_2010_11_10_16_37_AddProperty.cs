using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentMigrator;

namespace doCS.Migrations {

	[Migration(0000201011101637)]
	public class _0000_2010_11_10_16_37_AddProperty : Migration {

		public override void Up() {
			Create.Table("Property")
				.WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
				.WithColumn("Name").AsString(256).NotNullable().WithDefaultValue(null)
				.WithColumn("DeclaredOnId").AsGuid().NotNullable().ForeignKey().References("FK_Property_DeclaredOn", "Type", new string[] { "Id"})
				.WithColumn("TypeId").AsGuid().NotNullable().ForeignKey().References("FK_Property_Type", "Type", new string[] { "Id"})
				.WithColumn("OverridesId").AsGuid().Nullable().ForeignKey().References("FK_Property_OverridesProperty", "Property", new string[] { "Id"})
				.WithColumn("ShadowsId").AsGuid().Nullable().ForeignKey().References("FK_Property_ShadowsProperty", "Property", new string[] { "Id"})
				.WithColumn("GetAccessType").AsInt32().NotNullable().WithDefaultValue(0)
				.WithColumn("SetAccessType").AsInt32().NotNullable().WithDefaultValue(0)
				.WithColumn("XmlDocumentationId").AsGuid().Nullable().ForeignKey().References("FK_Property_XmlDocumentation", "XmlDocumentation", new string[] { "Id" })
			;
		}

		public override void Down() {
			throw new NotImplementedException();
		}

	}

}
