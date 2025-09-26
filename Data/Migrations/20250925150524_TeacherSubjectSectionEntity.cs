using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class TeacherSubjectSectionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjectSections_SectionId_TeacherSubjectId",
                table: "TeacherSubjectSections");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "TeacherSubjectSections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectSections_SectionId_SubjectId",
                table: "TeacherSubjectSections",
                columns: new[] { "SectionId", "SubjectId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjectSections_SectionId_SubjectId",
                table: "TeacherSubjectSections");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "TeacherSubjectSections");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectSections_SectionId_TeacherSubjectId",
                table: "TeacherSubjectSections",
                columns: new[] { "SectionId", "TeacherSubjectId" },
                unique: true);
        }
    }
}
