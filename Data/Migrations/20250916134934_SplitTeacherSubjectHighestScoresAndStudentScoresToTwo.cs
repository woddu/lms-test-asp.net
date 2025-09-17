using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class SplitTeacherSubjectHighestScoresAndStudentScoresToTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WW9",
                table: "TeacherSubjects",
                newName: "WW9_Second");

            migrationBuilder.RenameColumn(
                name: "WW8",
                table: "TeacherSubjects",
                newName: "WW9_First");

            migrationBuilder.RenameColumn(
                name: "WW7",
                table: "TeacherSubjects",
                newName: "WW8_Second");

            migrationBuilder.RenameColumn(
                name: "WW6",
                table: "TeacherSubjects",
                newName: "WW8_First");

            migrationBuilder.RenameColumn(
                name: "WW5",
                table: "TeacherSubjects",
                newName: "WW7_Second");

            migrationBuilder.RenameColumn(
                name: "WW4",
                table: "TeacherSubjects",
                newName: "WW7_First");

            migrationBuilder.RenameColumn(
                name: "WW3",
                table: "TeacherSubjects",
                newName: "WW6_Second");

            migrationBuilder.RenameColumn(
                name: "WW2",
                table: "TeacherSubjects",
                newName: "WW6_First");

            migrationBuilder.RenameColumn(
                name: "WW10",
                table: "TeacherSubjects",
                newName: "WW5_Second");

            migrationBuilder.RenameColumn(
                name: "WW1",
                table: "TeacherSubjects",
                newName: "WW5_First");

            migrationBuilder.RenameColumn(
                name: "PT9",
                table: "TeacherSubjects",
                newName: "WW4_Second");

            migrationBuilder.RenameColumn(
                name: "PT8",
                table: "TeacherSubjects",
                newName: "WW4_First");

            migrationBuilder.RenameColumn(
                name: "PT7",
                table: "TeacherSubjects",
                newName: "WW3_Second");

            migrationBuilder.RenameColumn(
                name: "PT6",
                table: "TeacherSubjects",
                newName: "WW3_First");

            migrationBuilder.RenameColumn(
                name: "PT5",
                table: "TeacherSubjects",
                newName: "WW2_Second");

            migrationBuilder.RenameColumn(
                name: "PT4",
                table: "TeacherSubjects",
                newName: "WW2_First");

            migrationBuilder.RenameColumn(
                name: "PT3",
                table: "TeacherSubjects",
                newName: "WW1_Second");

            migrationBuilder.RenameColumn(
                name: "PT2",
                table: "TeacherSubjects",
                newName: "WW1_First");

            migrationBuilder.RenameColumn(
                name: "PT10",
                table: "TeacherSubjects",
                newName: "WW10_Second");

            migrationBuilder.RenameColumn(
                name: "PT1",
                table: "TeacherSubjects",
                newName: "WW10_First");

            migrationBuilder.RenameColumn(
                name: "Exam",
                table: "TeacherSubjects",
                newName: "PT9_Second");

            migrationBuilder.RenameColumn(
                name: "WW9",
                table: "Scores",
                newName: "WW9_Second");

            migrationBuilder.RenameColumn(
                name: "WW8",
                table: "Scores",
                newName: "WW9_First");

            migrationBuilder.RenameColumn(
                name: "WW7",
                table: "Scores",
                newName: "WW8_Second");

            migrationBuilder.RenameColumn(
                name: "WW6",
                table: "Scores",
                newName: "WW8_First");

            migrationBuilder.RenameColumn(
                name: "WW5",
                table: "Scores",
                newName: "WW7_Second");

            migrationBuilder.RenameColumn(
                name: "WW4",
                table: "Scores",
                newName: "WW7_First");

            migrationBuilder.RenameColumn(
                name: "WW3",
                table: "Scores",
                newName: "WW6_Second");

            migrationBuilder.RenameColumn(
                name: "WW2",
                table: "Scores",
                newName: "WW6_First");

            migrationBuilder.RenameColumn(
                name: "WW10",
                table: "Scores",
                newName: "WW5_Second");

            migrationBuilder.RenameColumn(
                name: "WW1",
                table: "Scores",
                newName: "WW5_First");

            migrationBuilder.RenameColumn(
                name: "PT9",
                table: "Scores",
                newName: "WW4_Second");

            migrationBuilder.RenameColumn(
                name: "PT8",
                table: "Scores",
                newName: "WW4_First");

            migrationBuilder.RenameColumn(
                name: "PT7",
                table: "Scores",
                newName: "WW3_Second");

            migrationBuilder.RenameColumn(
                name: "PT6",
                table: "Scores",
                newName: "WW3_First");

            migrationBuilder.RenameColumn(
                name: "PT5",
                table: "Scores",
                newName: "WW2_Second");

            migrationBuilder.RenameColumn(
                name: "PT4",
                table: "Scores",
                newName: "WW2_First");

            migrationBuilder.RenameColumn(
                name: "PT3",
                table: "Scores",
                newName: "WW1_Second");

            migrationBuilder.RenameColumn(
                name: "PT2",
                table: "Scores",
                newName: "WW1_First");

            migrationBuilder.RenameColumn(
                name: "PT10",
                table: "Scores",
                newName: "WW10_Second");

            migrationBuilder.RenameColumn(
                name: "PT1",
                table: "Scores",
                newName: "WW10_First");

            migrationBuilder.RenameColumn(
                name: "Exam",
                table: "Scores",
                newName: "PT9_Second");

            migrationBuilder.AddColumn<double>(
                name: "Exam_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Exam_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT10_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT10_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT1_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT1_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT2_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT2_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT3_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT3_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT4_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT4_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT5_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT5_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT6_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT6_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT7_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT7_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT8_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT8_Second",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT9_First",
                table: "TeacherSubjects",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Exam_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Exam_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT10_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT10_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT1_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT1_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT2_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT2_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT3_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT3_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT4_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT4_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT5_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT5_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT6_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT6_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT7_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT7_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT8_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT8_Second",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PT9_First",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exam_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "Exam_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT10_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT10_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT1_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT1_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT2_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT2_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT3_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT3_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT4_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT4_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT5_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT5_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT6_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT6_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT7_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT7_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT8_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT8_Second",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "PT9_First",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "Exam_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Exam_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT10_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT10_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT1_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT1_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT2_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT2_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT3_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT3_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT4_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT4_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT5_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT5_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT6_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT6_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT7_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT7_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT8_First",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT8_Second",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PT9_First",
                table: "Scores");

            migrationBuilder.RenameColumn(
                name: "WW9_Second",
                table: "TeacherSubjects",
                newName: "WW9");

            migrationBuilder.RenameColumn(
                name: "WW9_First",
                table: "TeacherSubjects",
                newName: "WW8");

            migrationBuilder.RenameColumn(
                name: "WW8_Second",
                table: "TeacherSubjects",
                newName: "WW7");

            migrationBuilder.RenameColumn(
                name: "WW8_First",
                table: "TeacherSubjects",
                newName: "WW6");

            migrationBuilder.RenameColumn(
                name: "WW7_Second",
                table: "TeacherSubjects",
                newName: "WW5");

            migrationBuilder.RenameColumn(
                name: "WW7_First",
                table: "TeacherSubjects",
                newName: "WW4");

            migrationBuilder.RenameColumn(
                name: "WW6_Second",
                table: "TeacherSubjects",
                newName: "WW3");

            migrationBuilder.RenameColumn(
                name: "WW6_First",
                table: "TeacherSubjects",
                newName: "WW2");

            migrationBuilder.RenameColumn(
                name: "WW5_Second",
                table: "TeacherSubjects",
                newName: "WW10");

            migrationBuilder.RenameColumn(
                name: "WW5_First",
                table: "TeacherSubjects",
                newName: "WW1");

            migrationBuilder.RenameColumn(
                name: "WW4_Second",
                table: "TeacherSubjects",
                newName: "PT9");

            migrationBuilder.RenameColumn(
                name: "WW4_First",
                table: "TeacherSubjects",
                newName: "PT8");

            migrationBuilder.RenameColumn(
                name: "WW3_Second",
                table: "TeacherSubjects",
                newName: "PT7");

            migrationBuilder.RenameColumn(
                name: "WW3_First",
                table: "TeacherSubjects",
                newName: "PT6");

            migrationBuilder.RenameColumn(
                name: "WW2_Second",
                table: "TeacherSubjects",
                newName: "PT5");

            migrationBuilder.RenameColumn(
                name: "WW2_First",
                table: "TeacherSubjects",
                newName: "PT4");

            migrationBuilder.RenameColumn(
                name: "WW1_Second",
                table: "TeacherSubjects",
                newName: "PT3");

            migrationBuilder.RenameColumn(
                name: "WW1_First",
                table: "TeacherSubjects",
                newName: "PT2");

            migrationBuilder.RenameColumn(
                name: "WW10_Second",
                table: "TeacherSubjects",
                newName: "PT10");

            migrationBuilder.RenameColumn(
                name: "WW10_First",
                table: "TeacherSubjects",
                newName: "PT1");

            migrationBuilder.RenameColumn(
                name: "PT9_Second",
                table: "TeacherSubjects",
                newName: "Exam");

            migrationBuilder.RenameColumn(
                name: "WW9_Second",
                table: "Scores",
                newName: "WW9");

            migrationBuilder.RenameColumn(
                name: "WW9_First",
                table: "Scores",
                newName: "WW8");

            migrationBuilder.RenameColumn(
                name: "WW8_Second",
                table: "Scores",
                newName: "WW7");

            migrationBuilder.RenameColumn(
                name: "WW8_First",
                table: "Scores",
                newName: "WW6");

            migrationBuilder.RenameColumn(
                name: "WW7_Second",
                table: "Scores",
                newName: "WW5");

            migrationBuilder.RenameColumn(
                name: "WW7_First",
                table: "Scores",
                newName: "WW4");

            migrationBuilder.RenameColumn(
                name: "WW6_Second",
                table: "Scores",
                newName: "WW3");

            migrationBuilder.RenameColumn(
                name: "WW6_First",
                table: "Scores",
                newName: "WW2");

            migrationBuilder.RenameColumn(
                name: "WW5_Second",
                table: "Scores",
                newName: "WW10");

            migrationBuilder.RenameColumn(
                name: "WW5_First",
                table: "Scores",
                newName: "WW1");

            migrationBuilder.RenameColumn(
                name: "WW4_Second",
                table: "Scores",
                newName: "PT9");

            migrationBuilder.RenameColumn(
                name: "WW4_First",
                table: "Scores",
                newName: "PT8");

            migrationBuilder.RenameColumn(
                name: "WW3_Second",
                table: "Scores",
                newName: "PT7");

            migrationBuilder.RenameColumn(
                name: "WW3_First",
                table: "Scores",
                newName: "PT6");

            migrationBuilder.RenameColumn(
                name: "WW2_Second",
                table: "Scores",
                newName: "PT5");

            migrationBuilder.RenameColumn(
                name: "WW2_First",
                table: "Scores",
                newName: "PT4");

            migrationBuilder.RenameColumn(
                name: "WW1_Second",
                table: "Scores",
                newName: "PT3");

            migrationBuilder.RenameColumn(
                name: "WW1_First",
                table: "Scores",
                newName: "PT2");

            migrationBuilder.RenameColumn(
                name: "WW10_Second",
                table: "Scores",
                newName: "PT10");

            migrationBuilder.RenameColumn(
                name: "WW10_First",
                table: "Scores",
                newName: "PT1");

            migrationBuilder.RenameColumn(
                name: "PT9_Second",
                table: "Scores",
                newName: "Exam");
        }
    }
}
