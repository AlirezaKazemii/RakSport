using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Models.ContentFeature;
using RakSport.Domain.Models.ContentImage;
using RakSport.Domain.Models.ContentSection;
using RakSport.Domain.Models.FAQ;
using RakSport.Domain.Models.FutsalHall;
using RakSport.Domain.Models.HallFeature;
using RakSport.Domain.Models.HallImage;
using RakSport.Domain.Models.MartialHall;
using RakSport.Domain.Models.Notification;
using RakSport.Domain.Models.OtpCode;
using RakSport.Domain.Models.Payment;
using RakSport.Domain.Models.PoolHall;
using RakSport.Domain.Models.Reservation;
using RakSport.Domain.Models.ReservationItem;
using RakSport.Domain.Models.Review;
using RakSport.Domain.Models.SportHall;
using RakSport.Domain.Models.Ticket;
using RakSport.Domain.Models.TicketMessage;
using RakSport.Domain.Models.TimeSlot;
using RakSport.Domain.Models.User;

namespace RakSport.Infrastructure.DbContext
{
    public class DataBaseContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<ContentFeatureEntity> ContentFeatures { get; set; }
        public DbSet<ContentImageEntity> ContentImages { get; set; }
        public DbSet<ContentSectionEntity> ContentSections { get; set; }
        public DbSet<FAQEntity> Faqs { get; set; }
        public DbSet<FutsalHallEntity> FutsalHalls { get; set; }
        public DbSet<HallFeatureEntity> HallFeatures { get; set; }
        public DbSet<HallImageEntity> HallImages { get; set; }
        public DbSet<MartialHallEntity> MartialHalls { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<OtpCodeEntity> OtpCodes { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<PoolHallEntity> PoolHalls { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<ReservationItemEntity> ReservationItems { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<SportHallEntity> SportHalls { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<TicketMessageEntity> TicketMessages { get; set; }
        public DbSet<TimeSlotEntity> TimeSlots { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>()
                .Property(u => u.Email)
                .IsRequired(false);

            builder.Entity<UserEntity>()
                .Property(u => u.NormalizedEmail)
                .IsRequired(false);

            // TPH Configuration
            builder.Entity<SportHallEntity>()
                .HasDiscriminator<string>("SportHallType")
                .HasValue<PoolHallEntity>("SwimmingPool")
                .HasValue<FutsalHallEntity>("Futsal")
                .HasValue<MartialHallEntity>("MartialArts");


            builder.Entity<UserEntity>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserEntity>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserEntity>()
                .HasMany(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserEntity>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ReservationEntity>()
                .HasMany(r => r.ReservationItems)
                .WithOne(r => r.Reservation)
                .HasForeignKey(r => r.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ReservationEntity>()
                .HasOne(r => r.Payment)
                .WithOne(p => p.Reservation)
                .HasForeignKey<PaymentEntity>(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TimeSlotEntity>()
                .HasMany(t => t.reservationItems)
                .WithOne(r => r.TimeSlot)
                .HasForeignKey(r => r.TimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SportHallEntity>()
                .HasMany(s => s.TimeSlots)
                .WithOne(t => t.SportHall)
                .HasForeignKey(t => t.SportHallId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SportHallEntity>()
                .HasMany(s => s.Features)
                .WithOne(f => f.SportHall)
                .HasForeignKey(f => f.SportHallId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SportHallEntity>()
                .HasMany(s => s.Images)
                .WithOne(i => i.sportHall)
                .HasForeignKey(i => i.SportHallId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SportHallEntity>()
                .HasMany(s => s.reviews)
                .WithOne(r => r.sportHall)
                .HasForeignKey(r => r.SportHallId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SportHallEntity>()
                .HasMany(s => s.ContentSectionEntities)
                .WithOne(c => c.SportHall)
                .HasForeignKey(c => c.SportHallId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketEntity>()
                .HasMany(t => t.TicketMessages)
                .WithOne(t => t.Ticket)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ContentSectionEntity>()
                .HasMany(c => c.Images)
                .WithOne(i => i.ContentSection)
                .HasForeignKey(i => i.ContentSectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ContentSectionEntity>()
                .HasMany(c => c.Features)
                .WithOne(f => f.ContentSection)
                .HasForeignKey(f => f.ContentSectionId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
