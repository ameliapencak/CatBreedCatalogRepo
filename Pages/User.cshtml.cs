using CatBreedCatalog.Data;
using CatBreedCatalog.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CatBreedCatalog.Pages
{
	public class UserModel : PageModel
	{
		private readonly CatBreedContext _context;

		public UserModel(CatBreedContext context)
		{
			_context = context;
		}

		[BindProperty]
		public string FullName { get; set; }

		[BindProperty]
		public string Username { get; set; }

		[BindProperty]
		public string Email { get; set; }

		[BindProperty]
		public string Password { get; set; }

		[BindProperty]
		public string ConfirmPassword { get; set; }

		[TempData]
		public string UserEmail { get; set; }

		[TempData]
		public string UserRole { get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostRegisterAsync()
		{
			if (Password != ConfirmPassword)
			{
				ModelState.AddModelError("", "Passwords do not match");
				return Page();
			}

			var newUser = new User
			{
				FullName = FullName,
				Username = Username,
				Email = Email,
				Password = Password, // In a real application, make sure to hash the password
				Role = "User" // Domyœlna rola to "User"
			};

			_context.Users.Add(newUser);
			await _context.SaveChangesAsync();
			await SignInUser(newUser);

			return RedirectToPage("/Index");
		}

		public async Task<IActionResult> OnPostLoginAsync()
		{
			var user = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
			if (user == null)
			{
				ModelState.AddModelError("", "Invalid login attempt");
				return Page();
			}
			await SignInUser(user);
			return RedirectToPage("/Index");
		}

		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}

		private async Task SignInUser(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				IsPersistent = true
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity), authProperties);
		}
	}
}
