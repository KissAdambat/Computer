using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Computer.Migrations
{
    /// <inheritdoc />
    public partial class RelationIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Osystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    RegisterTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osystems", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    Brand = table.Column<string>(type: "varchar(30)", nullable: true),
                    Type = table.Column<string>(type: "varchar(40)", nullable: true),
                    Display = table.Column<float>(type: "float", nullable: false),
                    Memory = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OsId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Computers_Osystems_OsId",
                        column: x => x.OsId,
                        principalTable: "Osystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OsId",
                table: "Computers",
                column: "OsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "Osystems");
        }
    }
}
