using Microsoft.EntityFrameworkCore.Migrations;

namespace EloBaza.MigrationTool.Migrations
{
    public partial class AddExamSessionTableAndSubjectRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamSession",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Semester = table.Column<string>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSession_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamSession_SubjectId",
                table: "ExamSession",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamSession");
        }
    }
}
