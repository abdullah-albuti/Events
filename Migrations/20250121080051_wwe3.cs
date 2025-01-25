using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Migrations
{
    /// <inheritdoc />
    public partial class wwe3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Event_ID",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Event_ID",
                table: "Bookings");
        }
    }
}
