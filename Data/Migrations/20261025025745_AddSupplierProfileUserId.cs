using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierView.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierProfileUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SupplierProfiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SupplierProfiles");
        }
    }
}
