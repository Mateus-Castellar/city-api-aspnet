using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class SeedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Citys",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Cidade localizada no Brazil", "São Paulo" });

            migrationBuilder.InsertData(
                table: "Citys",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Cidade localizada no Brazil", "Belo Horizonte" });

            migrationBuilder.InsertData(
                table: "Citys",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Cidade localizada no Brazil", "Porto Alegre" });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 1, 1, "Estadio de futebol", "Neo Quimica Arena" });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 2, 2, "Estadio de futebol", "Mineirão" });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 3, 3, "Estadio de futebol", "Beira Rio" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Citys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Citys",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Citys",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
