using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduTrack.Migrations
{
    /// <inheritdoc />
    public partial class addcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrerequisiteID",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PrerequisiteID",
                table: "Courses",
                column: "PrerequisiteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Courses_PrerequisiteID",
                table: "Courses",
                column: "PrerequisiteID",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Courses_PrerequisiteID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_PrerequisiteID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PrerequisiteID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "code",
                table: "Courses");
        }
    }
}
