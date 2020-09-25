using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class CuisineTableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuisineId",
                table: "Vendors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfLocation",
                table: "Vendors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CuisineId",
                table: "Vendors",
                column: "CuisineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Cuisines_CuisineId",
                table: "Vendors",
                column: "CuisineId",
                principalTable: "Cuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Cuisines_CuisineId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_CuisineId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CuisineId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "NumberOfLocation",
                table: "Vendors");
        }
    }
}
