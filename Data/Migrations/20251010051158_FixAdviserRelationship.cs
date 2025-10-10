using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms_test1.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixAdviserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_AspNetUsers_AdviserId",
                table: "Sections");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_AspNetUsers_AdviserId",
                table: "Sections",
                column: "AdviserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_AspNetUsers_AdviserId",
                table: "Sections");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_AspNetUsers_AdviserId",
                table: "Sections",
                column: "AdviserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
