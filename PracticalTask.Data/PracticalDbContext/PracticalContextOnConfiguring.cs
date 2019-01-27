using Microsoft.EntityFrameworkCore;

namespace PracticalTask.Data.PracticalDbContext
{
    public partial class PracticalContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=.;initial catalog=EvolvicPracticalTask;integrated security = True; MultipleActiveResultSets =True;");
        }
    }
}
