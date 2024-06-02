using CatBreedCatalog.Data;
using CatBreedCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace CatBreedCatalog.Pages
{
    public class EditBreedModel : PageModel
    {
        private readonly CatBreedContext _context;

        public EditBreedModel(CatBreedContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CatBreed CatBreed { get; set; }

		public async Task<IActionResult> OnGetAsync(int id)
        {
            CatBreed = await _context.CatBreeds.FindAsync(id);

            if (CatBreed == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var breedToUpdate = await _context.CatBreeds.FindAsync(CatBreed.Id);

            if (breedToUpdate == null)
            {
                return NotFound();
            }

            // Aktualizacja w³aœciwoœci rekordu w bazie danych
            breedToUpdate.Name = CatBreed.Name;
            breedToUpdate.Description = CatBreed.Description;
            breedToUpdate.Origin = CatBreed.Origin;
            breedToUpdate.Temperament = CatBreed.Temperament;

            // Aktualizacja obrazu, jeœli nowy zosta³ za³adowany
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        breedToUpdate.ImageData = memoryStream.ToArray();
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatBreedExists(CatBreed.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/BreedDetails", new { id = breedToUpdate.Id });
        }

        private bool CatBreedExists(int id)
        {
            return _context.CatBreeds.Any(e => e.Id == id);
        }
    }
}
