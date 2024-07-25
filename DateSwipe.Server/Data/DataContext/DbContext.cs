using DateSwipe.Shared;
using Microsoft.EntityFrameworkCore;

namespace DateSwipe.Server.Data.DataContext
{
    public class DatingDbContext : DbContext
    {
        public DatingDbContext(DbContextOptions<DatingDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<DateIdea> DateIdeas { get; set; }
        public DbSet<UserSwipe> UserSwipes { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DateIdeaCategory> DateIdeaCategories { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DateIdea>().HasKey(d => d.Id);

            modelBuilder.Entity<UserSwipe>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSwipes)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSwipes)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<DateIdeaCategory>()
                .HasKey(dic => new { dic.DateIdeaId, dic.CategoryId });

            modelBuilder.Entity<DateIdeaCategory>()
                .HasOne(dic => dic.DateIdea)
                .WithMany(di => di.DateIdeaCategories)
                .HasForeignKey(dic => dic.DateIdeaId);

            modelBuilder.Entity<DateIdeaCategory>()
                .HasOne(dic => dic.Category)
                .WithMany(c => c.DateIdeaCategories)
                .HasForeignKey(dic => dic.CategoryId);

            modelBuilder.Entity<DateIdea>().HasData(
                           new DateIdea { Id = 1, Title = "Dinner at a fancy restaurant", Description = "Enjoy a fine dining experience.", ImageUrl = "https://images.unsplash.com/photo-1614387726083-c445e799102b?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTQ3fHxDb3VwbGUlMjBEaW5uZXJ8ZW58MHx8MHx8fDA%3D" },
                            new DateIdea { Id = 2, Title = "Hiking trip", Description = "Explore nature on a hiking trail.", ImageUrl = "https://images.unsplash.com/photo-1447302325121-30ef3bab8b65?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fEhpa2luZyUyMENvdXBsZXxlbnwwfHwwfHx8MA%3D%3D" },
                            new DateIdea { Id = 3, Title = "Movie night", Description = "Watch the latest blockbuster.", ImageUrl = "https://images.unsplash.com/photo-1689841497791-7aa38bfa4317?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8Q2luZW1hJTIwQ291cGxlfGVufDB8fDB8fHwy" },
                            new DateIdea { Id = 4, Title = "Picnic in the park", Description = "Relax with a picnic in the park.", ImageUrl = "https://images.unsplash.com/photo-1527574311754-da5f33012338?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fENvdXBsZSUyMFBpY05pY3xlbnwwfHwwfHx8MA%3D%3D&" },
                            new DateIdea { Id = 5, Title = "Beach day", Description = "Spend a day at the beach.", ImageUrl = "https://images.unsplash.com/photo-1488116593952-937c38246bbe?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OHx8Q29va2luZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D" },
                            new DateIdea { Id = 6, Title = "Cooking class", Description = "Learn to cook a new dish together.", ImageUrl = "https://images.unsplash.com/photo-1504753793650-d4f2d1d0b13d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y29va2luZyUyMGNsYXNzJTIwY291cGxlfGVufDB8fDB8fHww&" },
                            new DateIdea { Id = 7, Title = "Wine tasting", Description = "Sample different wines at a vineyard.", ImageUrl = "https://images.unsplash.com/photo-1556710808-a2bc27a448f2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8d2luZSUyMHRhc3RpbmclMjBtYW4lMjB3b21hbnxlbnwwfHwwfHx8Mg%3D%3D" },
                            new DateIdea { Id = 8, Title = "Museum visit", Description = "Explore art and history at a museum.", ImageUrl = "https://images.unsplash.com/photo-1706320291927-ea5b107db646?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTAyfHxtdXN1ZW18ZW58MHx8MHx8fDI%3D" },
                            new DateIdea { Id = 9, Title = "Bowling night", Description = "Enjoy a fun night of bowling.", ImageUrl = "https://images.unsplash.com/photo-1570472456794-8578e4bf1fab?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjE4fHxib3dsaW5nfGVufDB8fDB8fHwy" },
                            new DateIdea { Id = 10, Title = "Stargazing", Description = "Watch the stars together in a quiet spot.", ImageUrl = "https://images.unsplash.com/photo-1691518039261-350bd2190174?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8ODl8fHN0YXJnYXppbmd8ZW58MHx8MHx8fDI%3D" },
                            new DateIdea { Id = 11, Title = "Amusement park", Description = "Have fun at an amusement park.", ImageUrl = "https://images.unsplash.com/photo-1658134636987-a3f9aa63c9af?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjd8fGFtdXNtZW50JTIwcGFya3xlbnwwfHwwfHx8Mg%3D%3D" },
                            new DateIdea { Id = 12, Title = "Concert", Description = "Attend a live music concert.", ImageUrl = "https://images.unsplash.com/photo-1514533450685-4493e01d1fdc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGNvbmNlcnR8ZW58MHx8MHx8fDI%3D" },
                            new DateIdea { Id = 13, Title = "Camping", Description = "Spend a night under the stars camping.", ImageUrl = "https://images.unsplash.com/photo-1539183204366-63a0589187ab?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fGNhbXBpbmd8ZW58MHx8MHx8fDI%3D" },
                            new DateIdea { Id = 14, Title = "Ice skating", Description = "Enjoy ice skating together.", ImageUrl = "https://images.unsplash.com/photo-1609987051964-7bc15e54e9bc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8SWNlJTIwU2thdGluZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D" },
                            new DateIdea { Id = 15, Title = "Karaoke night", Description = "Sing your heart out at a karaoke bar.", ImageUrl = "https://images.unsplash.com/photo-1516108759901-daf1a20f9281?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8c2luZ2luZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 16, Title = "Road trip", Description = "Take a spontaneous road trip.", ImageUrl = "https://images.unsplash.com/photo-1495610379499-a1f03b4732a7?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fHJvYWR0cmlwfGVufDB8fDB8fHwy" },
                          new DateIdea { Id = 17, Title = "Hot air balloon ride", Description = "Experience a hot air balloon ride.", ImageUrl = "https://images.unsplash.com/photo-1502119095323-253837f293f9?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGhvdCUyMGFpciUyMGJhbGxvb258ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 18, Title = "Escape room", Description = "Solve puzzles in an escape room.", ImageUrl = "https://images.unsplash.com/photo-1580256079206-46a04a632758?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzV8fGVzY2FwZXJvb218ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 19, Title = "Mini golf", Description = "Have fun playing mini golf.", ImageUrl = "https://images.unsplash.com/photo-1647884756876-9b230e2f75a9?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bWluaWF0dXJlJTIwZ29sZnxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 20, Title = "Pottery class", Description = "Make pottery together in a class.", ImageUrl = "https://images.unsplash.com/photo-1635304438525-106382337f9b?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8cG90dGVyeXxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 21, Title = "Theater play", Description = "Watch a live theater play.", ImageUrl = "https://images.unsplash.com/photo-1503095396549-807759245b35?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8dGhlYXRlcnxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 22, Title = "Zoo visit", Description = "See animals at the zoo.", ImageUrl = "https://images.unsplash.com/photo-1503918756811-975bd3397178?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8em9vfGVufDB8fDB8fHwy" },
                          new DateIdea { Id = 23, Title = "Biking", Description = "Go for a bike ride together.", ImageUrl = "https://images.unsplash.com/photo-1476345692822-c597e09a0331?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGJpa2luZ3xlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 24, Title = "Farmers market", Description = "Explore a local farmers market.", ImageUrl = "https://images.unsplash.com/photo-1514583079045-e928a4732ade?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fGZhcm1lcnMlMjBtYXJrZXR8ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 25, Title = "Horseback riding", Description = "Go horseback riding.", ImageUrl = "https://images.unsplash.com/photo-1633767979501-6225d151ba70?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8aG9yc2ViYWNrJTIwcmlkaW5nfGVufDB8fDB8fHwy" },
                          new DateIdea { Id = 26, Title = "Cooking at home", Description = "Cook a meal together at home.", ImageUrl = "https://images.unsplash.com/photo-1556910103-1c02745aae4d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNvb2tpbmd8ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 27, Title = "Board games", Description = "Play board games.", ImageUrl = "https://images.unsplash.com/photo-1708863827400-00a5c21c10f7?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTB8fGJvYXJkZ2FtZXN8ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 28, Title = "Volunteer together", Description = "Volunteer at a local charity.", ImageUrl = "https://images.unsplash.com/photo-1618477460930-d8bffff64172?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTh8fHZvbHVudGVlcmluZ3xlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 29, Title = "Fishing", Description = "Go fishing together.", ImageUrl = "https://images.unsplash.com/photo-1657982469733-fbf83ebdab8f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8ZmlzaGluZ3xlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 30, Title = "Dance class", Description = "Take a dance class.", ImageUrl = "https://images.unsplash.com/photo-1504609813442-a8924e83f76e?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGRhbmNlJTIwY2xhc3N8ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 31, Title = "Photography day", Description = "Take photos together.", ImageUrl = "https://images.unsplash.com/photo-1719937206498-b31844530a96?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDF8MHxzZWFyY2h8MXx8cGhvdG9ncmFwaHl8ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 32, Title = "Spa day", Description = "Relax with a spa day.", ImageUrl = "https://images.unsplash.com/photo-1600334129128-685c5582fd35?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fHNwYXxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 33, Title = "Cooking competition", Description = "Have a cooking competition.", ImageUrl = "https://images.unsplash.com/photo-1576097449798-7c7f90e1248a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjR8fGNvb2tpbmclMjBjb21wZXRpdGlvbnxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 34, Title = "Trivia night", Description = "Enjoy a trivia night.", ImageUrl = "https://images.unsplash.com/photo-1570937943292-a574bd5bc722?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8dHJpdmlhfGVufDB8fDB8fHwy" },
                          new DateIdea { Id = 35, Title = "Plant a garden", Description = "Plant a garden together.", ImageUrl = "https://images.unsplash.com/photo-1597868165956-03a6827955b1?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Z2FyZGVuJTIwd29ya3xlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 36, Title = "Art gallery", Description = "Visit an art gallery.", ImageUrl = "https://images.unsplash.com/photo-1637680298164-74342b63a61a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8YXJ0JTIwZ2FsbGVyeXxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 37, Title = "Bookstore date", Description = "Browse books at a bookstore.", ImageUrl = "https://images.unsplash.com/photo-1671202867630-c897313a0a22?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8Ym9va3N0b3JlfGVufDB8fDB8fHwy" },
                          new DateIdea { Id = 38, Title = "Surfing", Description = "Go surfing together.", ImageUrl = "https://images.unsplash.com/photo-1502933691298-84fc14542831?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fHN1cmZpbmd8ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 39, Title = "Jet skiing", Description = "Experience jet skiing.", ImageUrl = "https://images.unsplash.com/photo-1650939489556-a6fc47e28372?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8amV0JTIwc2tpaW5nfGVufDB8fDB8fHwy" },
                          new DateIdea { Id = 40, Title = "Rock climbing", Description = "Go rock climbing.", ImageUrl = "https://images.unsplash.com/photo-1522362485439-83fcff4673f0?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cm9jayUyMGNsaW1iaW5nfGVufDB8fDB8fHwy" },
                          new DateIdea { Id = 42, Title = "Yoga class", Description = "Attend a yoga class together.", ImageUrl = "https://images.unsplash.com/photo-1517363898874-737b62a7db91?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fHlvZ2F8ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 43, Title = "Dog park", Description = "Visit a dog park with your pets.", ImageUrl = "https://images.unsplash.com/photo-1667230228326-c881966e2a29?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8ZG9ncGFya3xlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 44, Title = "Aquarium visit", Description = "Explore an aquarium.", ImageUrl = "https://images.unsplash.com/photo-1459207982041-089ff95be891?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8YXF1YXJpdW18ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 45, Title = "Trampoline park", Description = "Have fun at a trampoline park.", ImageUrl = "https://images.unsplash.com/photo-1626817654900-c07008365317?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjB8fHRyYW1wb2xpbmV8ZW58MHx8MHx8fDI%3D" },
                          new DateIdea { Id = 46, Title = "Laser tag", Description = "Play laser tag.", ImageUrl = "https://images.unsplash.com/photo-1542810205-0a5b379f9c52?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fGxhc2VyJTIwdGFnfGVufDB8fDB8fHwy" },
                          new DateIdea { Id = 47, Title = "Go-kart racing", Description = "Race go-karts together.", ImageUrl = "https://images.unsplash.com/photo-1505570554449-69ce7d4fa36b?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fGdvJTIwa2FydHxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 48, Title = "Painting class", Description = "Take a painting class.", ImageUrl = "https://images.unsplash.com/photo-1476055090065-a605fefd840e?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cGFpbnRpbmclMjBhY3Rpdml0eXxlbnwwfHwwfHx8Mg%3D%3D" },
                          new DateIdea { Id = 50, Title = "Skiing", Description = "Go skiing together.", ImageUrl = "https://images.unsplash.com/photo-1498576260462-eefc9d0ce9f7?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8c2tpaW5nfGVufDB8fDB8fHwy" }
                      );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Outdoor" },
                new Category { Id = 2, Name = "Indoor" },
                new Category { Id = 3, Name = "Romantic" },
                new Category { Id = 4, Name = "Adventurous" },
                new Category { Id = 5, Name = "Relaxing" },
                new Category { Id = 6, Name = "Cultural" }
            );

            modelBuilder.Entity<DateIdeaCategory>().HasData(
                new DateIdeaCategory { DateIdeaId = 1, CategoryId = 2 },
                new DateIdeaCategory { DateIdeaId = 2, CategoryId = 1 },
                new DateIdeaCategory { DateIdeaId = 3, CategoryId = 2 },
                new DateIdeaCategory { DateIdeaId = 4, CategoryId = 1 },
                new DateIdeaCategory { DateIdeaId = 5, CategoryId = 1 },
                new DateIdeaCategory { DateIdeaId = 6, CategoryId = 2 },
                new DateIdeaCategory { DateIdeaId = 7, CategoryId = 2 },
                new DateIdeaCategory { DateIdeaId = 8, CategoryId = 6 },
                new DateIdeaCategory { DateIdeaId = 9, CategoryId = 2 },
                new DateIdeaCategory { DateIdeaId = 10, CategoryId = 1 },
                new DateIdeaCategory { DateIdeaId = 11, CategoryId = 4 },
                new DateIdeaCategory { DateIdeaId = 12, CategoryId = 6 },
                new DateIdeaCategory { DateIdeaId = 13, CategoryId = 4 },
                new DateIdeaCategory { DateIdeaId = 14, CategoryId = 6 },
                new DateIdeaCategory { DateIdeaId = 15, CategoryId = 6 },
                new DateIdeaCategory { DateIdeaId = 16, CategoryId = 4 },
                new DateIdeaCategory { DateIdeaId = 17, CategoryId = 2 },
                new DateIdeaCategory { DateIdeaId = 18, CategoryId = 4 },
                new DateIdeaCategory { DateIdeaId = 19, CategoryId = 4 },
                new DateIdeaCategory { DateIdeaId = 20, CategoryId = 4 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
