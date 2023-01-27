using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinQTraining.LinQExtensions.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Data",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "Data 1" },
                    { 2, "Data 2" },
                    { 3, "Data 3" },
                    { 4, "Data 4" },
                    { 5, "Data 5" },
                    { 6, "Data 6" },
                    { 7, "Data 7" },
                    { 8, "Data 8" },
                    { 9, "Data 9" },
                    { 10, "Data 10" },
                    { 11, "Data 11" },
                    { 12, "Data 12" },
                    { 13, "Data 13" },
                    { 14, "Data 14" },
                    { 15, "Data 15" },
                    { 16, "Data 16" },
                    { 17, "Data 17" },
                    { 18, "Data 18" },
                    { 19, "Data 19" },
                    { 20, "Data 20" },
                    { 21, "Data 21" },
                    { 22, "Data 22" },
                    { 23, "Data 23" },
                    { 24, "Data 24" },
                    { 25, "Data 25" },
                    { 26, "Data 26" },
                    { 27, "Data 27" },
                    { 28, "Data 28" },
                    { 29, "Data 29" },
                    { 30, "Data 30" },
                    { 31, "Data 31" },
                    { 32, "Data 32" },
                    { 33, "Data 33" },
                    { 34, "Data 34" },
                    { 35, "Data 35" },
                    { 36, "Data 36" },
                    { 37, "Data 37" },
                    { 38, "Data 38" },
                    { 39, "Data 39" },
                    { 40, "Data 40" },
                    { 41, "Data 41" },
                    { 42, "Data 42" }
                });

            migrationBuilder.InsertData(
                table: "Data",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 43, "Data 43" },
                    { 44, "Data 44" },
                    { 45, "Data 45" },
                    { 46, "Data 46" },
                    { 47, "Data 47" },
                    { 48, "Data 48" },
                    { 49, "Data 49" },
                    { 50, "Data 50" },
                    { 51, "Data 51" },
                    { 52, "Data 52" },
                    { 53, "Data 53" },
                    { 54, "Data 54" },
                    { 55, "Data 55" },
                    { 56, "Data 56" },
                    { 57, "Data 57" },
                    { 58, "Data 58" },
                    { 59, "Data 59" },
                    { 60, "Data 60" },
                    { 61, "Data 61" },
                    { 62, "Data 62" },
                    { 63, "Data 63" },
                    { 64, "Data 64" },
                    { 65, "Data 65" },
                    { 66, "Data 66" },
                    { 67, "Data 67" },
                    { 68, "Data 68" },
                    { 69, "Data 69" },
                    { 70, "Data 70" },
                    { 71, "Data 71" },
                    { 72, "Data 72" },
                    { 73, "Data 73" },
                    { 74, "Data 74" },
                    { 75, "Data 75" },
                    { 76, "Data 76" },
                    { 77, "Data 77" },
                    { 78, "Data 78" },
                    { 79, "Data 79" },
                    { 80, "Data 80" },
                    { 81, "Data 81" },
                    { 82, "Data 82" },
                    { 83, "Data 83" },
                    { 84, "Data 84" }
                });

            migrationBuilder.InsertData(
                table: "Data",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 85, "Data 85" },
                    { 86, "Data 86" },
                    { 87, "Data 87" },
                    { 88, "Data 88" },
                    { 89, "Data 89" },
                    { 90, "Data 90" },
                    { 91, "Data 91" },
                    { 92, "Data 92" },
                    { 93, "Data 93" },
                    { 94, "Data 94" },
                    { 95, "Data 95" },
                    { 96, "Data 96" },
                    { 97, "Data 97" },
                    { 98, "Data 98" },
                    { 99, "Data 99" },
                    { 100, "Data 100" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Data");
        }
    }
}
