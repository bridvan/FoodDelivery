using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Vendor_table_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Users_UserId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Vendors");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Vendors",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Vendors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Vendors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Number_Of_Location",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_of_Cuisine",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorsId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_AreaId",
                table: "Vendors",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CategoryId",
                table: "Vendors",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VendorsId",
                table: "Users",
                column: "VendorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Vendors_VendorsId",
                table: "Users",
                column: "VendorsId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Area_AreaId",
                table: "Vendors",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Category_CategoryId",
                table: "Vendors",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Vendors_VendorsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Area_AreaId",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Category_CategoryId",
                table: "Vendors");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_AreaId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_CategoryId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Users_VendorsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Number_Of_Location",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Type_of_Cuisine",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorsId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Vendors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Users_UserId",
                table: "Vendors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
