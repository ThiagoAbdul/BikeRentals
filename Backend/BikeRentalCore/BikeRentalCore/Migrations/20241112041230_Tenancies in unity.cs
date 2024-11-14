using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalCore.Migrations
{
    /// <inheritdoc />
    public partial class Tenanciesinunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ReturnDate",
                table: "Tenancies",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Tenancies");
        }
    }
}
