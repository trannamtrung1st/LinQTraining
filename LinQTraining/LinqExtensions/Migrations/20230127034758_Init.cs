using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinQTraining.LinQExtensions.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "electronic", "electronic" },
                    { "fruit", "fruit" },
                    { "household", "household" },
                    { "misc", "misc" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { "apple", "fruit", "apple" },
                    { "banana", "fruit", "banana" },
                    { "bowl", "household", "bowl" },
                    { "chopsticks", "household", "chopsticks" },
                    { "grape", "fruit", "grape" },
                    { "keyboard", "electronic", "keyboard" },
                    { "knife", "household", "knife" },
                    { "laptop", "electronic", "laptop" },
                    { "mango", "fruit", "mango" },
                    { "monitor", "electronic", "monitor" },
                    { "orange", "fruit", "orange" },
                    { "spoon", "household", "spoon" },
                    { "television", "electronic", "television" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
