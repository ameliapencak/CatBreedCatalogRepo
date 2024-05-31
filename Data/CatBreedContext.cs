using Microsoft.EntityFrameworkCore;
using CatBreedCatalog.Models;

namespace CatBreedCatalog.Data
{
	public class CatBreedContext : DbContext
	{
		public CatBreedContext(DbContextOptions<CatBreedContext> options)
			: base(options)
		{
		}

		public DbSet<CatBreed> CatBreeds { get; set; }
	}
}
