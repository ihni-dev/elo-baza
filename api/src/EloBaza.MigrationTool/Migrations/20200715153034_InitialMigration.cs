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
                    SubjectKey = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectId);
                    table.UniqueConstraint("AK_Subject_SubjectKey", x => x.SubjectKey);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryKey = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParentCategoryId = table.Column<int>(nullable: true),
                    SubjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.UniqueConstraint("AK_Category_CategoryKey", x => x.CategoryKey);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Subject_SubjectId",
                        column: x => x.SubjectId,
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
                    ExamSessionKey = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    Year = table.Column<short>(nullable: false),
                    Semester = table.Column<string>(maxLength: 6, nullable: false),
                    ResitNumber = table.Column<byte>(nullable: false),
                    SubjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSession", x => x.ExamSessionId);
                    table.UniqueConstraint("AK_ExamSession_ExamSessionKey", x => x.ExamSessionKey);
                    table.ForeignKey(
                        name: "FK_ExamSession_Subject_SubjectId",
                        column: x => x.SubjectId,
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
                    QuestionKey = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    SubjectId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    ExamSessionId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.UniqueConstraint("AK_Question_QuestionKey", x => x.QuestionKey);
                    table.ForeignKey(
                        name: "FK_Question_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Question_ExamSession_ExamSessionId",
                        column: x => x.ExamSessionId,
                        principalTable: "ExamSession",
                        principalColumn: "ExamSessionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Question_Subject_SubjectId",
                        column: x => x.SubjectId,
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
                    AnswerKey = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.AnswerId);
                    table.UniqueConstraint("AK_Answer_AnswerKey", x => x.AnswerKey);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
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
                    ExplanationKey = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Content = table.Column<string>(maxLength: 70, nullable: false),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Explanation", x => x.ExplanationId);
                    table.UniqueConstraint("AK_Explanation_ExplanationKey", x => x.ExplanationKey);
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
                    AttachmentKey = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(maxLength: 256, nullable: false),
                    FileUri = table.Column<string>(type: "varchar(2048)", nullable: false),
                    FileSize = table.Column<long>(nullable: false),
                    ExplanationId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                    table.UniqueConstraint("AK_Attachment_AttachmentKey", x => x.AttachmentKey);
                    table.ForeignKey(
                        name: "FK_Attachment_Explanation_ExplanationId",
                        column: x => x.ExplanationId,
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
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ExplanationId",
                table: "Attachment",
                column: "ExplanationId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_QuestionId",
                table: "Attachment",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SubjectId",
                table: "Category",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSession_SubjectId",
                table: "ExamSession",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Explanation_QuestionId",
                table: "Explanation",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CategoryId",
                table: "Question",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ExamSessionId",
                table: "Question",
                column: "ExamSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_SubjectId",
                table: "Question",
                column: "SubjectId");
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
