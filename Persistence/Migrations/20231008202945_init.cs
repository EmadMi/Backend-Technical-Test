using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Areas",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTaxFee = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTaxFree = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AreaRules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TaxFee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaRules_Areas_AreaId",
                        column: x => x.AreaId,
                        principalSchema: "dbo",
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Areas",
                columns: new[] { "Id", "MaxTaxFee", "Name", "Order" },
                values: new object[] { 1, 60, "Gothenburg", 1 });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Holidays",
                columns: new[] { "Id", "HolyDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2013, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2013, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2013, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2013, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2013, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2013, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2013, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2013, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2013, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2013, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2013, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2013, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2013, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2013, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2013, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Vehicles",
                columns: new[] { "Id", "IsTaxFree", "Name", "Order" },
                values: new object[,]
                {
                    { 1, true, "Emergency", 1 },
                    { 2, true, "Bus", 2 },
                    { 3, true, "Diplomat", 3 },
                    { 4, true, "Motorcycle", 4 },
                    { 5, true, "Military", 5 },
                    { 6, true, "Foreign", 6 },
                    { 7, false, "Car", 7 },
                    { 8, false, "Motorbike", 8 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AreaRules",
                columns: new[] { "Id", "AreaId", "EndTime", "StartTime", "TaxFee", "Year" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 6, 29, 0, 0), new TimeSpan(0, 6, 0, 0, 0), 8, 2013 },
                    { 2, 1, new TimeSpan(0, 6, 59, 0, 0), new TimeSpan(0, 6, 30, 0, 0), 13, 2013 },
                    { 3, 1, new TimeSpan(0, 7, 59, 0, 0), new TimeSpan(0, 7, 0, 0, 0), 18, 2013 },
                    { 4, 1, new TimeSpan(0, 8, 29, 0, 0), new TimeSpan(0, 8, 0, 0, 0), 13, 2013 },
                    { 5, 1, new TimeSpan(0, 14, 59, 0, 0), new TimeSpan(0, 8, 30, 0, 0), 8, 2013 },
                    { 6, 1, new TimeSpan(0, 15, 29, 0, 0), new TimeSpan(0, 15, 0, 0, 0), 13, 2013 },
                    { 7, 1, new TimeSpan(0, 16, 59, 0, 0), new TimeSpan(0, 15, 30, 0, 0), 18, 2013 },
                    { 8, 1, new TimeSpan(0, 17, 59, 0, 0), new TimeSpan(0, 17, 0, 0, 0), 13, 2013 },
                    { 9, 1, new TimeSpan(0, 18, 29, 0, 0), new TimeSpan(0, 18, 0, 0, 0), 8, 2013 },
                    { 10, 1, new TimeSpan(0, 5, 59, 0, 0), new TimeSpan(0, 18, 30, 0, 0), 0, 2013 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaRules_AreaId",
                schema: "dbo",
                table: "AreaRules",
                column: "AreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaRules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Holidays",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Vehicles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Areas",
                schema: "dbo");
        }
    }
}
