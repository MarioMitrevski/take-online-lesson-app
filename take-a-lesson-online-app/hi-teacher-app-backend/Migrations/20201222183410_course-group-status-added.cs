using Microsoft.EntityFrameworkCore.Migrations;

namespace hi_teacher_app_backend.Migrations
{
    public partial class coursegroupstatusadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "courseGroupStatus",
                table: "CourseGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "courseGroupStatus",
                table: "CourseGroups");
        }
    }
}
