using Microsoft.EntityFrameworkCore;
using ProductivityApp.Domain.Entities;
using Task = ProductivityApp.Domain.Entities.Task;

namespace ProducitivityApp.DataAccess
{
    public class ProductivityAppDbContext : DbContext
    {
        public ProductivityAppDbContext(DbContextOptions<ProductivityAppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserSession> UserSessions => Set<UserSession>();
        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<Reminder> Reminders => Set<Reminder>();
        public DbSet<OfferToUsers> Offers => Set<OfferToUsers>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Offers

            modelBuilder.Entity<OfferToUsers>()
                .Property(x => x.OfferName)
                .IsRequired();

            modelBuilder.Entity<OfferToUsers>()
                .Property(x => x.OfferMessage)
                .IsRequired();

            //UserSession
            modelBuilder.Entity<UserSession>()
                .Property(x => x.StartTime)
                .IsRequired();

            modelBuilder.Entity<UserSession>()
                .Property(x => x.FinishTime)
                .IsRequired();

            modelBuilder.Entity<UserSession>()
                .Property(x => x.UserSessionLength)
                .IsRequired();

            //UserSession -> User Relation 
            modelBuilder.Entity<UserSession>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserSessions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //User
            modelBuilder.Entity<User>()
                .Property(f => f.Fullname)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();
            //modelBuilder.Entity<User>()
            //    .Property(u => u.Email)
            //    .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .HasMaxLength(30);
                

            //modelBuilder.Entity<User>()
            //    .Property(x => x.Password)
            //    .IsRequired();

            //modelBuilder.Entity<User>()
            //    .Property(x => x.ConfirmPassword)
            //    .IsRequired();

            //Task
            modelBuilder.Entity<Task>()
                .Property(t => t.Title)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(t => t.AssignedTimeDuration)
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(t => t.Note)
                .HasMaxLength(250);


            modelBuilder.Entity<Task>()
                .Property(t => t.Priority)
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(t => t.Pace)
                .IsRequired();

            //Task -> UserSession Relation 
            modelBuilder.Entity<Task>()
                .HasOne(s => s.UserSession)
                .WithMany(t => t.Tasks)
                .HasForeignKey(s => s.UserSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            //Reminder
            modelBuilder.Entity<Reminder>()
                .Property(f => f.ReminderTitle)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Reminder>()
                .Property(f => f.ReminderNote);

            modelBuilder.Entity<Reminder>()
                .Property(l => l.ReminderTime)
                .IsRequired();

            modelBuilder.Entity<Reminder>()
                .Property(l => l.ReminderDate)
                .IsRequired();

            modelBuilder.Entity<Reminder>()
                .Property(u => u.Priority)
                .HasMaxLength(30)
                .IsRequired();

            //Reminder -> User Relation
            modelBuilder.Entity<Reminder>()
                .HasOne(x => x.User)
                .WithMany(x => x.Reminders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
