using CatBreedCatalog.Data;
using CatBreedCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CatBreedCatalog.Pages
{
    public class IndexModel : PageModel
    {
		private readonly CatBreedContext _context;


		public IndexModel(CatBreedContext context)
        {
			_context = context;
		}

		public IList<CatBreed>? CatBreeds { get; private set; }

		public async Task OnGetAsync()
		{
			CatBreeds = await _context.CatBreeds.ToListAsync();
		}
	}
}