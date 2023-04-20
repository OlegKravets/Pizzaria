using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzaria.Migrations
{
    public partial class RewriteModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBeef",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "IsDoubleCheese",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "IsTomato",
                table: "Pizzas");

            migrationBuilder.AddColumn<bool>(
                name: "IsBeef",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDoubleCheese",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTomato",
                table: "PizzaOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBeef",
                table: "PizzaOrders");

            migrationBuilder.DropColumn(
                name: "IsDoubleCheese",
                table: "PizzaOrders");

            migrationBuilder.DropColumn(
                name: "IsTomato",
                table: "PizzaOrders");

            migrationBuilder.AddColumn<bool>(
                name: "IsBeef",
                table: "Pizzas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDoubleCheese",
                table: "Pizzas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTomato",
                table: "Pizzas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
