using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DateSwipe.Server.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
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
                name: "DateProposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoupleId = table.Column<int>(type: "int", nullable: false),
                    DateIdeaId = table.Column<int>(type: "int", nullable: false),
                    Accept = table.Column<bool>(type: "bit", nullable: true),
                    Canceled = table.Column<bool>(type: "bit", nullable: false),
                    DateProposalIssuer = table.Column<int>(type: "int", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateProposals_DateIdeas_DateIdeaId",
                        column: x => x.DateIdeaId,
                        principalTable: "DateIdeas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PushSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Endpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P256DH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PushSubscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCategoryPreferences",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Liked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategoryPreferences", x => new { x.UserId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_UserCategoryPreferences_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCategoryPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    CoupleId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PlannedDates",
                columns: table => new
                {
                    PlannedDateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoupleId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateIdeaId = table.Column<int>(type: "int", nullable: false),
                    DateProposalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedDates", x => x.PlannedDateId);
                    table.ForeignKey(
                        name: "FK_PlannedDates_DateIdeas_DateIdeaId",
                        column: x => x.DateIdeaId,
                        principalTable: "DateIdeas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlannedDates_DateProposals_DateProposalId",
                        column: x => x.DateProposalId,
                        principalTable: "DateProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    { 11, "Have fun at an amusement park.", "https://images.unsplash.com/photo-1658134636987-a3f9aa63c9af?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjd8fGFtdXNtZW50JTIwcGFya3xlbnwwfHwwfHx8Mg%3D%3D", "Amusement park" },
                    { 12, "Attend a live music concert.", "https://images.unsplash.com/photo-1514533450685-4493e01d1fdc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGNvbmNlcnR8ZW58MHx8MHx8fDI%3D", "Concert" },
                    { 13, "Spend a night under the stars camping.", "https://images.unsplash.com/photo-1539183204366-63a0589187ab?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fGNhbXBpbmd8ZW58MHx8MHx8fDI%3D", "Camping" },
                    { 14, "Enjoy ice skating together.", "https://images.unsplash.com/photo-1609987051964-7bc15e54e9bc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8SWNlJTIwU2thdGluZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D", "Ice skating" },
                    { 15, "Sing your heart out at a karaoke bar.", "https://images.unsplash.com/photo-1516108759901-daf1a20f9281?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8c2luZ2luZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D", "Karaoke night" },
                    { 16, "Take a spontaneous road trip.", "https://images.unsplash.com/photo-1495610379499-a1f03b4732a7?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fHJvYWR0cmlwfGVufDB8fDB8fHwy", "Road trip" },
                    { 17, "Experience a hot air balloon ride.", "https://images.unsplash.com/photo-1502119095323-253837f293f9?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGhvdCUyMGFpciUyMGJhbGxvb258ZW58MHx8MHx8fDI%3D", "Hot air balloon ride" },
                    { 18, "Solve puzzles in an escape room.", "https://images.unsplash.com/photo-1580256079206-46a04a632758?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzV8fGVzY2FwZXJvb218ZW58MHx8MHx8fDI%3D", "Escape room" },
                    { 19, "Have fun playing mini golf.", "https://images.unsplash.com/photo-1647884756876-9b230e2f75a9?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bWluaWF0dXJlJTIwZ29sZnxlbnwwfHwwfHx8Mg%3D%3D", "Mini golf" },
                    { 20, "Make pottery together in a class.", "https://images.unsplash.com/photo-1635304438525-106382337f9b?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8cG90dGVyeXxlbnwwfHwwfHx8Mg%3D%3D", "Pottery class" },
                    { 21, "Watch a live theater play.", "https://images.unsplash.com/photo-1503095396549-807759245b35?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8dGhlYXRlcnxlbnwwfHwwfHx8Mg%3D%3D", "Theater play" },
                    { 22, "See animals at the zoo.", "https://images.unsplash.com/photo-1503918756811-975bd3397178?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8em9vfGVufDB8fDB8fHwy", "Zoo visit" },
                    { 23, "Go for a bike ride together.", "https://images.unsplash.com/photo-1476345692822-c597e09a0331?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGJpa2luZ3xlbnwwfHwwfHx8Mg%3D%3D", "Biking" },
                    { 24, "Explore a local farmers market.", "https://images.unsplash.com/photo-1514583079045-e928a4732ade?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fGZhcm1lcnMlMjBtYXJrZXR8ZW58MHx8MHx8fDI%3D", "Farmers market" },
                    { 25, "Go horseback riding.", "https://images.unsplash.com/photo-1633767979501-6225d151ba70?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8aG9yc2ViYWNrJTIwcmlkaW5nfGVufDB8fDB8fHwy", "Horseback riding" },
                    { 26, "Cook a meal together at home.", "https://images.unsplash.com/photo-1556910103-1c02745aae4d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNvb2tpbmd8ZW58MHx8MHx8fDI%3D", "Cooking at home" },
                    { 27, "Play board games.", "https://images.unsplash.com/photo-1708863827400-00a5c21c10f7?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTB8fGJvYXJkZ2FtZXN8ZW58MHx8MHx8fDI%3D", "Board games" },
                    { 28, "Volunteer at a local charity.", "https://images.unsplash.com/photo-1618477460930-d8bffff64172?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTh8fHZvbHVudGVlcmluZ3xlbnwwfHwwfHx8Mg%3D%3D", "Volunteer together" },
                    { 29, "Go fishing together.", "https://images.unsplash.com/photo-1657982469733-fbf83ebdab8f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8ZmlzaGluZ3xlbnwwfHwwfHx8Mg%3D%3D", "Fishing" },
                    { 30, "Take a dance class.", "https://images.unsplash.com/photo-1504609813442-a8924e83f76e?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGRhbmNlJTIwY2xhc3N8ZW58MHx8MHx8fDI%3D", "Dance class" },
                    { 31, "Take photos together.", "https://images.unsplash.com/photo-1719937206498-b31844530a96?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDF8MHxzZWFyY2h8MXx8cGhvdG9ncmFwaHl8ZW58MHx8MHx8fDI%3D", "Photography day" },
                    { 32, "Relax with a spa day.", "https://images.unsplash.com/photo-1600334129128-685c5582fd35?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fHNwYXxlbnwwfHwwfHx8Mg%3D%3D", "Spa day" },
                    { 33, "Have a cooking competition.", "https://images.unsplash.com/photo-1576097449798-7c7f90e1248a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjR8fGNvb2tpbmclMjBjb21wZXRpdGlvbnxlbnwwfHwwfHx8Mg%3D%3D", "Cooking competition" },
                    { 34, "Enjoy a trivia night.", "https://images.unsplash.com/photo-1570937943292-a574bd5bc722?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8dHJpdmlhfGVufDB8fDB8fHwy", "Trivia night" },
                    { 35, "Plant a garden together.", "https://images.unsplash.com/photo-1597868165956-03a6827955b1?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Z2FyZGVuJTIwd29ya3xlbnwwfHwwfHx8Mg%3D%3D", "Plant a garden" },
                    { 36, "Visit an art gallery.", "https://images.unsplash.com/photo-1637680298164-74342b63a61a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8YXJ0JTIwZ2FsbGVyeXxlbnwwfHwwfHx8Mg%3D%3D", "Art gallery" },
                    { 37, "Browse books at a bookstore.", "https://images.unsplash.com/photo-1671202867630-c897313a0a22?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8Ym9va3N0b3JlfGVufDB8fDB8fHwy", "Bookstore date" },
                    { 38, "Go surfing together.", "https://images.unsplash.com/photo-1502933691298-84fc14542831?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fHN1cmZpbmd8ZW58MHx8MHx8fDI%3D", "Surfing" },
                    { 39, "Experience jet skiing.", "https://images.unsplash.com/photo-1650939489556-a6fc47e28372?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8amV0JTIwc2tpaW5nfGVufDB8fDB8fHwy", "Jet skiing" },
                    { 40, "Go rock climbing.", "https://images.unsplash.com/photo-1522362485439-83fcff4673f0?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cm9jayUyMGNsaW1iaW5nfGVufDB8fDB8fHwy", "Rock climbing" },
                    { 42, "Attend a yoga class together.", "https://images.unsplash.com/photo-1517363898874-737b62a7db91?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fHlvZ2F8ZW58MHx8MHx8fDI%3D", "Yoga class" },
                    { 43, "Visit a dog park with your pets.", "https://images.unsplash.com/photo-1667230228326-c881966e2a29?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8ZG9ncGFya3xlbnwwfHwwfHx8Mg%3D%3D", "Dog park" },
                    { 44, "Explore an aquarium.", "https://images.unsplash.com/photo-1459207982041-089ff95be891?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8YXF1YXJpdW18ZW58MHx8MHx8fDI%3D", "Aquarium visit" },
                    { 45, "Have fun at a trampoline park.", "https://images.unsplash.com/photo-1626817654900-c07008365317?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjB8fHRyYW1wb2xpbmV8ZW58MHx8MHx8fDI%3D", "Trampoline park" },
                    { 46, "Play laser tag.", "https://images.unsplash.com/photo-1542810205-0a5b379f9c52?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fGxhc2VyJTIwdGFnfGVufDB8fDB8fHwy", "Laser tag" },
                    { 47, "Race go-karts together.", "https://images.unsplash.com/photo-1505570554449-69ce7d4fa36b?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fGdvJTIwa2FydHxlbnwwfHwwfHx8Mg%3D%3D", "Go-kart racing" },
                    { 48, "Take a painting class.", "https://images.unsplash.com/photo-1476055090065-a605fefd840e?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cGFpbnRpbmclMjBhY3Rpdml0eXxlbnwwfHwwfHx8Mg%3D%3D", "Painting class" },
                    { 50, "Go skiing together.", "https://images.unsplash.com/photo-1498576260462-eefc9d0ce9f7?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8c2tpaW5nfGVufDB8fDB8fHwy", "Skiing" }
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
                name: "IX_DateProposals_DateIdeaId",
                table: "DateProposals",
                column: "DateIdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedDates_DateIdeaId",
                table: "PlannedDates",
                column: "DateIdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedDates_DateProposalId",
                table: "PlannedDates",
                column: "DateProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_PushSubscriptions_UserId",
                table: "PushSubscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategoryPreferences_CategoryId",
                table: "UserCategoryPreferences",
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
                name: "PlannedDates");

            migrationBuilder.DropTable(
                name: "PushSubscriptions");

            migrationBuilder.DropTable(
                name: "UserCategoryPreferences");

            migrationBuilder.DropTable(
                name: "UserSwipes");

            migrationBuilder.DropTable(
                name: "DateProposals");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DateIdeas");
        }
    }
}
