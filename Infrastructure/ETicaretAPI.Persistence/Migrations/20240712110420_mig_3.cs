using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    public partial class mig_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_Customerid",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Customerid",
                table: "Orders",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Customerid",
                table: "Orders",
                newName: "IX_Orders_customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_customerId",
                table: "Orders",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_customerId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Orders",
                newName: "Customerid");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_customerId",
                table: "Orders",
                newName: "IX_Orders_Customerid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_Customerid",
                table: "Orders",
                column: "Customerid",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
