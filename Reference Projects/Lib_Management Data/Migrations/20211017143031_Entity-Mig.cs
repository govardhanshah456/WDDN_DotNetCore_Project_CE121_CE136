using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lib_Management_Data.Migrations
{
    public partial class EntityMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PhoneNum",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customer",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customer",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeLibraryBranchId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customer",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LibraryCardId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LibraryBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryBranch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibraryCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    OpenTime = table.Column<int>(type: "int", nullable: false),
                    CloseTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchHours_LibraryBranch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "LibraryBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibraryAsset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfCopies = table.Column<int>(type: "int", nullable: false),
                    AssetType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeweyIndex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryAsset_LibraryBranch_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LibraryBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibraryAsset_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checkout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryAssetId = table.Column<int>(type: "int", nullable: true),
                    LibraryCardId = table.Column<int>(type: "int", nullable: true),
                    Since = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Until = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkout_LibraryAsset_LibraryAssetId",
                        column: x => x.LibraryAssetId,
                        principalTable: "LibraryAsset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checkout_LibraryCard_LibraryCardId",
                        column: x => x.LibraryCardId,
                        principalTable: "LibraryCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryAssetId = table.Column<int>(type: "int", nullable: true),
                    LibraryCardId = table.Column<int>(type: "int", nullable: true),
                    CheckedOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckedIn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutHistory_LibraryAsset_LibraryAssetId",
                        column: x => x.LibraryAssetId,
                        principalTable: "LibraryAsset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckoutHistory_LibraryCard_LibraryCardId",
                        column: x => x.LibraryCardId,
                        principalTable: "LibraryCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hold",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryAssetId = table.Column<int>(type: "int", nullable: true),
                    LibraryCardId = table.Column<int>(type: "int", nullable: true),
                    HoldPlaced = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hold", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hold_LibraryAsset_LibraryAssetId",
                        column: x => x.LibraryAssetId,
                        principalTable: "LibraryAsset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hold_LibraryCard_LibraryCardId",
                        column: x => x.LibraryCardId,
                        principalTable: "LibraryCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_HomeLibraryBranchId",
                table: "Customer",
                column: "HomeLibraryBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LibraryCardId",
                table: "Customer",
                column: "LibraryCardId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchHours_BranchId",
                table: "BranchHours",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_LibraryAssetId",
                table: "Checkout",
                column: "LibraryAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_LibraryCardId",
                table: "Checkout",
                column: "LibraryCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutHistory_LibraryAssetId",
                table: "CheckoutHistory",
                column: "LibraryAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutHistory_LibraryCardId",
                table: "CheckoutHistory",
                column: "LibraryCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Hold_LibraryAssetId",
                table: "Hold",
                column: "LibraryAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Hold_LibraryCardId",
                table: "Hold",
                column: "LibraryCardId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAsset_LocationId",
                table: "LibraryAsset",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAsset_StatusId",
                table: "LibraryAsset",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_LibraryBranch_HomeLibraryBranchId",
                table: "Customer",
                column: "HomeLibraryBranchId",
                principalTable: "LibraryBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_LibraryCard_LibraryCardId",
                table: "Customer",
                column: "LibraryCardId",
                principalTable: "LibraryCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_LibraryBranch_HomeLibraryBranchId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_LibraryCard_LibraryCardId",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "BranchHours");

            migrationBuilder.DropTable(
                name: "Checkout");

            migrationBuilder.DropTable(
                name: "CheckoutHistory");

            migrationBuilder.DropTable(
                name: "Hold");

            migrationBuilder.DropTable(
                name: "LibraryAsset");

            migrationBuilder.DropTable(
                name: "LibraryCard");

            migrationBuilder.DropTable(
                name: "LibraryBranch");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Customer_HomeLibraryBranchId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_LibraryCardId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "HomeLibraryBranchId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LibraryCardId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customer",
                newName: "CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNum",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
