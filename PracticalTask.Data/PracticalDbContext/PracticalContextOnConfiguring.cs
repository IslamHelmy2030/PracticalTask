using Microsoft.EntityFrameworkCore;

namespace PracticalTask.Data.PracticalDbContext
{
    public partial class PracticalContext
    {
        public PracticalContext(DbContextOptions<PracticalContext> options):base(options)
        {
            
        }
        
    }
}
