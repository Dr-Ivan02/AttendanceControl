using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AttendanceSessionsAndCourseInstructors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DECLARE @fkName sysname;

                SELECT @fkName = fk.name
                FROM sys.foreign_keys fk
                INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                WHERE fk.parent_object_id = OBJECT_ID(N'Attendances')
                    AND fkc.parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'Attendances'), N'CourseId', 'ColumnId');

                IF @fkName IS NOT NULL
                    EXEC(N'ALTER TABLE [Attendances] DROP CONSTRAINT [' + @fkName + N']');

                SELECT @fkName = NULL;

                SELECT @fkName = fk.name
                FROM sys.foreign_keys fk
                INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                WHERE fk.parent_object_id = OBJECT_ID(N'Attendances')
                    AND fkc.parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'Attendances'), N'StudentId', 'ColumnId');

                IF @fkName IS NOT NULL
                    EXEC(N'ALTER TABLE [Attendances] DROP CONSTRAINT [' + @fkName + N']');

                SELECT @fkName = NULL;

                SELECT @fkName = fk.name
                FROM sys.foreign_keys fk
                INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                WHERE fk.parent_object_id = OBJECT_ID(N'Students')
                    AND fkc.parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'Students'), N'CourseId', 'ColumnId');

                IF @fkName IS NOT NULL
                    EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @fkName + N']');
                """);

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.RenameTable(
                name: "Attendances",
                newName: "AttendanceRows");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    AttendanceId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceDetails_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceDetails_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql("""
                INSERT INTO Attendances (Date, CourseId)
                SELECT CAST([Date] AS date), CourseId
                FROM AttendanceRows
                GROUP BY CAST([Date] AS date), CourseId;

                INSERT INTO AttendanceDetails (IsPresent, AttendanceId, StudentId)
                SELECT
                    MAX(CAST(r.IsPresent AS int)),
                    a.Id,
                    r.StudentId
                FROM AttendanceRows r
                INNER JOIN Attendances a
                    ON a.CourseId = r.CourseId
                    AND CAST(a.[Date] AS date) = CAST(r.[Date] AS date)
                GROUP BY a.Id, r.StudentId;
                """);

            migrationBuilder.DropTable(
                name: "AttendanceRows");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CourseId_Date",
                table: "Attendances",
                columns: new[] { "CourseId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetails_AttendanceId_StudentId",
                table: "AttendanceDetails",
                columns: new[] { "AttendanceId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetails_StudentId",
                table: "AttendanceDetails",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instructors_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CourseId",
                table: "Students",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Instructors_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CourseId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "AttendanceDetails");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CourseId",
                table: "Attendances",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CourseId",
                table: "Students",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
