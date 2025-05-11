using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RakSport.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_GenerateOtpAt_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtpGeneratedAt",
                table: "OtpCodes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OtpGeneratedAt",
                table: "OtpCodes",
                type: "datetime2",
                nullable: true);
        }
    }
}
