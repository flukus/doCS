using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMigrator;

namespace doCS.Migrations {
	[Migration(0000201010281515)]
	public class _0000_2010_10_28_15_15_ExtendTypeRelations : Migration {

		public override void Up() {
			Create.Column("BaseTypeId").OnTable("Type").AsGuid().Nullable().WithDefaultValue(null);

			Create.Table("Type_Interfaces")
				.WithColumn("TypeId").AsGuid().NotNullable()
				.WithColumn("InterfaceId").AsGuid().NotNullable()
			;
		}

		public override void Down() {
			throw new NotImplementedException();
		}

	}
}
