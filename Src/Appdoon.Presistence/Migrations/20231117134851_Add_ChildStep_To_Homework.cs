using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mapdoon.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_ChildStep_To_Homework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChildSteps_HomeworkId",
                table: "ChildSteps");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2023, 11, 17, 17, 18, 50, 896, DateTimeKind.Local).AddTicks(3185));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2023, 11, 17, 17, 18, 50, 896, DateTimeKind.Local).AddTicks(3259));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 11, 17, 17, 18, 50, 896, DateTimeKind.Local).AddTicks(3267));

            migrationBuilder.CreateIndex(
                name: "IX_ChildSteps_HomeworkId",
                table: "ChildSteps",
                column: "HomeworkId",
                unique: true,
                filter: "[HomeworkId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChildSteps_HomeworkId",
                table: "ChildSteps");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2023, 11, 16, 20, 22, 20, 833, DateTimeKind.Local).AddTicks(9281));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2023, 11, 16, 20, 22, 20, 833, DateTimeKind.Local).AddTicks(9396));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 11, 16, 20, 22, 20, 833, DateTimeKind.Local).AddTicks(9409));

            migrationBuilder.CreateIndex(
                name: "IX_ChildSteps_HomeworkId",
                table: "ChildSteps",
                column: "HomeworkId");
        }
    }
}
