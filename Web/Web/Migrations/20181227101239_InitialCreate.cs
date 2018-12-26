using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "content_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_types", x => x.id);
                    table.UniqueConstraint("AK_content_types_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "fabric_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabric_types", x => x.id);
                    table.UniqueConstraint("AK_fabric_types_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "kit_manufacturers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kit_manufacturers", x => x.id);
                    table.UniqueConstraint("AK_kit_manufacturers_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "pattern_authors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pattern_authors", x => x.id);
                    table.UniqueConstraint("AK_pattern_authors_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "thread_manufacturers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thread_manufacturers", x => x.id);
                    table.UniqueConstraint("AK_thread_manufacturers_name", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "fabrics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(64)", nullable: false),
                    count = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'16'"),
                    fabric_type_id = table.Column<int>(type: "int(11)", nullable: false),
                    content_type_id = table.Column<int>(type: "int(11)", nullable: false),
                    priority = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'3'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabrics", x => x.id);
                    table.UniqueConstraint("AK_fabrics_name_count", x => new { x.name, x.count });
                    table.ForeignKey(
                        name: "fk_content_type_id",
                        column: x => x.content_type_id,
                        principalTable: "content_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fabric_type_id",
                        column: x => x.fabric_type_id,
                        principalTable: "fabric_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patterns",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(64)", nullable: false),
                    item = table.Column<string>(type: "varchar(32)", nullable: false),
                    colors_count = table.Column<int>(type: "int(11)", nullable: false),
                    width = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    height = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    author_id = table.Column<int>(type: "int(11)", nullable: false),
                    image = table.Column<string>(type: "varchar(256)", nullable: false),
                    link = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patterns", x => x.id);
                    table.UniqueConstraint("AK_patterns_author_id_title", x => new { x.author_id, x.title });
                    table.ForeignKey(
                        name: "fk_pattern_author_id",
                        column: x => x.author_id,
                        principalTable: "pattern_authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thread_colors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    manufacturer_id = table.Column<int>(type: "int(11)", nullable: false),
                    sku = table.Column<string>(type: "varchar(16)", nullable: false),
                    color_id = table.Column<string>(type: "varchar(16)", nullable: false),
                    color_name = table.Column<string>(type: "varchar(64)", nullable: false),
                    rgb_color = table.Column<string>(type: "varchar(8)", nullable: true),
                    length = table.Column<sbyte>(type: "tinyint(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thread_colors", x => x.id);
                    table.UniqueConstraint("AK_thread_colors_manufacturer_id_color_id", x => new { x.manufacturer_id, x.color_id });
                    table.ForeignKey(
                        name: "fk_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalTable: "thread_manufacturers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fabric_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fabric_id = table.Column<int>(type: "int(11)", nullable: false),
                    sku = table.Column<string>(type: "varchar(16)", nullable: false),
                    color_id = table.Column<string>(type: "varchar(16)", nullable: false),
                    color_name = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabric_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_fabric_id",
                        column: x => x.fabric_id,
                        principalTable: "fabrics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thread_color_options",
                columns: table => new
                {
                    pattern_id = table.Column<int>(type: "int(11)", nullable: false),
                    thread_color_id = table.Column<int>(type: "int(11)", nullable: false),
                    required_length = table.Column<decimal>(type: "decimal(5,2) unsigned zerofill", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thread_color_options", x => new { x.pattern_id, x.thread_color_id });
                    table.ForeignKey(
                        name: "fk_thread_color_option_pattern_id",
                        column: x => x.pattern_id,
                        principalTable: "patterns",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_thread_color_option_thread_color_id",
                        column: x => x.thread_color_id,
                        principalTable: "thread_colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fabric_options",
                columns: table => new
                {
                    pattern_id = table.Column<int>(type: "int(11)", nullable: false),
                    fabric_item_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabric_options", x => new { x.pattern_id, x.fabric_item_id });
                    table.ForeignKey(
                        name: "fk_fabric_option_fabric_item_id",
                        column: x => x.fabric_item_id,
                        principalTable: "fabric_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_fabric_option_pattern_id",
                        column: x => x.pattern_id,
                        principalTable: "patterns",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kits",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    manufacturer_id = table.Column<int>(type: "int(11)", nullable: false),
                    title = table.Column<string>(type: "varchar(64)", nullable: false),
                    item = table.Column<string>(type: "varchar(32)", nullable: false),
                    thread_manufacturer_id = table.Column<int>(type: "int(11)", nullable: false),
                    fabric_item_id = table.Column<int>(type: "int(11)", nullable: false),
                    colors_count = table.Column<int>(type: "int(11)", nullable: false),
                    width_sm = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    height_sm = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    width_stitches = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    height_stitches = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    author_id = table.Column<int>(type: "int(11)", nullable: true),
                    image = table.Column<string>(type: "varchar(256)", nullable: false),
                    link = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kits", x => x.id);
                    table.UniqueConstraint("AK_kits_title_manufacturer_id", x => new { x.title, x.manufacturer_id });
                    table.ForeignKey(
                        name: "fk_kit_author_id",
                        column: x => x.author_id,
                        principalTable: "pattern_authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_kit_fabric_item_id",
                        column: x => x.fabric_item_id,
                        principalTable: "fabric_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_kit_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalTable: "kit_manufacturers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_kit_thread_manufacturer_id",
                        column: x => x.thread_manufacturer_id,
                        principalTable: "thread_manufacturers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "fk_fabric_id",
                table: "fabric_items",
                column: "fabric_id");

            migrationBuilder.CreateIndex(
                name: "fk_fabric_option_fabric_item_id",
                table: "fabric_options",
                column: "fabric_item_id");

            migrationBuilder.CreateIndex(
                name: "fk_content_type_id",
                table: "fabrics",
                column: "content_type_id");

            migrationBuilder.CreateIndex(
                name: "fk_fabric_type_id",
                table: "fabrics",
                column: "fabric_type_id");

            migrationBuilder.CreateIndex(
                name: "fk_kit_author_id",
                table: "kits",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "fk_kit_fabric_item_id",
                table: "kits",
                column: "fabric_item_id");

            migrationBuilder.CreateIndex(
                name: "fk_kit_manufacturer_id",
                table: "kits",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "fk_kit_thread_manufacturer_id",
                table: "kits",
                column: "thread_manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "fk_pattern_author_id",
                table: "patterns",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "fk_thread_color_option_thread_color_id",
                table: "thread_color_options",
                column: "thread_color_id");

            migrationBuilder.CreateIndex(
                name: "fk_manufacturer_id",
                table: "thread_colors",
                column: "manufacturer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fabric_options");

            migrationBuilder.DropTable(
                name: "kits");

            migrationBuilder.DropTable(
                name: "thread_color_options");

            migrationBuilder.DropTable(
                name: "fabric_items");

            migrationBuilder.DropTable(
                name: "kit_manufacturers");

            migrationBuilder.DropTable(
                name: "patterns");

            migrationBuilder.DropTable(
                name: "thread_colors");

            migrationBuilder.DropTable(
                name: "fabrics");

            migrationBuilder.DropTable(
                name: "pattern_authors");

            migrationBuilder.DropTable(
                name: "thread_manufacturers");

            migrationBuilder.DropTable(
                name: "content_types");

            migrationBuilder.DropTable(
                name: "fabric_types");
        }
    }
}
