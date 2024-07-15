using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialWPFApp.Migrations
{
    public partial class Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Status",
                columns: table => new
                {
                    transaction_status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaction_status_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Status", x => x.transaction_status_id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Type",
                columns: table => new
                {
                    transaction_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaction_type_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Type", x => x.transaction_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: true),
                    dob = table.Column<DateTime>(type: "date", nullable: false),
                    registered_date = table.Column<DateTime>(type: "date", nullable: false),
                    phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    avatar = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__AB6E6165725A56AF", x => x.email);
                    table.ForeignKey(
                        name: "FK__Account__role_id__398D8EEE",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    wallet_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    wallet_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    balance = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.wallet_id);
                    table.ForeignKey(
                        name: "FK__Wallet__email__45F365D3",
                        column: x => x.email,
                        principalTable: "Account",
                        principalColumn: "email");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    transaction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    owner = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    from_to = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    transaction_date = table.Column<DateTime>(type: "date", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    transaction_type_id = table.Column<int>(type: "int", nullable: true),
                    transaction_status_id = table.Column<int>(type: "int", nullable: true),
                    wallet_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK__Transacti__owner__48CFD27E",
                        column: x => x.owner,
                        principalTable: "Account",
                        principalColumn: "email");
                    table.ForeignKey(
                        name: "FK__Transacti__trans__49C3F6B7",
                        column: x => x.transaction_type_id,
                        principalTable: "Transaction_Type",
                        principalColumn: "transaction_type_id");
                    table.ForeignKey(
                        name: "FK__Transacti__trans__4AB81AF0",
                        column: x => x.transaction_status_id,
                        principalTable: "Transaction_Status",
                        principalColumn: "transaction_status_id");
                    table.ForeignKey(
                        name: "FK__Transacti__walle__4BAC3F29",
                        column: x => x.wallet_id,
                        principalTable: "Wallet",
                        principalColumn: "wallet_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_role_id",
                table: "Account",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_owner",
                table: "Transaction",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_transaction_status_id",
                table: "Transaction",
                column: "transaction_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_transaction_type_id",
                table: "Transaction",
                column: "transaction_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_wallet_id",
                table: "Transaction",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_email",
                table: "Wallet",
                column: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Transaction_Type");

            migrationBuilder.DropTable(
                name: "Transaction_Status");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
