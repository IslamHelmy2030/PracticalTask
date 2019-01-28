using Microsoft.EntityFrameworkCore;
using PracticalTask.Data.PracticalDataModel;

namespace PracticalTask.Data.PracticalDbContext
{
    public partial class PracticalContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        //IDesignTimeDbContextFactory<>
    }
}
