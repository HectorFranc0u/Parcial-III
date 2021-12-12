using Microsoft.EntityFrameworkCore;
using webAPI.Models;

namespace webAPI.Data
{
    public class DatabaseContext : DbContext
    {
        internal object gifts;

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }

        public DbSet<Furniture> furnitures {get;set;}   
        public DbSet<Gift> gift {get;set;}
    }
}