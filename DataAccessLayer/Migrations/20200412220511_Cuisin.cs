using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Cuisin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Areas_AreaId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AreaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address_Location",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Address_Location",
                table: "Vendors",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Vendors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_Location = table.Column<string>(maxLength: 100, nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_AreaId",
                table: "Vendors",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DriverId",
                table: "Users",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AreaId",
                table: "Drivers",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Drivers_DriverId",
                table: "Users",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Areas_AreaId",
                table: "Vendors",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Drivers_DriverId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Areas_AreaId",
                table: "Vendors");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_AreaId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Users_DriverId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address_Location",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Address_Location",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AreaId",
                table: "Users",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Areas_AreaId",
                table: "Users",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
