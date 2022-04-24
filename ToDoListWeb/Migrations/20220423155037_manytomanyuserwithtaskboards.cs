using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListWeb.Migrations
{
    public partial class manytomanyuserwithtaskboards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskBoardUser",
                columns: table => new
                {
                    TaskBoardsId = table.Column<int>(type: "int", nullable: false),
                    UserListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskBoardUser", x => new { x.TaskBoardsId, x.UserListId });
                    table.ForeignKey(
                        name: "FK_TaskBoardUser_AspNetUsers_UserListId",
                        column: x => x.UserListId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskBoardUser_TaskBoards_TaskBoardsId",
                        column: x => x.TaskBoardsId,
                        principalTable: "TaskBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoardUser_UserListId",
                table: "TaskBoardUser",
                column: "UserListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskBoardUser");
        }
    }
}
