using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentExtraFieldsAndDefinitionModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsExtraFieldDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FieldName = table.Column<string>(type: "TEXT", nullable: false),
                    FieldType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsExtraFieldDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentsExtraFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    FieldValue = table.Column<string>(type: "TEXT", nullable: false),
                    ExtraFieldDefinitionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsExtraFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsExtraFields_StudentsExtraFieldDefinitions_ExtraFieldDefinitionId",
                        column: x => x.ExtraFieldDefinitionId,
                        principalTable: "StudentsExtraFieldDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsExtraFields_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsExtraFields_ExtraFieldDefinitionId",
                table: "StudentsExtraFields",
                column: "ExtraFieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsExtraFields_StudentId",
                table: "StudentsExtraFields",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsExtraFields");

            migrationBuilder.DropTable(
                name: "StudentsExtraFieldDefinitions");
        }
    }
}
