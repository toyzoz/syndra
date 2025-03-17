#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.API.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Brands",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Brand = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Brands", x => x.Id); });

        migrationBuilder.CreateTable(
            "Types",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Type = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Types", x => x.Id); });

        migrationBuilder.CreateTable(
            "CatalogItems",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: false),
                Price = table.Column<decimal>("decimal(18,2)", precision: 18, scale: 2, nullable: false),
                PictureFileName = table.Column<string>("nvarchar(max)", nullable: false),
                CatalogTypeId = table.Column<int>("int", nullable: false),
                CatalogBrandId = table.Column<int>("int", nullable: false),
                AvailableStock = table.Column<int>("int", nullable: false),
                RestockThreshold = table.Column<int>("int", nullable: false),
                MaxStockThreshold = table.Column<int>("int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CatalogItems", x => x.Id);
                table.ForeignKey(
                    "FK_CatalogItems_Brands_CatalogBrandId",
                    x => x.CatalogBrandId,
                    "Brands",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CatalogItems_Types_CatalogTypeId",
                    x => x.CatalogTypeId,
                    "Types",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_CatalogItems_CatalogBrandId",
            "CatalogItems",
            "CatalogBrandId");

        migrationBuilder.CreateIndex(
            "IX_CatalogItems_CatalogTypeId",
            "CatalogItems",
            "CatalogTypeId");

        migrationBuilder.CreateIndex(
            "IX_CatalogItems_Name",
            "CatalogItems",
            "Name");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "CatalogItems");

        migrationBuilder.DropTable(
            "Brands");

        migrationBuilder.DropTable(
            "Types");
    }
}