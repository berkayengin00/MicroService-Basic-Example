using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stock.API.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Count", "ProductId" },
                values: new object[,]
                {
                    { new Guid("305fcb3d-2eb3-4660-a135-7c287638f09f"), 4, new Guid("dafa94ed-6ab2-4eb9-b081-0ff320712ccc") },
                    { new Guid("635d528b-cfcd-4a14-b46a-7beb009babe5"), 2, new Guid("34c6a3a5-4f37-4d59-9c6f-bc8bcfc86816") },
                    { new Guid("691f2028-c1f7-40d3-bc66-403630a73941"), 10, new Guid("021ed5d7-b5e0-44d4-a9b8-1efdada32594") },
                    { new Guid("816cf5e1-1e54-425d-8d58-0491e3a121a6"), 5, new Guid("840e2dab-c5b1-43e4-8f61-dea1e007afe9") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
