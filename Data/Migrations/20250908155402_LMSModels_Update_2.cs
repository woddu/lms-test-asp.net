using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMSModels_Update_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdvisorySectionId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "YearLevel",
                table: "Sections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdvisorySectionId",
                table: "AspNetUsers",
                column: "AdvisorySectionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdvisorySectionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YearLevel",
                table: "Sections");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdvisorySectionId",
                table: "AspNetUsers",
                column: "AdvisorySectionId");
        }
    }
}
