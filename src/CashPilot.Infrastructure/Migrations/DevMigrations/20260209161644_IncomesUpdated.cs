using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashPilot.Infrastructure.Migrations.DevMigrations
{
    /// <inheritdoc />
    public partial class IncomesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Incomes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Incomes");
        }
    }
}
