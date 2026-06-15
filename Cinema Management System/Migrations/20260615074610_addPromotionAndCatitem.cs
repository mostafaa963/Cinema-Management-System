using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class addPromotionAndCatitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductPromotion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Usage = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPromotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPromotion_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPromotion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationuserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPromotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPromotion_AspNetUsers_ApplicationuserId",
                        column: x => x.ApplicationuserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPromotion_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionUserUsage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationuserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productPromotionId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionUserUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionUserUsage_AspNetUsers_ApplicationuserId",
                        column: x => x.ApplicationuserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionUserUsage_ProductPromotion_productPromotionId",
                        column: x => x.productPromotionId,
                        principalTable: "ProductPromotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPromotion_MovieId",
                table: "ProductPromotion",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionUserUsage_ApplicationuserId",
                table: "PromotionUserUsage",
                column: "ApplicationuserId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionUserUsage_productPromotionId",
                table: "PromotionUserUsage",
                column: "productPromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPromotion_ApplicationuserId",
                table: "UserPromotion",
                column: "ApplicationuserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPromotion_MovieId",
                table: "UserPromotion",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionUserUsage");

            migrationBuilder.DropTable(
                name: "UserPromotion");

            migrationBuilder.DropTable(
                name: "ProductPromotion");
        }
    }
}
