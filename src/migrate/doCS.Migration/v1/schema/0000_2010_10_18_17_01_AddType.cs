using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMigrator;

namespace doCS.Migrations {
	[Migration(0000201010181701)]
	public class _0000_2010_10_18_17_01_AddType : Migration {

		public override void Up() {
			Create.Table("Type")
				.WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
				.WithColumn("NamespaceId").AsGuid().NotNullable()
				.WithColumn("Name").AsString(256).NotNullable().WithDefaultValue(null)
			;

		}

		public override void Down() {
			Delete.Table("Type");

		}

	}
}
