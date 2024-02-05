using HotelReservation.Models;
using HotelReservation.Models.Reservations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<BillingInfo> BillingInfos { get; set; }
    public DbSet<Guest> Guests { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Room>()
    .Property(r => r.AvailableFrom)
    .HasColumnType("timestamp with time zone");

        modelBuilder.Entity<Room>()
            .Property(r => r.AvailableTo)
            .HasColumnType("timestamp with time zone");

    }

}