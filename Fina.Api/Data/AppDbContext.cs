using Fina.Api.Data.Mappings;
using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fina.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-BLIKA9R; Database=Fina; Uid=sa; Pwd=!Z2x525q1; Trusted_Connection=False; TrustServerCertificate=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Transaction> Transaction { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CategoryMapping());
            //modelBuilder.ApplyConfiguration(new TransactionMapping());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
