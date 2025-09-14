using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewInitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Strand = table.Column<string>(type: "TEXT", nullable: false),
                    YearLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    AdviserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_AspNetUsers_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Track = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<char>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    SectionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    table.PrimaryKey("PK_TeacherSubjects", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeacherSubjectId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_TeacherSubjects_TeacherSubjectId",
                        column: x => x.TeacherSubjectId,
                        principalTable: "TeacherSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjectSections",
                columns: table => new
                {
                    SectionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeacherSubjectsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjectSections", x => new { x.SectionsId, x.TeacherSubjectsId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjectSections_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjectSections_TeacherSubjects_TeacherSubjectsId",
                        column: x => x.TeacherSubjectsId,
                        principalTable: "TeacherSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_StudentId",
                table: "Scores",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_TeacherSubjectId",
                table: "Scores",
                column: "TeacherSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_AdviserId",
                table: "Sections",
                column: "AdviserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SectionId",
                table: "Students",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectSections_TeacherSubjectsId",
                table: "TeacherSubjectSections",
                column: "TeacherSubjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "TeacherSubjectSections");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "AspNetUsers");
        }
    }
}
