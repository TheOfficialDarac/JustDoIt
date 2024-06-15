using JustDoIt.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JustDoIt.DAL
{
    public partial class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(optionsBuilder.con)
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=task_manager; Integrated Security=True; Trust Server Certificate=True");
        }

        

    }
}
