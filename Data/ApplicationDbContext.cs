using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tpAuth.Models;

namespace tpAuth.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<WhishList> whishLists { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = Guid.Parse("92ede3c2-f5a4-425c-a037-3ad42153ef7b"), Title = "Inception", Genre = "Action" },
            new Movie { Id = Guid.Parse("3b0a8b2d-ef2a-42e7-9b4c-7b17118d8da0"), Title = "Interstellar", Genre = "Sci-Fi" }
        );
    }
}