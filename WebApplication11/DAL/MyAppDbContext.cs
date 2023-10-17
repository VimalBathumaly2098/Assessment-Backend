using Microsoft.EntityFrameworkCore;
using WebApplication11.Models;

namespace WebApplication11.DAL
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
