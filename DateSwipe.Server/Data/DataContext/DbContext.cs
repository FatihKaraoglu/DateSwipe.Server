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
                new DateIdea { Id = 11, Title = "Amusement park", Description = "Have fun at an amusement park.", ImageUrl = "https://images.unsplash.com/photo-1658134636987-a3f9aa63c9af?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjd8fGFtdXNtZW50JTIwcGFya3xlbnwwfHwwfHx8MA%3D%3D" },
                new DateIdea { Id = 12, Title = "Theater show", Description = "Watch a play or musical together.", ImageUrl = "https://images.unsplash.com/photo-1625843706570-10ff8fd9b796?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTE2fHx0aGVhdGVyfGVufDB8fDB8fHwy" },
                new DateIdea { Id = 13, Title = "Camping trip", Description = "Spend a night under the stars camping.", ImageUrl = "https://images.unsplash.com/photo-1552176802-54b65e0f2024?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Njd8fGNhbXBpbmd8ZW58MHx8MHx8fDI%3D" },
                new DateIdea { Id = 14, Title = "Art class", Description = "Take an art class together.", ImageUrl = "https://images.unsplash.com/photo-1504252060328-1455a645f4e3?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mzl8fGFydCUyMGNvbXBsZXRpb258ZW58MHx8MHx8fDI%3D" },
                new DateIdea { Id = 15, Title = "Zoo visit", Description = "Visit a zoo and see exotic animals.", ImageUrl = "https://images.unsplash.com/photo-1570197786657-f0b067e3f2dc?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NTN8fHpvb3xlbnwwfHwwfHx8Mg%3D%3D" },
                new DateIdea { Id = 16, Title = "Ice skating", Description = "Go ice skating at a local rink.", ImageUrl = "https://images.unsplash.com/photo-1600721651867-7ab4c12a67f1?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fGljZSUyMHNrYXRpbmd8ZW58MHx8MHx8fDI%3D" },
                new DateIdea { Id = 17, Title = "Karaoke night", Description = "Sing your favorite songs at a karaoke bar.", ImageUrl = "https://images.unsplash.com/photo-1563357999-7c1893e1b6ca?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OTB8fGthcmFva2V8ZW58MHx8MHx8fDI%3D" },
                new DateIdea { Id = 18, Title = "Bike ride", Description = "Go for a bike ride together.", ImageUrl = "https://images.unsplash.com/photo-1562059390-a761a084768d?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NjJ8fGJpa2luZyUyMGNvdXBsZXxlbnwwfHwwfHx8Mg%3D%3D" },
                new DateIdea { Id = 19, Title = "Horseback riding", Description = "Try horseback riding for a unique experience.", ImageUrl = "https://images.unsplash.com/photo-1638103188975-08f3ad6ae623?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTA0fHxob3JzZWJhY2slMjByaWRpbmd8ZW58MHx8MHx8fDI%3D" },
                new DateIdea { Id = 20, Title = "Escape room", Description = "Challenge yourselves with an escape room game.", ImageUrl = "https://images.unsplash.com/photo-1585864731036-6ab6fb54f063?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjF8fGVzY2FwZSUyMHJvb218ZW58MHx8MHx8fDI%3D" }
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
