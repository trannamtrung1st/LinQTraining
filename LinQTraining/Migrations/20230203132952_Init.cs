using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinQTraining.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "company1", "company1" },
                    { "company2", "company2" },
                    { "company3", "company3" },
                    { "company4", "company4" },
                    { "company5", "company5" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CompanyId", "Name" },
                values: new object[,]
                {
                    { "electronic", "company2", "electronic" },
                    { "fruit", "company1", "fruit" },
                    { "household", "company2", "household" },
                    { "misc", "company3", "misc" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "CompanyId", "Name" },
                values: new object[,]
                {
                    { "apple", "fruit", "company1", "apple" },
                    { "banana", "fruit", "company1", "banana" },
                    { "bowl", "household", "company3", "bowl" },
                    { "chopsticks", "household", "company3", "chopsticks" },
                    { "grape", "fruit", "company1", "grape" },
                    { "keyboard", "electronic", "company2", "keyboard" },
                    { "knife", "household", "company3", "knife" },
                    { "laptop", "electronic", "company2", "laptop" },
                    { "mango", "fruit", "company2", "mango" },
                    { "monitor", "electronic", "company2", "monitor" },
                    { "orange", "fruit", "company1", "orange" },
                    { "spoon", "household", "company3", "spoon" },
                    { "television", "electronic", "company2", "television" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_CompanyId",
                table: "Category",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompanyId",
                table: "Product",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
