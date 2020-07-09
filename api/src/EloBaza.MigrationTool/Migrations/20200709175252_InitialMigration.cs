using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EloBaza.MigrationTool.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.SubjectId);
                    table.UniqueConstraint("AK_Subject_Key", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParentCategory_id = table.Column<int>(nullable: true),
                    Subject_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.CategoryId);
                    table.UniqueConstraint("AK_Category_Key", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategory_id",
                        column: x => x.ParentCategory_id,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Subject_Subject_id",
                        column: x => x.Subject_id,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ExamSession",
                columns: table => new
                {
                    ExamSessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    Year = table.Column<short>(nullable: false),
                    Semester = table.Column<string>(maxLength: 6, nullable: false),
                    ResitNumber = table.Column<byte>(nullable: true),
                    Subject_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.ExamSessionId);
                    table.UniqueConstraint("AK_ExamSession_Key", x => x.Key);
                    table.ForeignKey(
                        name: "FK_ExamSession_Subject_Subject_id",
                        column: x => x.Subject_id,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    _categoryId = table.Column<int>(nullable: true),
                    _examSessionId = table.Column<int>(nullable: true),
                    _subjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.QuestionId);
                    table.UniqueConstraint("AK_Question_Key", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Question_Category__categoryId",
                        column: x => x._categoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Question_ExamSession__examSessionId",
                        column: x => x._examSessionId,
                        principalTable: "ExamSession",
                        principalColumn: "ExamSessionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Question_Subject__subjectId",
                        column: x => x._subjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false),
                    Question_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.AnswerId);
                    table.UniqueConstraint("AK_Answer_Key", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Answer_Question_Question_id",
                        column: x => x.Question_id,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Explanation",
                columns: table => new
                {
                    ExplanationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    Content = table.Column<string>(maxLength: 70, nullable: false),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.ExplanationId);
                    table.UniqueConstraint("AK_Explanation_Key", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Explanation_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(maxLength: 256, nullable: false),
                    FileUri = table.Column<string>(type: "varchar(2048)", nullable: false),
                    FileSize = table.Column<long>(nullable: false),
                    Explanation_id = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.AttachmentId);
                    table.UniqueConstraint("AK_Attachment_Key", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Attachment_Explanation_Explanation_id",
                        column: x => x.Explanation_id,
                        principalTable: "Explanation",
                        principalColumn: "ExplanationId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Attachment_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_Question_id",
                table: "Answer",
                column: "Question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_Explanation_id",
                table: "Attachment",
                column: "Explanation_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_QuestionId",
                table: "Attachment",
                column: "QuestionId",
                unique: true,
                filter: "[QuestionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategory_id",
                table: "Category",
                column: "ParentCategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Subject_id",
                table: "Category",
                column: "Subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSession_Subject_id",
                table: "ExamSession",
                column: "Subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_Explanation_QuestionId",
                table: "Explanation",
                column: "QuestionId",
                unique: true,
                filter: "[QuestionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Question__categoryId",
                table: "Question",
                column: "_categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Question__examSessionId",
                table: "Question",
                column: "_examSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question__subjectId",
                table: "Question",
                column: "_subjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Explanation");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ExamSession");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
