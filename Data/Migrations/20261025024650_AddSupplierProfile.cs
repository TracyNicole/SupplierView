using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierView.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusinessName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    OwnerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    BusinessAddress = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    BusinessDescription = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierProfiles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierProfiles");
        }
    }
}
