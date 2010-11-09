using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMigrator;

namespace doCS.Migrations {
	[Migration(0000201010191321)]
	public class _0000_2010_10_19_13_21_AddAssembly : Migration {

		public override void Up() {
			Create.Table("Assembly")
				.WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
				.WithColumn("ProjectId").AsGuid().NotNullable()
				.WithColumn("Name").AsString(256).NotNullable()
			;

			Create.Column("AssemblyId").OnTable("Type").AsGuid().Nullable();
		}

		public override void Down() {
			Delete.Column("AssemblyId").FromTable("Type");
			Delete.Table("Assembly");
		}

	}
}
