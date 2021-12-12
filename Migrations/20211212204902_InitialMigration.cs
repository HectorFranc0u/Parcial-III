using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace webAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gift",
                columns: table => new
                {
                    GiftID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GiftName = table.Column<string>(type: "text", nullable: true),
                    GiftType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gift", x => x.GiftID);
                });

            migrationBuilder.CreateTable(
                name: "furnitures",
                columns: table => new
                {
                    FurId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FurName = table.Column<string>(type: "text", nullable: true),
                    FurType = table.Column<string>(type: "text", nullable: true),
                    FurMaterial = table.Column<string>(type: "text", nullable: true),
                    FurPrice = table.Column<int>(type: "integer", nullable: false),
                    GiftID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_furnitures", x => x.FurId);
                    table.ForeignKey(
                        name: "FK_furnitures_gift_GiftID",
                        column: x => x.GiftID,
                        principalTable: "gift",
                        principalColumn: "GiftID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_furnitures_GiftID",
                table: "furnitures",
                column: "GiftID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "furnitures");

            migrationBuilder.DropTable(
                name: "gift");
        }
    }
}
