using System.ComponentModel.DataAnnotations;

namespace Adornian.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public List<Jewellery>? Jewellery { get; set; }
    }
}
