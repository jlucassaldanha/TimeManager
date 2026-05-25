using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndTenantArchitecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkJourneyRules_UserId",
                table: "WorkJourneyRules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRecords_UserId",
                table: "TimeRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAllowance_UserId",
                table: "TimeAllowance",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
