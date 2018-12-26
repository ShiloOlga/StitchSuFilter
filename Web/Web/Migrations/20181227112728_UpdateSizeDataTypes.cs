using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class UpdateSizeDataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "width",
                table: "patterns",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<short>(
                name: "height",
                table: "patterns",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<short>(
                name: "width_stitches",
                table: "kits",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "width_sm",
                table: "kits",
                type: "decimal(7,2)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<short>(
                name: "height_stitches",
                table: "kits",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "height_sm",
                table: "kits",
                type: "decimal(7,2)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "width",
                table: "patterns",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<sbyte>(
                name: "height",
                table: "patterns",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<sbyte>(
                name: "width_stitches",
                table: "kits",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<sbyte>(
                name: "width_sm",
                table: "kits",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "height_stitches",
                table: "kits",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<sbyte>(
                name: "height_sm",
                table: "kits",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)");
        }
    }
}
