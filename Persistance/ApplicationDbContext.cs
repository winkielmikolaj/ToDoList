using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models.Domains;

namespace ToDoList.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Core.Models.Domains.Task> Tasks { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
