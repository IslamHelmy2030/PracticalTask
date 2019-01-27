using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticalTask.Data.PracticalDataModel
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
