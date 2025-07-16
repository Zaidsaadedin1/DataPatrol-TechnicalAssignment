using Microsoft.EntityFrameworkCore;
using Cores.Entities;

namespace Cores.ApplicationDbContext
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; } = null!;
        public DbSet<PolicyTable> Policies { get; set; } = null!;
        public DbSet<UserGroup> UserGroups { get; set; } = null!;
        public DbSet<UserRequest> UserRequests { get; set; } = null!;
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
