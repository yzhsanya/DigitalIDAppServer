using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovDigitalApp.Infrastructure.Persistence.Migrations
{
    public partial class AddAllDocumentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Documents;");
            migrationBuilder.Sql("DELETE FROM DocumentOrders;");

            migrationBuilder.AddColumn<string>(
                name: "VisaType",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImmigrationStatus",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermitType",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseTwoFirstName",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseTwoLastName",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "VisaType", table: "Documents");
            migrationBuilder.DropColumn(name: "ImmigrationStatus", table: "Documents");
            migrationBuilder.DropColumn(name: "PermitType", table: "Documents");
            migrationBuilder.DropColumn(name: "SpouseTwoFirstName", table: "Documents");
            migrationBuilder.DropColumn(name: "SpouseTwoLastName", table: "Documents");
        }
    }
}
