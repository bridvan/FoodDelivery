using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Cuisine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Areas_AreaId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_AreaId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Number_Of_Location",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Type_of_Cuisine",
                table: "Vendors");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Users",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Areas_AreaId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AreaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Number_Of_Location",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_of_Cuisine",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_AreaId",
                table: "Vendors",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Areas_AreaId",
                table: "Vendors",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
