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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    { "company10", "company10" },
                    { "company100", "company100" },
                    { "company101", "company101" },
                    { "company102", "company102" },
                    { "company103", "company103" },
                    { "company104", "company104" },
                    { "company105", "company105" },
                    { "company106", "company106" },
                    { "company107", "company107" },
                    { "company108", "company108" },
                    { "company109", "company109" },
                    { "company11", "company11" },
                    { "company110", "company110" },
                    { "company111", "company111" },
                    { "company112", "company112" },
                    { "company113", "company113" },
                    { "company114", "company114" },
                    { "company115", "company115" },
                    { "company116", "company116" },
                    { "company117", "company117" },
                    { "company118", "company118" },
                    { "company119", "company119" },
                    { "company12", "company12" },
                    { "company120", "company120" },
                    { "company121", "company121" },
                    { "company122", "company122" },
                    { "company123", "company123" },
                    { "company124", "company124" },
                    { "company125", "company125" },
                    { "company126", "company126" },
                    { "company127", "company127" },
                    { "company128", "company128" },
                    { "company129", "company129" },
                    { "company13", "company13" },
                    { "company130", "company130" },
                    { "company131", "company131" },
                    { "company132", "company132" },
                    { "company133", "company133" },
                    { "company134", "company134" },
                    { "company135", "company135" },
                    { "company136", "company136" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "company137", "company137" },
                    { "company138", "company138" },
                    { "company139", "company139" },
                    { "company14", "company14" },
                    { "company140", "company140" },
                    { "company141", "company141" },
                    { "company142", "company142" },
                    { "company143", "company143" },
                    { "company144", "company144" },
                    { "company145", "company145" },
                    { "company146", "company146" },
                    { "company147", "company147" },
                    { "company148", "company148" },
                    { "company149", "company149" },
                    { "company15", "company15" },
                    { "company150", "company150" },
                    { "company151", "company151" },
                    { "company152", "company152" },
                    { "company153", "company153" },
                    { "company154", "company154" },
                    { "company155", "company155" },
                    { "company156", "company156" },
                    { "company157", "company157" },
                    { "company158", "company158" },
                    { "company159", "company159" },
                    { "company16", "company16" },
                    { "company160", "company160" },
                    { "company161", "company161" },
                    { "company162", "company162" },
                    { "company163", "company163" },
                    { "company164", "company164" },
                    { "company165", "company165" },
                    { "company166", "company166" },
                    { "company167", "company167" },
                    { "company168", "company168" },
                    { "company169", "company169" },
                    { "company17", "company17" },
                    { "company170", "company170" },
                    { "company171", "company171" },
                    { "company172", "company172" },
                    { "company173", "company173" },
                    { "company174", "company174" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "company175", "company175" },
                    { "company176", "company176" },
                    { "company177", "company177" },
                    { "company178", "company178" },
                    { "company179", "company179" },
                    { "company18", "company18" },
                    { "company180", "company180" },
                    { "company181", "company181" },
                    { "company182", "company182" },
                    { "company183", "company183" },
                    { "company184", "company184" },
                    { "company185", "company185" },
                    { "company186", "company186" },
                    { "company187", "company187" },
                    { "company188", "company188" },
                    { "company189", "company189" },
                    { "company19", "company19" },
                    { "company190", "company190" },
                    { "company191", "company191" },
                    { "company192", "company192" },
                    { "company193", "company193" },
                    { "company194", "company194" },
                    { "company195", "company195" },
                    { "company196", "company196" },
                    { "company197", "company197" },
                    { "company198", "company198" },
                    { "company199", "company199" },
                    { "company2", "company2" },
                    { "company20", "company20" },
                    { "company200", "company200" },
                    { "company21", "company21" },
                    { "company22", "company22" },
                    { "company23", "company23" },
                    { "company24", "company24" },
                    { "company25", "company25" },
                    { "company26", "company26" },
                    { "company27", "company27" },
                    { "company28", "company28" },
                    { "company29", "company29" },
                    { "company3", "company3" },
                    { "company30", "company30" },
                    { "company31", "company31" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "company32", "company32" },
                    { "company33", "company33" },
                    { "company34", "company34" },
                    { "company35", "company35" },
                    { "company36", "company36" },
                    { "company37", "company37" },
                    { "company38", "company38" },
                    { "company39", "company39" },
                    { "company4", "company4" },
                    { "company40", "company40" },
                    { "company41", "company41" },
                    { "company42", "company42" },
                    { "company43", "company43" },
                    { "company44", "company44" },
                    { "company45", "company45" },
                    { "company46", "company46" },
                    { "company47", "company47" },
                    { "company48", "company48" },
                    { "company49", "company49" },
                    { "company5", "company5" },
                    { "company50", "company50" },
                    { "company51", "company51" },
                    { "company52", "company52" },
                    { "company53", "company53" },
                    { "company54", "company54" },
                    { "company55", "company55" },
                    { "company56", "company56" },
                    { "company57", "company57" },
                    { "company58", "company58" },
                    { "company59", "company59" },
                    { "company6", "company6" },
                    { "company60", "company60" },
                    { "company61", "company61" },
                    { "company62", "company62" },
                    { "company63", "company63" },
                    { "company64", "company64" },
                    { "company65", "company65" },
                    { "company66", "company66" },
                    { "company67", "company67" },
                    { "company68", "company68" },
                    { "company69", "company69" },
                    { "company7", "company7" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "company70", "company70" },
                    { "company71", "company71" },
                    { "company72", "company72" },
                    { "company73", "company73" },
                    { "company74", "company74" },
                    { "company75", "company75" },
                    { "company76", "company76" },
                    { "company77", "company77" },
                    { "company78", "company78" },
                    { "company79", "company79" },
                    { "company8", "company8" },
                    { "company80", "company80" },
                    { "company81", "company81" },
                    { "company82", "company82" },
                    { "company83", "company83" },
                    { "company84", "company84" },
                    { "company85", "company85" },
                    { "company86", "company86" },
                    { "company87", "company87" },
                    { "company88", "company88" },
                    { "company89", "company89" },
                    { "company9", "company9" },
                    { "company90", "company90" },
                    { "company91", "company91" },
                    { "company92", "company92" },
                    { "company93", "company93" },
                    { "company94", "company94" },
                    { "company95", "company95" },
                    { "company96", "company96" },
                    { "company97", "company97" },
                    { "company98", "company98" },
                    { "company99", "company99" }
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
                    { 1, "fruit", "company1", "apple" },
                    { 2, "fruit", "company1", "banana" },
                    { 3, "fruit", "company1", "orange" },
                    { 4, "fruit", "company1", "grape" },
                    { 5, "fruit", "company2", "mango" },
                    { 6, "electronic", "company2", "television" },
                    { 7, "electronic", "company2", "laptop" },
                    { 8, "electronic", "company2", "keyboard" },
                    { 9, "electronic", "company2", "monitor" },
                    { 10, "household", "company3", "knife" },
                    { 11, "household", "company3", "spoon" },
                    { 12, "household", "company3", "bowl" },
                    { 13, "household", "company3", "chopsticks" }
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
