using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mapdoon.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class ChatMessage_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    RoadMapId = table.Column<int>(type: "int", nullable: true),
                    ReplyMessageId = table.Column<int>(type: "int", nullable: true),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatMessages_ReplyMessageId",
                        column: x => x.ReplyMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_RoadMaps_RoadMapId",
                        column: x => x.RoadMapId,
                        principalTable: "RoadMaps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2023, 12, 5, 22, 4, 7, 79, DateTimeKind.Local).AddTicks(8865));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertTime",
                value: new DateTime(2023, 12, 5, 22, 4, 7, 79, DateTimeKind.Local).AddTicks(8940));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 12, 5, 22, 4, 7, 79, DateTimeKind.Local).AddTicks(8949));

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReplyMessageId",
                table: "ChatMessages",
                column: "ReplyMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_RoadMapId",
                table: "ChatMessages",
                column: "RoadMapId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

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
                value: new DateTime(2023, 11, 24, 14, 58, 6, 627, DateTimeKind.Local).AddTicks(9661));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertTime",
                value: new DateTime(2023, 11, 24, 14, 58, 6, 627, DateTimeKind.Local).AddTicks(9671));
        }
    }
}
