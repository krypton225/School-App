using Microsoft.EntityFrameworkCore.Migrations;

namespace school.Migrations
{
    public partial class first12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAbsent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Absent",
                table: "Absent");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "User",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Absent",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Absent",
                table: "Absent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Absent_StudentId",
                table: "Absent",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Absent",
                table: "Absent");

            migrationBuilder.DropIndex(
                name: "IX_Absent_StudentId",
                table: "Absent");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Absent");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Absent",
                table: "Absent",
                column: "StudentId");

            migrationBuilder.CreateTable(
                name: "StudentAbsent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Is_rejected = table.Column<bool>(type: "bit", nullable: false),
                    Times = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAbsent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAbsent_Student_Id",
                        column: x => x.Id,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
