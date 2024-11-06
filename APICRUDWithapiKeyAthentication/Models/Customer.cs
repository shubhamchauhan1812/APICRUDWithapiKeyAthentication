using System.ComponentModel.DataAnnotations;

namespace APICRUDWithapiKeyAthentication.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
