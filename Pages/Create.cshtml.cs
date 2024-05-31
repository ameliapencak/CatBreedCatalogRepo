using CatBreedCatalog.Data;
using CatBreedCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatBreedCatalog.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CatBreedContext _context;

        public CreateModel(CatBreedContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CatBreed CatBreed { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newCatBreed = new CatBreed
            {
                Name = CatBreed.Name,
                Description = CatBreed.Description,
                Origin = CatBreed.Origin,
                Temperament = CatBreed.Temperament
            };

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        newCatBreed.ImageData = memoryStream.ToArray();
                    }
                }
            }

            _context.CatBreeds.Add(newCatBreed);
            await _context.SaveChangesAsync();

            return RedirectToPage("/BreedDetails", new { id = newCatBreed.Id });
        }
    }
}
