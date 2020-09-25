using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class MOtherLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "OtherLocation",
               columns: table => new
               {
                   Id = table.Column<string>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                   LocationName = table.Column<string>(maxLength: 256, nullable: true),
                   LocationAddress = table.Column<string>(maxLength: 256, nullable: true),
                   VendorId = table.Column<string>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("Id", x => x.Id);
                   table.ForeignKey(
                       name: "FK_OtherLocation_Vendor_VendorId",
                       column: x => x.VendorId,
                       principalTable: "Vendors",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherLocation");
        }
    }
}
