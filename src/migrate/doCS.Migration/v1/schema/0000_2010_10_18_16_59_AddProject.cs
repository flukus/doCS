using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentMigrator;

namespace doCS.Migrations {
	[Migration(0000201010181659)]
	public class _0000_2010_10_18_16_59_AddProject: Migration {

		public override void Up() {
			Create.Table("Project")
				.WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
				.WithColumn("Name").AsString(256).NotNullable().WithDefaultValue(null)
				.WithColumn("Version").AsString(32).NotNullable().WithDefaultValue(null)
			;

			Create.Table("ProjectSettings")
				.WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
				.WithColumn("IncludedAssemblies").AsString(int.MaxValue)
				.WithColumn("IncludedXmlFiles").AsString(int.MaxValue)
			;

			Create.ForeignKey().FromTable("ProjectSettings").ForeignColumn("Id").ToTable("Project").PrimaryColumn("Id");
		}

		public override void Down() {
			Delete.Column("ProjectId").FromTable("Namespace");
			Delete.Table("Project");
		}

	}
}
