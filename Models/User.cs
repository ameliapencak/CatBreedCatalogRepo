using System.ComponentModel.DataAnnotations;

namespace CatBreedCatalog.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string Role { get; set; } = "User"; // Domyślna rola to "User"
	}
}
