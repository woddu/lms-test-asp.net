using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMSModels_Update_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Students_StudentId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Subjects_SubjectId",
                table: "Score");

            migrationBuilder.DropTable(
                name: "LMSUserSubject");

            migrationBuilder.DropTable(
                name: "SectionSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Score",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_SubjectId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "Exam",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT1",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT10",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT2",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT3",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT4",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT5",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT6",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT7",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT8",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PT9",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW1",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW10",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW2",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW3",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW4",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW5",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW6",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW7",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW8",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WW9",
                table: "Subjects");

            migrationBuilder.RenameTable(
                name: "Score",
                newName: "Scores");

            migrationBuilder.RenameIndex(
                name: "IX_Score_StudentId",
                table: "Scores",
                newName: "IX_Scores_StudentId");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Scores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    TeacherId = table.Column<string>(type: "TEXT", nullable: false),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    WW1 = table.Column<double>(type: "REAL", nullable: false),
                    WW2 = table.Column<double>(type: "REAL", nullable: false),
                    WW3 = table.Column<double>(type: "REAL", nullable: false),
                    WW4 = table.Column<double>(type: "REAL", nullable: false),
                    WW5 = table.Column<double>(type: "REAL", nullable: false),
                    WW6 = table.Column<double>(type: "REAL", nullable: false),
                    WW7 = table.Column<double>(type: "REAL", nullable: false),
                    WW8 = table.Column<double>(type: "REAL", nullable: false),
                    WW9 = table.Column<double>(type: "REAL", nullable: false),
                    WW10 = table.Column<double>(type: "REAL", nullable: false),
                    PT1 = table.Column<double>(type: "REAL", nullable: false),
                    PT2 = table.Column<double>(type: "REAL", nullable: false),
                    PT3 = table.Column<double>(type: "REAL", nullable: false),
                    PT4 = table.Column<double>(type: "REAL", nullable: false),
                    PT5 = table.Column<double>(type: "REAL", nullable: false),
                    PT6 = table.Column<double>(type: "REAL", nullable: false),
                    PT7 = table.Column<double>(type: "REAL", nullable: false),
                    PT8 = table.Column<double>(type: "REAL", nullable: false),
                    PT9 = table.Column<double>(type: "REAL", nullable: false),
                    PT10 = table.Column<double>(type: "REAL", nullable: false),
                    Exam = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => new { x.TeacherId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_TeacherId_SubjectId",
                table: "Scores",
                columns: new[] { "TeacherId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Students_StudentId",
                table: "Scores",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_TeacherSubjects_TeacherId_SubjectId",
                table: "Scores",
                columns: new[] { "TeacherId", "SubjectId" },
                principalTable: "TeacherSubjects",
                principalColumns: new[] { "TeacherId", "SubjectId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Students_StudentId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_TeacherSubjects_TeacherId_SubjectId",
                table: "Scores");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_TeacherId_SubjectId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Scores");

            migrationBuilder.RenameTable(
                name: "Scores",
                newName: "Score");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_StudentId",
                table: "Score",
                newName: "IX_Score_StudentId");

            migrationBuilder.AddColumn<double>(
                name: "Exam",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT1",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT10",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT2",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT3",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT4",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT5",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT6",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT7",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT8",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT9",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW1",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW10",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW2",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW3",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW4",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW5",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW6",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW7",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW8",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WW9",
                table: "Subjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Score",
                table: "Score",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LMSUserSubject",
                columns: table => new
                {
                    SubjectsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeacherId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LMSUserSubject", x => new { x.SubjectsId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_LMSUserSubject_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LMSUserSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionSubject",
                columns: table => new
                {
                    SectionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionSubject", x => new { x.SectionsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_SectionSubject_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Score_SubjectId",
                table: "Score",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LMSUserSubject_TeacherId",
                table: "LMSUserSubject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSubject_SubjectsId",
                table: "SectionSubject",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Students_StudentId",
                table: "Score",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Subjects_SubjectId",
                table: "Score",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
