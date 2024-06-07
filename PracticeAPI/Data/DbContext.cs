using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models;

namespace PracticeAPI.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }
    }
}
