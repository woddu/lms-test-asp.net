using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreFinalGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FinalGrade_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FinalGrade_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalGrade_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "FinalGrade_Second",
                table: "Scores");
        }
    }
}
