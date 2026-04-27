using Microsoft.EntityFrameworkCore;
using ParkShareApi.Models;

namespace ParkShareApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<ParkingSpace> ParkingSpaces { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Rating)
            .HasPrecision(3, 1);

        // ParkingSpace
        modelBuilder.Entity<ParkingSpace>()
            .HasOne(s => s.Owner)
            .WithMany(u => u.OwnedSpaces)
            .HasForeignKey(s => s.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParkingSpace>()
            .Property(s => s.PricePerDay).HasPrecision(10, 2);
        modelBuilder.Entity<ParkingSpace>()
            .Property(s => s.PricePerWeek).HasPrecision(10, 2);
        modelBuilder.Entity<ParkingSpace>()
            .Property(s => s.PricePerMonth).HasPrecision(10, 2);
        modelBuilder.Entity<ParkingSpace>()
            .Property(s => s.PricePerQuarter).HasPrecision(10, 2);
        modelBuilder.Entity<ParkingSpace>()
            .Property(s => s.Rating).HasPrecision(3, 1);
        modelBuilder.Entity<ParkingSpace>()
            .Property(s => s.LengthM).HasPrecision(6, 2);
        modelBuilder.Entity<ParkingSpace>()
            .Property(s => s.WidthM).HasPrecision(6, 2);
        modelBuilder.Entity<ParkingSpace>()
            .Property(s => s.HeightM).HasPrecision(6, 2);

        // Booking
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Space)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.SpaceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Seeker)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.SeekerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .Property(b => b.TotalAmount).HasPrecision(10, 2);

        // Chat
        modelBuilder.Entity<Chat>()
            .HasOne(c => c.Seeker)
            .WithMany()
            .HasForeignKey(c => c.SeekerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Chat>()
            .HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Message
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
