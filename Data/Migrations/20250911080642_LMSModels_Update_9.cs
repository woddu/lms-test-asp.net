using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMSModels_Update_9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sections_AdvisorySectionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Sections_SectionId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdvisorySectionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AdvisorySectionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "TeacherSubjects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "AdviserId",
                table: "Sections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_AdviserId",
                table: "Sections",
                column: "AdviserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_AspNetUsers_AdviserId",
                table: "Sections",
                column: "AdviserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Sections_SectionId",
                table: "TeacherSubjects",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_AspNetUsers_AdviserId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Sections_SectionId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Sections_AdviserId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "AdviserId",
                table: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "TeacherSubjects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdvisorySectionId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdvisorySectionId",
                table: "AspNetUsers",
                column: "AdvisorySectionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sections_AdvisorySectionId",
                table: "AspNetUsers",
                column: "AdvisorySectionId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Sections_SectionId",
                table: "TeacherSubjects",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
