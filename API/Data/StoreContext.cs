using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class StoreContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Product> Products { get; set; }

    public required DbSet<Basket> Baskets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole { Id = "5fc5c41f-de96-4180-94e1-eee6d64dd383", Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole { Id = "c1761d08-68e8-4cd1-996d-359e2837d967", Name = "Admin", NormalizedName = "ADMIN" }
            );
    }
}