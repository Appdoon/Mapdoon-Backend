using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mapdoon.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class HomeworkDatamodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Users_CreatorId",
                table: "Homeworks");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Homeworks_HomeworkId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_HomeworkId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "Questions");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinScore",
                table: "Homeworks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Homeworks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "HomeworkProgresses",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2023, 11, 10, 12, 20, 8, 725, DateTimeKind.Local).AddTicks(1781));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2023, 11, 10, 12, 20, 8, 725, DateTimeKind.Local).AddTicks(1863));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 11, 10, 12, 20, 8, 725, DateTimeKind.Local).AddTicks(1872));

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Users_CreatorId",
                table: "Homeworks",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Users_CreatorId",
                table: "Homeworks");

            migrationBuilder.AddColumn<int>(
                name: "HomeworkId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MinScore",
                table: "Homeworks",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Homeworks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "HomeworkProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2023, 10, 30, 16, 44, 51, 621, DateTimeKind.Local).AddTicks(9478));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2023, 10, 30, 16, 44, 51, 621, DateTimeKind.Local).AddTicks(9547));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 10, 30, 16, 44, 51, 621, DateTimeKind.Local).AddTicks(9578));

            migrationBuilder.CreateIndex(
                name: "IX_Questions_HomeworkId",
                table: "Questions",
                column: "HomeworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Users_CreatorId",
                table: "Homeworks",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Homeworks_HomeworkId",
                table: "Questions",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
