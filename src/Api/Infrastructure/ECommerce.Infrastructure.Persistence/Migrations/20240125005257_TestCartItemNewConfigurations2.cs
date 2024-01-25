using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TestCartItemNewConfigurations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartitem_shoppingcart_ShoppingCartID",
                schema: "dbo",
                table: "cartitem");

            migrationBuilder.AddForeignKey(
                name: "FK_cartitem_shoppingcart_ShoppingCartID",
                schema: "dbo",
                table: "cartitem",
                column: "ShoppingCartID",
                principalSchema: "dbo",
                principalTable: "shoppingcart",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartitem_shoppingcart_ShoppingCartID",
                schema: "dbo",
                table: "cartitem");

            migrationBuilder.AddForeignKey(
                name: "FK_cartitem_shoppingcart_ShoppingCartID",
                schema: "dbo",
                table: "cartitem",
                column: "ShoppingCartID",
                principalSchema: "dbo",
                principalTable: "shoppingcart",
                principalColumn: "ID");
        }
    }
}
