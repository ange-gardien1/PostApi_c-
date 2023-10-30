using tweeter_api.Models;
using Microsoft.EntityFrameworkCore;

namespace tweeter_api.Migrations;

public class TweeterDbContext : DbContext
{
    public DbSet<Tweet> Tweets { get; set; }
    public DbSet<User> Users { get; set; }

    public TweeterDbContext(DbContextOptions<TweeterDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany<Tweet>(u => u.Tweets)
            .WithOne(t => t.User)
            .HasForeignKey(u => u.UserId);

        modelBuilder.Entity<Tweet>(entity =>
        {
            entity.HasKey(e => e.TweetId);
            entity.Property(e => e.Message).IsRequired();
            entity.HasIndex(e => e.UserId);
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("datetime()");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired();
            entity.HasIndex(x => x.Username).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.State).IsRequired();
            entity.Property(e => e.City).IsRequired();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("datetime()");
        });


    }
}