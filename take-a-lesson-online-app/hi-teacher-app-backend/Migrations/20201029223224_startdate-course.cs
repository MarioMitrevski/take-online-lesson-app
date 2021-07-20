using Microsoft.EntityFrameworkCore.Migrations;

namespace hi_teacher_app_backend.Migrations
{
    public partial class startdatecourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Courses");
        }
    }
}
