using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                    new Villa() 
                    {  
                        Id = 1,
                        Name = "Royal Villa",
                        Details = "Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts and visual mockups.",
                        ImageUrl = "https://th.bing.com/th?id=OPA.NvCG2EyglKS01w474C474&w=592&h=550&o=5&pid=21.1",
                        Occupancy = 5,
                        Rate = 200,
                        Sqft = 550,
                        Amenity = "",
                        CreatedDate = DateTime.UtcNow
                    },
                    new Villa()
                    {
                        Id = 2,
                        Name = "Beach Villa",
                        Details = "Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts and visual mockups.",
                        ImageUrl = "https://findepartament.com/transit-native/in/183/3.jpg",
                        Occupancy = 8,
                        Rate = 400,
                        Sqft = 1050,
                        Amenity = "",
                        CreatedDate = DateTime.UtcNow
                    },
                    new Villa()
                    {
                        Id = 3,
                        Name = "Pool Villa",
                        Details = "Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts and visual mockups.",
                        ImageUrl = "https://th.bing.com/th/id/OIP.237S1XUTBtdJ32-y9xFCAwHaE8?pid=ImgDet&rs=1",
                        Occupancy = 10,
                        Rate = 600,
                        Sqft = 1250,
                        Amenity = "",
                        CreatedDate = DateTime.UtcNow
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
