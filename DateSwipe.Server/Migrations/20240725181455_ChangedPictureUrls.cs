using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateSwipe.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPictureUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1527574311754-da5f33012338?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fENvdXBsZSUyMFBpY05pY3xlbnwwfHwwfHx8MA%3D%3D");

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1504753793650-d4f2d1d0b13d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y29va2luZyUyMGNsYXNzJTIwY291cGxlfGVufDB8fDB8fHww");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1527574311754-da5f33012338?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fENvdXBsZSUyMFBpY05pY3xlbnwwfHwwfHx8MA%3D%3D&");

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1504753793650-d4f2d1d0b13d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y29va2luZyUyMGNsYXNzJTIwY291cGxlfGVufDB8fDB8fHww&");
        }
    }
}
