using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using HotelReservation.Models;

public static class DbSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!context.Rooms.Any() && !context.Users.Any())
        {
            SeedRoles(roleManager);
            SeedAdminUser(userManager);

            SeedRooms(context);
        }
    }

    private static void SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!roleManager.RoleExistsAsync(role).Result)
            {
                roleManager.CreateAsync(new IdentityRole(role)).Wait();
            }
        }
    }

    private static void SeedAdminUser(UserManager<ApplicationUser> userManager)
    {
        var adminUser = new ApplicationUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "User",
            UserRole = "Admin"
        };

        var result = userManager.CreateAsync(adminUser, "Admin@123").Result;

        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(adminUser, "Admin").Wait();
        }
    }

    private static void SeedRooms(ApplicationDbContext context)
    {
        var rooms = new List<Room>
        {
            new Room
            {
                RoomNumber = "101",
                Type = "Standard suite rooms",
                Description = "The Chalet Champagne room is the ideal room to celebrate a special occasion.\r\nMaximum comfort for a romantic stay: toast to love in the private jacuzzi on the terrace under the star.\r\nGlass shower and bathtub with panoramic views",
                Capacity = 2,
                Price = 29.00m,
                PictureUrl1 = "/images/room2-1.jpg",
                PictureUrl2 = "/images/room2-2.jpg",
                PictureUrl3 = "/images/room2-3.jpg",
                Available = true,
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(30),
                Amenities = new List<Amenities> { Amenities.WiFi, Amenities.Toiletries, Amenities.AirConditioning, Amenities.FreeParking, Amenities.TV }
            },
            new Room
            {
                RoomNumber = "102",
                Type = "Standard suite rooms",
                Description = "Room divided on two floors to give privacy and space even to large families. A special design and tailored to make your holiday unforgettable.",
                Capacity = 4,
                Price = 29.00m,
                PictureUrl1 = "/images/room3-1.jpg",
                PictureUrl2 = "/images/room3-2.jpg",
                PictureUrl3 = "/images/room3-3.jpg",
                Available = true,
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(30),
                Amenities = new List<Amenities> { Amenities.WiFi, Amenities.Toiletries, Amenities.AirConditioning, Amenities.FreeParking, Amenities.TV }
            },
            new Room
            {
                RoomNumber = "201",
                Type = "Presidential suites",
                Description = "Room divided on two floors: ground floor with private bathroom, shower and double bed, mezzanine floor with double bed, bathroom, bathtub and glass open space shower.\r\nBalcony with large windows to admire our beloved Cima Presanella and private Jacuzzi.\r\nA particular and tailor-made design to make your holiday unforgettable.",
                Capacity = 2,
                Price = 39.00m,
                PictureUrl1 = "/images/room1-1.jpg",
                PictureUrl2 = "/images/room1-2.jpg",
                PictureUrl3 = "/images/room1-outside.jpg",
                Available = true,
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(30),
                Amenities = new List<Amenities> { Amenities.WiFi, Amenities.Toiletries, Amenities.AirConditioning, Amenities.FreeParking, Amenities.TV }
            },
            new Room
            {
                RoomNumber = "301",
                Type = "Penthouse suites",
                Description = "The Penthouse room is the top for those who love rustic and refined environments. Everything is studied in detail to recall the warm atmosphere that distinguishes the Chalet.\r\nSolid wood bed, large open space glass shower inside the room and bathtub. It enjoys a large rooftop panoramic terrrace with a view of the Presanella Ice and private Jacuzzi.",
                Capacity = 2,
                Price = 100.00m,
                PictureUrl1 = "/images/penthouse-1.jpg",
                PictureUrl2 = "/images/penthouse-2.jpg",
                PictureUrl3 = "/images/penthouse-3.jpg",
                Available = true,
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(30),
                Amenities = new List<Amenities> { Amenities.WiFi, Amenities.Toiletries, Amenities.AirConditioning, Amenities.FreeParking, Amenities.TV }
            },
        };

        context.Rooms.AddRange(rooms);
        context.SaveChanges();
    }
}
