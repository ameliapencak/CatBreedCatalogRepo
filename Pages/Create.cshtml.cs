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

        [BindProperty]  //CatBreed bêdzie automatycznie powi¹zana z danymi formularza przes³anymi do serwera.
		public CatBreed CatBreed { get; set; }

        public void OnGet() //nic nie jest pobierane
        {
        }

        public async Task<IActionResult> OnPostAsync() // wykonuje sie przy rzadaniu post
        {
            if (!ModelState.IsValid) // czy pola poprawie wypelnione
            {
                return Page();
            }

            var newCatBreed = new CatBreed //nowa instancja
            {
                Name = CatBreed.Name,
                Description = CatBreed.Description,
                Origin = CatBreed.Origin,
                Temperament = CatBreed.Temperament
            };

            if (Request.Form.Files.Count > 0) //czy plik przeslano
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream()) // strumieñ pamieci
                    {
                        await file.CopyToAsync(memoryStream); //kopiuje
                        newCatBreed.ImageData = memoryStream.ToArray(); //Przypisuje dane obrazu do Imagedata
					}
                }
            }

            _context.CatBreeds.Add(newCatBreed); //dodaje do bazy
            await _context.SaveChangesAsync(); //zapis

            return RedirectToPage("/BreedDetails", new { id = newCatBreed.Id });
        }
    }
}
