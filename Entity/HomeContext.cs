using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HomeContext : DbContext
    {
        const string connectionString = "Server=DESKTOP-A2VCHQ2\\SQLEXPRESS;Database=HomeDb;Trusted_Connection=True;Integrated Security=True";        
        //const string connectionString = "Server=DESKTOP-A2VCHQ2\\SQLEXPRESS;Database=HomeDb;user id=admin;password=123123;Integrated Security=False";

        public HomeContext() : base() { }

        public HomeContext(DbContextOptions<HomeContext> options) : base(options) { }

        public DbSet<Household> Household { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Spending> Spendings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(connectionString);

        }
    }
}
