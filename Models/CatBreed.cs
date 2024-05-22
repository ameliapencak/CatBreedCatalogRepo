using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CatBreedCatalog.Models
{
    public class CatBreed : Controller
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Origin { get; set; }

        [Required]
        [StringLength(50)]
        public string Temperament { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }

}
