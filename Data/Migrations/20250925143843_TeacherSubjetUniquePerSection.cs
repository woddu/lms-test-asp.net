using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class TeacherSubjetUniquePerSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectSections_Sections_SectionsId",
                table: "TeacherSubjectSections");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectSections_TeacherSubjects_TeacherSubjectsId",
                table: "TeacherSubjectSections");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjectSections_TeacherSubjectsId",
                table: "TeacherSubjectSections");

            migrationBuilder.RenameColumn(
                name: "TeacherSubjectsId",
                table: "TeacherSubjectSections",
                newName: "SectionId");

            migrationBuilder.RenameColumn(
                name: "SectionsId",
                table: "TeacherSubjectSections",
                newName: "TeacherSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectSections_SectionId_TeacherSubjectId",
                table: "TeacherSubjectSections",
                columns: new[] { "SectionId", "TeacherSubjectId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectSections_Sections_SectionId",
                table: "TeacherSubjectSections",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectSections_TeacherSubjects_TeacherSubjectId",
                table: "TeacherSubjectSections",
                column: "TeacherSubjectId",
                principalTable: "TeacherSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectSections_Sections_SectionId",
                table: "TeacherSubjectSections");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectSections_TeacherSubjects_TeacherSubjectId",
                table: "TeacherSubjectSections");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjectSections_SectionId_TeacherSubjectId",
                table: "TeacherSubjectSections");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "TeacherSubjectSections",
                newName: "TeacherSubjectsId");

            migrationBuilder.RenameColumn(
                name: "TeacherSubjectId",
                table: "TeacherSubjectSections",
                newName: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectSections_TeacherSubjectsId",
                table: "TeacherSubjectSections",
                column: "TeacherSubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectSections_Sections_SectionsId",
                table: "TeacherSubjectSections",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectSections_TeacherSubjects_TeacherSubjectsId",
                table: "TeacherSubjectSections",
                column: "TeacherSubjectsId",
                principalTable: "TeacherSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
