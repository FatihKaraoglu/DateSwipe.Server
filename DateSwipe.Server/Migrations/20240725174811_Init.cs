using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DateSwipe.Server.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoupleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateIdeas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateIdeas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSubscribed = table.Column<bool>(type: "bit", nullable: false),
                    CoupleId = table.Column<int>(type: "int", nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateIdeaCategories",
                columns: table => new
                {
                    DateIdeaId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateIdeaCategories", x => new { x.DateIdeaId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_DateIdeaCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DateIdeaCategories_DateIdeas_DateIdeaId",
                        column: x => x.DateIdeaId,
                        principalTable: "DateIdeas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSwipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateIdeaId = table.Column<int>(type: "int", nullable: false),
                    Liked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CoupleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSwipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSwipes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Outdoor" },
                    { 2, "Indoor" },
                    { 3, "Romantic" },
                    { 4, "Adventurous" },
                    { 5, "Relaxing" },
                    { 6, "Cultural" }
                });

            migrationBuilder.InsertData(
                table: "DateIdeas",
                columns: new[] { "Id", "Description", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { 1, "Enjoy a fine dining experience.", "https://images.unsplash.com/photo-1614387726083-c445e799102b?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTQ3fHxDb3VwbGUlMjBEaW5uZXJ8ZW58MHx8MHx8fDA%3D", "Dinner at a fancy restaurant" },
                    { 2, "Explore nature on a hiking trail.", "https://images.unsplash.com/photo-1447302325121-30ef3bab8b65?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fEhpa2luZyUyMENvdXBsZXxlbnwwfHwwfHx8MA%3D%3D", "Hiking trip" },
                    { 3, "Watch the latest blockbuster.", "https://images.unsplash.com/photo-1689841497791-7aa38bfa4317?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8Q2luZW1hJTIwQ291cGxlfGVufDB8fDB8fHwy", "Movie night" },
                    { 4, "Relax with a picnic in the park.", "https://images.unsplash.com/photo-1527574311754-da5f33012338?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fENvdXBsZSUyMFBpY05pY3xlbnwwfHwwfHx8MA%3D%3D&", "Picnic in the park" },
                    { 5, "Spend a day at the beach.", "https://images.unsplash.com/photo-1488116593952-937c38246bbe?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OHx8Q29va2luZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D", "Beach day" },
                    { 6, "Learn to cook a new dish together.", "https://images.unsplash.com/photo-1504753793650-d4f2d1d0b13d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y29va2luZyUyMGNsYXNzJTIwY291cGxlfGVufDB8fDB8fHww&", "Cooking class" },
                    { 7, "Sample different wines at a vineyard.", "https://images.unsplash.com/photo-1556710808-a2bc27a448f2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8d2luZSUyMHRhc3RpbmclMjBtYW4lMjB3b21hbnxlbnwwfHwwfHx8Mg%3D%3D", "Wine tasting" },
                    { 8, "Explore art and history at a museum.", "https://images.unsplash.com/photo-1706320291927-ea5b107db646?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTAyfHxtdXN1ZW18ZW58MHx8MHx8fDI%3D", "Museum visit" },
                    { 9, "Enjoy a fun night of bowling.", "https://images.unsplash.com/photo-1570472456794-8578e4bf1fab?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjE4fHxib3dsaW5nfGVufDB8fDB8fHwy", "Bowling night" },
                    { 10, "Watch the stars together in a quiet spot.", "https://images.unsplash.com/photo-1691518039261-350bd2190174?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8ODl8fHN0YXJnYXppbmd8ZW58MHx8MHx8fDI%3D", "Stargazing" },
                    { 11, "Have fun at an amusement park.", "https://images.unsplash.com/photo-1658134636987-a3f9aa63c9af?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjd8fGFtdXNtZW50JTIwcGFya3xlbnwwfHwwfHx8MA%3D%3D", "Amusement park" },
                    { 12, "Watch a play or musical together.", "https://images.unsplash.com/photo-1625843706570-10ff8fd9b796?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTE2fHx0aGVhdGVyfGVufDB8fDB8fHwy", "Theater show" },
                    { 13, "Spend a night under the stars camping.", "https://images.unsplash.com/photo-1552176802-54b65e0f2024?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Njd8fGNhbXBpbmd8ZW58MHx8MHx8fDI%3D", "Camping trip" },
                    { 14, "Take an art class together.", "https://images.unsplash.com/photo-1504252060328-1455a645f4e3?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mzl8fGFydCUyMGNvbXBsZXRpb258ZW58MHx8MHx8fDI%3D", "Art class" },
                    { 15, "Visit a zoo and see exotic animals.", "https://images.unsplash.com/photo-1570197786657-f0b067e3f2dc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NTN8fHpvb3xlbnwwfHwwfHx8Mg%3D%3D", "Zoo visit" },
                    { 16, "Go ice skating at a local rink.", "https://images.unsplash.com/photo-1600721651867-7ab4c12a67f1?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fGljZSUyMHNrYXRpbmd8ZW58MHx8MHx8fDI%3D", "Ice skating" },
                    { 17, "Sing your favorite songs at a karaoke bar.", "https://images.unsplash.com/photo-1563357999-7c1893e1b6ca?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OTB8fGthcmFva2V8ZW58MHx8MHx8fDI%3D", "Karaoke night" },
                    { 18, "Go for a bike ride together.", "https://images.unsplash.com/photo-1562059390-a761a084768d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NjJ8fGJpa2luZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D", "Bike ride" },
                    { 19, "Try horseback riding for a unique experience.", "https://images.unsplash.com/photo-1638103188975-08f3ad6ae623?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTA0fHxob3JzZWJhY2slMjByaWRpbmd8ZW58MHx8MHx8fDI%3D", "Horseback riding" },
                    { 20, "Challenge yourselves with an escape room game.", "https://images.unsplash.com/photo-1585864731036-6ab6fb54f063?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjF8fGVzY2FwZSUyMHJvb218ZW58MHx8MHx8fDI%3D", "Escape room" }
                });

            migrationBuilder.InsertData(
                table: "DateIdeaCategories",
                columns: new[] { "CategoryId", "DateIdeaId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 6, 8 },
                    { 2, 9 },
                    { 1, 10 },
                    { 4, 11 },
                    { 6, 12 },
                    { 4, 13 },
                    { 6, 14 },
                    { 6, 15 },
                    { 4, 16 },
                    { 2, 17 },
                    { 4, 18 },
                    { 4, 19 },
                    { 4, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateIdeaCategories_CategoryId",
                table: "DateIdeaCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSwipes_UserId",
                table: "UserSwipes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "DateIdeaCategories");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "UserSwipes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "DateIdeas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
