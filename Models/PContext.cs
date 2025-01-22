using Microsoft.EntityFrameworkCore;
namespace Sample.Models;

public partial class PContext : DbContext
{
    public PContext()
    {
    }

    public PContext(DbContextOptions<PContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs{ get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.RowId);
            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Jobs_Users");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.RowId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


