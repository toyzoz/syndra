#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "CardTypes",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_CardTypes", x => x.Id); });

        migrationBuilder.CreateTable(
            "Orders",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BuyerId = table.Column<string>("nvarchar(max)", nullable: false),
                Address_Street = table.Column<string>("nvarchar(max)", nullable: false),
                Address_City = table.Column<string>("nvarchar(max)", nullable: false),
                Address_State = table.Column<string>("nvarchar(max)", nullable: false),
                Address_Country = table.Column<string>("nvarchar(max)", nullable: false),
                Address_ZipCode = table.Column<string>("nvarchar(max)", nullable: false),
                OrderDate = table.Column<DateTime>("datetime2", nullable: false),
                OrderStatus = table.Column<int>("int", nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Orders", x => x.Id); });

        migrationBuilder.CreateTable(
            "OrderItems",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ProductId = table.Column<int>("int", nullable: false),
                ProductName = table.Column<string>("nvarchar(max)", nullable: false),
                PictureUrl = table.Column<string>("nvarchar(max)", nullable: false),
                UnitPrice = table.Column<decimal>("decimal(18,2)", nullable: false),
                Discount = table.Column<decimal>("decimal(18,2)", nullable: false),
                Units = table.Column<int>("int", nullable: false),
                OrderId = table.Column<int>("int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderItems", x => x.Id);
                table.ForeignKey(
                    "FK_OrderItems_Orders_OrderId",
                    x => x.OrderId,
                    "Orders",
                    "Id");
            });

        migrationBuilder.CreateIndex(
            "IX_OrderItems_OrderId",
            "OrderItems",
            "OrderId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "CardTypes");

        migrationBuilder.DropTable(
            "OrderItems");

        migrationBuilder.DropTable(
            "Orders");
    }
}