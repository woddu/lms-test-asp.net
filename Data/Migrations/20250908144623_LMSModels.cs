using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class LMSModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvisorySectionId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Track = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
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
                    SectionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    Exam = table.Column<double>(type: "REAL", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Score_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdvisorySectionId",
                table: "AspNetUsers",
                column: "AdvisorySectionId");

            migrationBuilder.CreateIndex(
                name: "IX_LMSUserSubject_TeacherId",
                table: "LMSUserSubject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_StudentId",
                table: "Score",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_SubjectId",
                table: "Score",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSubject_SubjectsId",
                table: "SectionSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SectionId",
                table: "Students",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sections_AdvisorySectionId",
                table: "AspNetUsers",
                column: "AdvisorySectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sections_AdvisorySectionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LMSUserSubject");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "SectionSubject");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdvisorySectionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AdvisorySectionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

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
