using Microsoft.EntityFrameworkCore;
using WebShell.Models;

namespace WebShell.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CommandModel> Commands { get; set; }
    }
}
