using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZAD_10.Migrations
{
    /// <inheritdoc />
    public partial class createtabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    PK_category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.PK_category);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    PK_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false),
                    width = table.Column<double>(type: "float", nullable: false),
                    height = table.Column<double>(type: "float", nullable: false),
                    depth = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.PK_product);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    PK_role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.PK_role);
                });

            migrationBuilder.CreateTable(
                name: "Products_Categories",
                columns: table => new
                {
                    FK_product = table.Column<int>(type: "int", nullable: false),
                    FK_category = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products_Categories", x => new { x.FK_product, x.FK_category });
                    table.ForeignKey(
                        name: "FK_Products_Categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "PK_category");
                    table.ForeignKey(
                        name: "FK_Products_Categories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "PK_product");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    PK_account = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_role = table.Column<int>(type: "int", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.PK_account);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "PK_role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shopping_Carts",
                columns: table => new
                {
                    FK_account = table.Column<int>(type: "int", nullable: false),
                    FK_product = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    Account = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shopping_Carts", x => new { x.FK_account, x.FK_product });
                    table.ForeignKey(
                        name: "FK_Shopping_Carts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "PK_account");
                    table.ForeignKey(
                        name: "FK_Shopping_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "PK_product");
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "PK_account", "email", "first_name", "last_name", "phone", "FK_role", "RoleId" },
                values: new object[] { 1, "bvsdkub@fbh.drg", "Agata", "Mielanska", null, 1, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "PK_category", "name" },
                values: new object[] { 1, "catname" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "PK_product", "depth", "height", "name", "weight", "width" },
                values: new object[] { 1, 20.199999999999999, 30.300000000000001, "prodname", 10.5, 80.099999999999994 });

            migrationBuilder.InsertData(
                table: "Products_Categories",
                columns: new[] { "FK_category", "FK_product", "Category", "CategoryId", "Product", "ProductId" },
                values: new object[] { 1, 1, 0, null, 0, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "PK_role", "name" },
                values: new object[] { 1, "marketing" });

            migrationBuilder.InsertData(
                table: "Shopping_Carts",
                columns: new[] { "FK_account", "FK_product", "Account", "AccountId", "Product", "ProductId", "amount" },
                values: new object[] { 1, 1, 0, null, 0, null, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Categories_CategoryId",
                table: "Products_Categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Categories_ProductId",
                table: "Products_Categories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Shopping_Carts_AccountId",
                table: "Shopping_Carts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Shopping_Carts_ProductId",
                table: "Shopping_Carts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products_Categories");

            migrationBuilder.DropTable(
                name: "Shopping_Carts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
