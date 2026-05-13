using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeAllowance_Users_UserId",
                table: "TimeAllowance");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeRecords_Users_UserId",
                table: "TimeRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkJourneyRules_Users_UserId",
                table: "WorkJourneyRules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_WorkJourneyRules_UserId",
                table: "WorkJourneyRules");

            migrationBuilder.DropIndex(
                name: "IX_TimeRecords_UserId",
                table: "TimeRecords");

            migrationBuilder.DropIndex(
                name: "IX_TimeAllowance_UserId",
                table: "TimeAllowance");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkJourneyRules");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeAllowance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "WorkJourneyRules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TimeRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TimeAllowance",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkJourneyRules_UserId",
                table: "WorkJourneyRules",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeRecords_UserId",
                table: "TimeRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAllowance_UserId",
                table: "TimeAllowance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeAllowance_Users_UserId",
                table: "TimeAllowance",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRecords_Users_UserId",
                table: "TimeRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkJourneyRules_Users_UserId",
                table: "WorkJourneyRules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
