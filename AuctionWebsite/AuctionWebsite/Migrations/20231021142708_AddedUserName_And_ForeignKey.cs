using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserName_And_ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "BidDbs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuctionDbs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "BidDbs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuctionDbs");
        }
    }
}
