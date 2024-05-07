using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crims.Data.Migrations
{
    /// <inheritdoc />
    public partial class EstablishmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Establishments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Earnings = table.Column<double>(type: "double precision", nullable: false),
                    SellPrice = table.Column<double>(type: "double precision", nullable: false),
                    RentValue = table.Column<double>(type: "double precision", nullable: false),
                    Limit = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishments", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Establishments");
        }
    }
}
