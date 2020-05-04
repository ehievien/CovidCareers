using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Entity.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    LastDateUpdated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Completed", "DateCreated", "Description", "LastDateUpdated", "Title" },
                values: new object[] { 1, true, new DateTime(2020, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cook", new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn how to cook" });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Completed", "DateCreated", "Description", "LastDateUpdated", "Title" },
                values: new object[] { 2, true, new DateTime(2020, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dance", new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn how to dance" });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Completed", "DateCreated", "Description", "LastDateUpdated", "Title" },
                values: new object[] { 3, true, new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drive", new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn how to drive" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");
        }
    }
}
