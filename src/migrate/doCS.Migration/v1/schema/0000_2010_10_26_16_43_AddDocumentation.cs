using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMigrator;

namespace doCS.Migrations {
	[Migration(0000201010261643)]
	public class _0000_2010_10_26_16_43_AddDocumentation : Migration {

		public override void Up() {
			Create.Table("XmlDocumentation")
				.WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
				.WithColumn("XmlComments").AsString(int.MaxValue).Nullable()
			;

			Create.Column("XmlDocumentationId").OnTable("Type").AsGuid().Nullable();
		}

		public override void Down() {
			throw new NotImplementedException();
		}

	}
}
