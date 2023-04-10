using System.ComponentModel.DataAnnotations;

namespace Adornian.Models
{
    public class Jewellery
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
