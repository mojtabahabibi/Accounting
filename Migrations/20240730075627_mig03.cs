using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoBar.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class mig03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClose",
                table: "FinancialYears",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClose",
                table: "FinancialYears");
        }
    }
}
