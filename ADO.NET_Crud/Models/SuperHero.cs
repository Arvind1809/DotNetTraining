using System.ComponentModel.DataAnnotations;

namespace ADO.NET_Crud.Models
{
    public class SuperHero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } =null!;
        public long? PhoneNumber { get; set; }
    }
}
