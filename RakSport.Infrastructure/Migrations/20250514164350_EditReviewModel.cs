using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RakSport.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditReviewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Reviews",
                newName: "VentilationLightingRating");

            migrationBuilder.AddColumn<int>(
                name: "CleanLinessRating",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LockerRoomRating",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceToQualityRating",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StaffBehaviorRating",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReservationId",
                table: "Reviews",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reservations_ReservationId",
                table: "Reviews",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reservations_ReservationId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReservationId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CleanLinessRating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LockerRoomRating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PriceToQualityRating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "StaffBehaviorRating",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "VentilationLightingRating",
                table: "Reviews",
                newName: "Rating");
        }
    }
}
