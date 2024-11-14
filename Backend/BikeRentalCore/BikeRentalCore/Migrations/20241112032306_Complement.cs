using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalCore.Migrations
{
    /// <inheritdoc />
    public partial class Complement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "RentalPoints",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complement",
                table: "RentalPoints");
        }
    }
}
