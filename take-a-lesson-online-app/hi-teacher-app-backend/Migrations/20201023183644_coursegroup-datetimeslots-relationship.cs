using Microsoft.EntityFrameworkCore.Migrations;

namespace hi_teacher_app_backend.Migrations
{
    public partial class coursegroupdatetimeslotsrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtartTime",
                table: "DateTimeSlots");

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "DateTimeSlots",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeSlots_CourseGroupId",
                table: "DateTimeSlots",
                column: "CourseGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DateTimeSlots_CourseGroups_CourseGroupId",
                table: "DateTimeSlots",
                column: "CourseGroupId",
                principalTable: "CourseGroups",
                principalColumn: "CourseGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateTimeSlots_CourseGroups_CourseGroupId",
                table: "DateTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_DateTimeSlots_CourseGroupId",
                table: "DateTimeSlots");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "DateTimeSlots");

            migrationBuilder.AddColumn<string>(
                name: "AtartTime",
                table: "DateTimeSlots",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
