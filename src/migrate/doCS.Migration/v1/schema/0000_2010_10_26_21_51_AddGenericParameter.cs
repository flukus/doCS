using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMigrator;

namespace doCS.Migrations {
	[Migration(0000201010262151)]
	public class _0000_2010_10_26_21_51_AddGenericArgument : Migration {

		public override void Up() {
			Create.Table("GenericArgument")
				.WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
				.WithColumn("TypeId").AsGuid().NotNullable().WithDefaultValue(null)
				.WithColumn("Name").AsString(64).NotNullable().WithDefaultValue(null)
				.WithColumn("ArgumentOrder").AsInt16().NotNullable().WithDefaultValue(null)
			;
		}

		public override void Down() {
			throw new NotImplementedException();
		}

	}
}
