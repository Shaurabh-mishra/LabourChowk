using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabourChowk_webapi.Migrations
{
    /// <inheritdoc />
    public partial class Auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_WorkPosters_WorkPosterId",
                table: "Jobs");

            migrationBuilder.AlterColumn<string>(
                name: "Skill",
                table: "WorkPosters",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "WorkPosterId",
                table: "Jobs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            // migrationBuilder.CreateTable(
            //     name: "Users",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "INTEGER", nullable: false)
            //             .Annotation("Sqlite:Autoincrement", true),
            //         Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
            //         PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
            //         Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
            //         CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Users", x => x.Id);
            //     });
            migrationBuilder.Sql(
                @"CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    PasswordHash TEXT NOT NULL,
                    Role TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL
                );",
                suppressTransaction: true
            );


            migrationBuilder.Sql(@"
    CREATE UNIQUE INDEX IF NOT EXISTS IX_Users_Username
    ON Users(Username);
", suppressTransaction: true);

            migrationBuilder.Sql(@"
    CREATE UNIQUE INDEX IF NOT EXISTS IX_Workers_Phone
    ON Workers(Phone);
", suppressTransaction: true);

            migrationBuilder.Sql(@"
    CREATE UNIQUE INDEX IF NOT EXISTS IX_WorkPosters_Phone
    ON WorkPosters(Phone);
", suppressTransaction: true);


            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_WorkPosters_WorkPosterId",
                table: "Jobs",
                column: "WorkPosterId",
                principalTable: "WorkPosters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_WorkPosters_WorkPosterId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_WorkPosters_Phone",
                table: "WorkPosters");

            migrationBuilder.DropIndex(
                name: "IX_Workers_Phone",
                table: "Workers");

            migrationBuilder.AlterColumn<string>(
                name: "Skill",
                table: "WorkPosters",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkPosterId",
                table: "Jobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_WorkPosters_WorkPosterId",
                table: "Jobs",
                column: "WorkPosterId",
                principalTable: "WorkPosters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
