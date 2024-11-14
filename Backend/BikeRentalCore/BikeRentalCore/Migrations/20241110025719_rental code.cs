using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalCore.Migrations
{
    /// <inheritdoc />
    public partial class rentalcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RentalCode",
                table: "Tenancies",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalCode",
                table: "Tenancies");
        }
    }
}
