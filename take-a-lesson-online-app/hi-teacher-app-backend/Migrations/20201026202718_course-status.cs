using Microsoft.EntityFrameworkCore.Migrations;

namespace hi_teacher_app_backend.Migrations
{
    public partial class coursestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseStatus",
                table: "Courses",
                newName: "courseStatus");

            migrationBuilder.AlterColumn<string>(
                name: "courseStatus",
                table: "Courses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "courseStatus",
                table: "Courses",
                newName: "CourseStatus");

            migrationBuilder.AlterColumn<int>(
                name: "CourseStatus",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
