using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appdoon.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class rate_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rates_RoadMapId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UserId",
                table: "Rates");

            migrationBuilder.AddColumn<int>(
                name: "RateCount",
                table: "RoadMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2023, 10, 25, 22, 32, 10, 149, DateTimeKind.Local).AddTicks(5492));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2023, 10, 25, 22, 32, 10, 149, DateTimeKind.Local).AddTicks(5579));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 10, 25, 22, 32, 10, 149, DateTimeKind.Local).AddTicks(5614));

            migrationBuilder.CreateIndex(
                name: "IX_Rates_RoadMapId",
                table: "Rates",
                column: "RoadMapId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserId",
                table: "Rates",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rates_RoadMapId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UserId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "RateCount",
                table: "RoadMaps");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2023, 10, 25, 20, 20, 6, 853, DateTimeKind.Local).AddTicks(8506));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2023, 10, 25, 20, 20, 6, 856, DateTimeKind.Local).AddTicks(7983));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 10, 25, 20, 20, 6, 856, DateTimeKind.Local).AddTicks(8136));

            migrationBuilder.CreateIndex(
                name: "IX_Rates_RoadMapId",
                table: "Rates",
                column: "RoadMapId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserId",
                table: "Rates",
                column: "UserId",
                unique: true);
        }
    }
}
