using System;
using System.Collections.Generic;
using System.Text;

using FluentMigrator;

namespace doCS.Migrations {
	[Migration(0000201010181700)]
	public class _0000_2010_10_18_17_00_AddNamespace : Migration {
		public override void Up() {
			Create.Table("Namespace")
					.WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
					.WithColumn("ProjectId").AsGuid().NotNullable()
					.WithColumn("Name").AsString(256).NotNullable().WithDefaultValue(null)
			;
		}

		public override void Down() {
			Delete.Table("Namespace");
		}
	}
}
