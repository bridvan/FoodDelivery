using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class MAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Drivers_DriverId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Vendors_VendorsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DriverId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_VendorsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VendorsId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UniqueFileName",
                table: "Users",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueFileName",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DriverId",
                table: "Users",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VendorsId",
                table: "Users",
                column: "VendorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Drivers_DriverId",
                table: "Users",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Vendors_VendorsId",
                table: "Users",
                column: "VendorsId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
