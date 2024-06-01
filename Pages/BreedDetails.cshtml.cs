using CatBreedCatalog.Data;
using CatBreedCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatBreedCatalog.Pages
{
    public class BreedDetailsModel : PageModel
    {
		private readonly CatBreedContext _context;

		public BreedDetailsModel(CatBreedContext context)
		{
			_context = context;
		}

		public CatBreed? CatBreed { get; private set; }

		public async Task OnGetAsync(int id)
		{
			CatBreed = await _context.CatBreeds.FindAsync(id);

			
		}
		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			if (!User.IsInRole("Admin"))
			{
				return Unauthorized();
			}

			var catBreed = await _context.CatBreeds.FindAsync(id);

			if (catBreed != null)
			{
				_context.CatBreeds.Remove(catBreed);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("/Index");
		}
	}
}
