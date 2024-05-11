using System.Security.Claims;
using Core.Configuration;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Enums;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.EntityFramework.Context;

public class MiniECommerceContext : DbContext
{
    private IHttpContextAccessor? _httpContextAccessor;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(Configuration.PostgreSQLConnectionString);
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var data = ChangeTracker.Entries<BaseEntity>();
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        foreach (var entry in data)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedUserId = _httpContextAccessor!.HttpContext.User.Claims
                        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    entry.Entity.CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    entry.Entity.Status = Status.Active;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdaterUserId = _httpContextAccessor!.HttpContext.User.Claims
                        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    entry.Entity.UpdatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}