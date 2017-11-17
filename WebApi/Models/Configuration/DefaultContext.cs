using System.Data.Entity;

namespace WebApi.Models.Configuration
{
    public class DefaultContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    }
}