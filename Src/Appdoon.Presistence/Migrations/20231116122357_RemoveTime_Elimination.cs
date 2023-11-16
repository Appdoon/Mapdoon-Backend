using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mapdoon.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTime_Elimination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "StepProgresses");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "RoadMaps");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Linkers");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "HomeworkProgresses");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "ChildSteps");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "ChildStepProgresses");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2023, 11, 16, 15, 53, 57, 183, DateTimeKind.Local).AddTicks(695));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2023, 11, 16, 15, 53, 57, 183, DateTimeKind.Local).AddTicks(761));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 11, 16, 15, 53, 57, 183, DateTimeKind.Local).AddTicks(769));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Steps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "StepProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "RoadMaps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Rates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Questions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Linkers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Lessons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Homeworks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "HomeworkProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "ChildSteps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "ChildStepProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertTime", "RemoveTime" },
                values: new object[] { new DateTime(2023, 11, 10, 12, 20, 8, 725, DateTimeKind.Local).AddTicks(1781), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertTime", "RemoveTime" },
                values: new object[] { new DateTime(2023, 11, 10, 12, 20, 8, 725, DateTimeKind.Local).AddTicks(1863), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "InsertTime", "RemoveTime" },
                values: new object[] { new DateTime(2023, 11, 10, 12, 20, 8, 725, DateTimeKind.Local).AddTicks(1872), null });
        }
    }
}
