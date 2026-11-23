using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RepairTransactionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Transactions",
               columns: table => new
               {
                   TransactionId = table.Column<int>(
                       type: "int",
                       nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),

                   TransactionName = table.Column<string>(
                       type: "nvarchar(max)",
                       nullable: false),

                   CustomerId = table.Column<int>(
                       type: "int",
                       nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey(
                       "PK_Transactions",
                       x => x.TransactionId);

                   table.ForeignKey(
                       name: "FK_Transactions_Customers_CustomerId",
                       column: x => x.CustomerId,
                       principalTable: "Customers",
                       principalColumn: "CustomerId",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Transactions");
        }
    }
}
