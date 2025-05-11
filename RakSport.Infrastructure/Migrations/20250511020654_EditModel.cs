using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RakSport.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentFeatureEntity_ContentSectionEntity_ContentSectionId",
                table: "ContentFeatureEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentImageEntity_ContentSectionEntity_ContentSectionId",
                table: "ContentImageEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentSectionEntity_SportHallEntity_SportHallId",
                table: "ContentSectionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_HallFeatureEntity_SportHallEntity_SportHallId",
                table: "HallFeatureEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_HallImageEntity_SportHallEntity_SportHallId",
                table: "HallImageEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationEntity_AspNetUsers_UserId",
                table: "NotificationEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentEntity_ReservationEntity_ReservationId",
                table: "PaymentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationEntity_AspNetUsers_UserId",
                table: "ReservationEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationItemEntity_ReservationEntity_ReservationId",
                table: "ReservationItemEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationItemEntity_TimeSlotEntity_TimeSlotId",
                table: "ReservationItemEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewEntity_AspNetUsers_UserId",
                table: "ReviewEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewEntity_SportHallEntity_SportHallId",
                table: "ReviewEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntity_AspNetUsers_UserId",
                table: "TicketEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessageEntity_TicketEntity_TicketId",
                table: "TicketMessageEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlotEntity_SportHallEntity_SportHallId",
                table: "TimeSlotEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlotEntity",
                table: "TimeSlotEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketMessageEntity",
                table: "TicketMessageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketEntity",
                table: "TicketEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SportHallEntity",
                table: "SportHallEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewEntity",
                table: "ReviewEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationItemEntity",
                table: "ReservationItemEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationEntity",
                table: "ReservationEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentEntity",
                table: "PaymentEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationEntity",
                table: "NotificationEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HallImageEntity",
                table: "HallImageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HallFeatureEntity",
                table: "HallFeatureEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentSectionEntity",
                table: "ContentSectionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentImageEntity",
                table: "ContentImageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentFeatureEntity",
                table: "ContentFeatureEntity");

            migrationBuilder.DropColumn(
                name: "OtpCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpGeneratedAt",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TimeSlotEntity",
                newName: "TimeSlots");

            migrationBuilder.RenameTable(
                name: "TicketMessageEntity",
                newName: "TicketMessages");

            migrationBuilder.RenameTable(
                name: "TicketEntity",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "SportHallEntity",
                newName: "SportHalls");

            migrationBuilder.RenameTable(
                name: "ReviewEntity",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "ReservationItemEntity",
                newName: "ReservationItems");

            migrationBuilder.RenameTable(
                name: "ReservationEntity",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "PaymentEntity",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "NotificationEntity",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "HallImageEntity",
                newName: "HallImages");

            migrationBuilder.RenameTable(
                name: "HallFeatureEntity",
                newName: "HallFeatures");

            migrationBuilder.RenameTable(
                name: "ContentSectionEntity",
                newName: "ContentSections");

            migrationBuilder.RenameTable(
                name: "ContentImageEntity",
                newName: "ContentImages");

            migrationBuilder.RenameTable(
                name: "ContentFeatureEntity",
                newName: "ContentFeatures");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlotEntity_SportHallId",
                table: "TimeSlots",
                newName: "IX_TimeSlots_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketMessageEntity_TicketId",
                table: "TicketMessages",
                newName: "IX_TicketMessages_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketEntity_UserId",
                table: "Tickets",
                newName: "IX_Tickets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewEntity_UserId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewEntity_SportHallId",
                table: "Reviews",
                newName: "IX_Reviews_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationItemEntity_TimeSlotId",
                table: "ReservationItems",
                newName: "IX_ReservationItems_TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationItemEntity_ReservationId",
                table: "ReservationItems",
                newName: "IX_ReservationItems_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationEntity_UserId",
                table: "Reservations",
                newName: "IX_Reservations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentEntity_ReservationId",
                table: "Payments",
                newName: "IX_Payments_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationEntity_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HallImageEntity_SportHallId",
                table: "HallImages",
                newName: "IX_HallImages_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_HallFeatureEntity_SportHallId",
                table: "HallFeatures",
                newName: "IX_HallFeatures_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentSectionEntity_SportHallId",
                table: "ContentSections",
                newName: "IX_ContentSections_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentImageEntity_ContentSectionId",
                table: "ContentImages",
                newName: "IX_ContentImages_ContentSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentFeatureEntity_ContentSectionId",
                table: "ContentFeatures",
                newName: "IX_ContentFeatures_ContentSectionId");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlots",
                table: "TimeSlots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketMessages",
                table: "TicketMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportHalls",
                table: "SportHalls",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationItems",
                table: "ReservationItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HallImages",
                table: "HallImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HallFeatures",
                table: "HallFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentSections",
                table: "ContentSections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentImages",
                table: "ContentImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentFeatures",
                table: "ContentFeatures",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Faqs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtpCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    OtpGeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpCodes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContentFeatures_ContentSections_ContentSectionId",
                table: "ContentFeatures",
                column: "ContentSectionId",
                principalTable: "ContentSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentImages_ContentSections_ContentSectionId",
                table: "ContentImages",
                column: "ContentSectionId",
                principalTable: "ContentSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentSections_SportHalls_SportHallId",
                table: "ContentSections",
                column: "SportHallId",
                principalTable: "SportHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HallFeatures_SportHalls_SportHallId",
                table: "HallFeatures",
                column: "SportHallId",
                principalTable: "SportHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HallImages_SportHalls_SportHallId",
                table: "HallImages",
                column: "SportHallId",
                principalTable: "SportHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reservations_ReservationId",
                table: "Payments",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationItems_Reservations_ReservationId",
                table: "ReservationItems",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationItems_TimeSlots_TimeSlotId",
                table: "ReservationItems",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_SportHalls_SportHallId",
                table: "Reviews",
                column: "SportHallId",
                principalTable: "SportHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessages_Tickets_TicketId",
                table: "TicketMessages",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_SportHalls_SportHallId",
                table: "TimeSlots",
                column: "SportHallId",
                principalTable: "SportHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentFeatures_ContentSections_ContentSectionId",
                table: "ContentFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentImages_ContentSections_ContentSectionId",
                table: "ContentImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentSections_SportHalls_SportHallId",
                table: "ContentSections");

            migrationBuilder.DropForeignKey(
                name: "FK_HallFeatures_SportHalls_SportHallId",
                table: "HallFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_HallImages_SportHalls_SportHallId",
                table: "HallImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reservations_ReservationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationItems_Reservations_ReservationId",
                table: "ReservationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationItems_TimeSlots_TimeSlotId",
                table: "ReservationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_SportHalls_SportHallId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessages_Tickets_TicketId",
                table: "TicketMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_UserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_SportHalls_SportHallId",
                table: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Faqs");

            migrationBuilder.DropTable(
                name: "OtpCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlots",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketMessages",
                table: "TicketMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SportHalls",
                table: "SportHalls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationItems",
                table: "ReservationItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HallImages",
                table: "HallImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HallFeatures",
                table: "HallFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentSections",
                table: "ContentSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentImages",
                table: "ContentImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentFeatures",
                table: "ContentFeatures");

            migrationBuilder.RenameTable(
                name: "TimeSlots",
                newName: "TimeSlotEntity");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "TicketEntity");

            migrationBuilder.RenameTable(
                name: "TicketMessages",
                newName: "TicketMessageEntity");

            migrationBuilder.RenameTable(
                name: "SportHalls",
                newName: "SportHallEntity");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "ReviewEntity");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "ReservationEntity");

            migrationBuilder.RenameTable(
                name: "ReservationItems",
                newName: "ReservationItemEntity");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "PaymentEntity");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "NotificationEntity");

            migrationBuilder.RenameTable(
                name: "HallImages",
                newName: "HallImageEntity");

            migrationBuilder.RenameTable(
                name: "HallFeatures",
                newName: "HallFeatureEntity");

            migrationBuilder.RenameTable(
                name: "ContentSections",
                newName: "ContentSectionEntity");

            migrationBuilder.RenameTable(
                name: "ContentImages",
                newName: "ContentImageEntity");

            migrationBuilder.RenameTable(
                name: "ContentFeatures",
                newName: "ContentFeatureEntity");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlots_SportHallId",
                table: "TimeSlotEntity",
                newName: "IX_TimeSlotEntity_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_UserId",
                table: "TicketEntity",
                newName: "IX_TicketEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketMessages_TicketId",
                table: "TicketMessageEntity",
                newName: "IX_TicketMessageEntity_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "ReviewEntity",
                newName: "IX_ReviewEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_SportHallId",
                table: "ReviewEntity",
                newName: "IX_ReviewEntity_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId",
                table: "ReservationEntity",
                newName: "IX_ReservationEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationItems_TimeSlotId",
                table: "ReservationItemEntity",
                newName: "IX_ReservationItemEntity_TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationItems_ReservationId",
                table: "ReservationItemEntity",
                newName: "IX_ReservationItemEntity_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ReservationId",
                table: "PaymentEntity",
                newName: "IX_PaymentEntity_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "NotificationEntity",
                newName: "IX_NotificationEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HallImages_SportHallId",
                table: "HallImageEntity",
                newName: "IX_HallImageEntity_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_HallFeatures_SportHallId",
                table: "HallFeatureEntity",
                newName: "IX_HallFeatureEntity_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentSections_SportHallId",
                table: "ContentSectionEntity",
                newName: "IX_ContentSectionEntity_SportHallId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentImages_ContentSectionId",
                table: "ContentImageEntity",
                newName: "IX_ContentImageEntity_ContentSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentFeatures_ContentSectionId",
                table: "ContentFeatureEntity",
                newName: "IX_ContentFeatureEntity_ContentSectionId");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtpCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpGeneratedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlotEntity",
                table: "TimeSlotEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketEntity",
                table: "TicketEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketMessageEntity",
                table: "TicketMessageEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportHallEntity",
                table: "SportHallEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewEntity",
                table: "ReviewEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationEntity",
                table: "ReservationEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationItemEntity",
                table: "ReservationItemEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentEntity",
                table: "PaymentEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationEntity",
                table: "NotificationEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HallImageEntity",
                table: "HallImageEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HallFeatureEntity",
                table: "HallFeatureEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentSectionEntity",
                table: "ContentSectionEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentImageEntity",
                table: "ContentImageEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentFeatureEntity",
                table: "ContentFeatureEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentFeatureEntity_ContentSectionEntity_ContentSectionId",
                table: "ContentFeatureEntity",
                column: "ContentSectionId",
                principalTable: "ContentSectionEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentImageEntity_ContentSectionEntity_ContentSectionId",
                table: "ContentImageEntity",
                column: "ContentSectionId",
                principalTable: "ContentSectionEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentSectionEntity_SportHallEntity_SportHallId",
                table: "ContentSectionEntity",
                column: "SportHallId",
                principalTable: "SportHallEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HallFeatureEntity_SportHallEntity_SportHallId",
                table: "HallFeatureEntity",
                column: "SportHallId",
                principalTable: "SportHallEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HallImageEntity_SportHallEntity_SportHallId",
                table: "HallImageEntity",
                column: "SportHallId",
                principalTable: "SportHallEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationEntity_AspNetUsers_UserId",
                table: "NotificationEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentEntity_ReservationEntity_ReservationId",
                table: "PaymentEntity",
                column: "ReservationId",
                principalTable: "ReservationEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationEntity_AspNetUsers_UserId",
                table: "ReservationEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationItemEntity_ReservationEntity_ReservationId",
                table: "ReservationItemEntity",
                column: "ReservationId",
                principalTable: "ReservationEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationItemEntity_TimeSlotEntity_TimeSlotId",
                table: "ReservationItemEntity",
                column: "TimeSlotId",
                principalTable: "TimeSlotEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewEntity_AspNetUsers_UserId",
                table: "ReviewEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewEntity_SportHallEntity_SportHallId",
                table: "ReviewEntity",
                column: "SportHallId",
                principalTable: "SportHallEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntity_AspNetUsers_UserId",
                table: "TicketEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessageEntity_TicketEntity_TicketId",
                table: "TicketMessageEntity",
                column: "TicketId",
                principalTable: "TicketEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlotEntity_SportHallEntity_SportHallId",
                table: "TimeSlotEntity",
                column: "SportHallId",
                principalTable: "SportHallEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
