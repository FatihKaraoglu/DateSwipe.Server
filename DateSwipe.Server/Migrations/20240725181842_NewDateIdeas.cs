using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DateSwipe.Server.Migrations
{
    /// <inheritdoc />
    public partial class NewDateIdeas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1658134636987-a3f9aa63c9af?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjd8fGFtdXNtZW50JTIwcGFya3xlbnwwfHwwfHx8Mg%3D%3D");

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Attend a live music concert.", "https://images.unsplash.com/photo-1514533450685-4493e01d1fdc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGNvbmNlcnR8ZW58MHx8MHx8fDI%3D", "Concert" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ImageUrl", "Title" },
                values: new object[] { "https://images.unsplash.com/photo-1539183204366-63a0589187ab?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fGNhbXBpbmd8ZW58MHx8MHx8fDI%3D", "Camping" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Enjoy ice skating together.", "https://images.unsplash.com/photo-1609987051964-7bc15e54e9bc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8SWNlJTIwU2thdGluZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D", "Ice skating" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Sing your heart out at a karaoke bar.", "https://images.unsplash.com/photo-1516108759901-daf1a20f9281?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8c2luZ2luZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D", "Karaoke night" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Take a spontaneous road trip.", "https://images.unsplash.com/photo-1495610379499-a1f03b4732a7?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fHJvYWR0cmlwfGVufDB8fDB8fHwy", "Road trip" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Experience a hot air balloon ride.", "https://images.unsplash.com/photo-1502119095323-253837f293f9?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGhvdCUyMGFpciUyMGJhbGxvb258ZW58MHx8MHx8fDI%3D", "Hot air balloon ride" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Solve puzzles in an escape room.", "https://images.unsplash.com/photo-1580256079206-46a04a632758?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzV8fGVzY2FwZXJvb218ZW58MHx8MHx8fDI%3D", "Escape room" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Have fun playing mini golf.", "https://images.unsplash.com/photo-1647884756876-9b230e2f75a9?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bWluaWF0dXJlJTIwZ29sZnxlbnwwfHwwfHx8Mg%3D%3D", "Mini golf" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Make pottery together in a class.", "https://images.unsplash.com/photo-1635304438525-106382337f9b?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8cG90dGVyeXxlbnwwfHwwfHx8Mg%3D%3D", "Pottery class" });

            migrationBuilder.InsertData(
                table: "DateIdeas",
                columns: new[] { "Id", "Description", "ImageUrl", "Title" },
                values: new object[,]
                {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 50);

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

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1658134636987-a3f9aa63c9af?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjd8fGFtdXNtZW50JTIwcGFya3xlbnwwfHwwfHx8MA%3D%3D");

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Watch a play or musical together.", "https://images.unsplash.com/photo-1625843706570-10ff8fd9b796?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTE2fHx0aGVhdGVyfGVufDB8fDB8fHwy", "Theater show" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ImageUrl", "Title" },
                values: new object[] { "https://images.unsplash.com/photo-1552176802-54b65e0f2024?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Njd8fGNhbXBpbmd8ZW58MHx8MHx8fDI%3D", "Camping trip" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Take an art class together.", "https://images.unsplash.com/photo-1504252060328-1455a645f4e3?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mzl8fGFydCUyMGNvbXBsZXRpb258ZW58MHx8MHx8fDI%3D", "Art class" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Visit a zoo and see exotic animals.", "https://images.unsplash.com/photo-1570197786657-f0b067e3f2dc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NTN8fHpvb3xlbnwwfHwwfHx8Mg%3D%3D", "Zoo visit" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Go ice skating at a local rink.", "https://images.unsplash.com/photo-1600721651867-7ab4c12a67f1?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fGljZSUyMHNrYXRpbmd8ZW58MHx8MHx8fDI%3D", "Ice skating" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Sing your favorite songs at a karaoke bar.", "https://images.unsplash.com/photo-1563357999-7c1893e1b6ca?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OTB8fGthcmFva2V8ZW58MHx8MHx8fDI%3D", "Karaoke night" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Go for a bike ride together.", "https://images.unsplash.com/photo-1562059390-a761a084768d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NjJ8fGJpa2luZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D", "Bike ride" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Try horseback riding for a unique experience.", "https://images.unsplash.com/photo-1638103188975-08f3ad6ae623?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTA0fHxob3JzZWJhY2slMjByaWRpbmd8ZW58MHx8MHx8fDI%3D", "Horseback riding" });

            migrationBuilder.UpdateData(
                table: "DateIdeas",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Challenge yourselves with an escape room game.", "https://images.unsplash.com/photo-1585864731036-6ab6fb54f063?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjF8fGVzY2FwZSUyMHJvb218ZW58MHx8MHx8fDI%3D", "Escape room" });
        }
    }
}
