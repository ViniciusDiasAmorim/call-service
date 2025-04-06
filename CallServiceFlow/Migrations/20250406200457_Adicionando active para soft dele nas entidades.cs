using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallServiceFlow.Migrations
{
    /// <inheritdoc />
    public partial class Adicionandoactiveparasoftdelenasentidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTickets",
                table: "Technicals");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Customers");

            migrationBuilder.AddColumn<short>(
                name: "ActiveTickets",
                table: "Technicals",
                type: "smallint",
                nullable: true);
        }
    }
}
