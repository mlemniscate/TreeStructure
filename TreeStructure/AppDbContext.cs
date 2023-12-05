using Microsoft.EntityFrameworkCore;

namespace TreeStructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Folder> Folders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name);
            entity.HasOne(x => x.Parent)
                .WithMany(x => x.SubFolders)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}