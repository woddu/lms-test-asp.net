using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMSModels_Update_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Track",
                table: "Sections");

            migrationBuilder.AddColumn<string>(
                name: "Track",
                table: "Subjects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Track",
                table: "Subjects");

            migrationBuilder.AddColumn<string>(
                name: "Track",
                table: "Sections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
