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
		public string FirstName { get; set; }

		[BindProperty]
		public string LastName { get; set; }

		[BindProperty]
		public string Email { get; set; }

		[BindProperty]
		public string Password { get; set; }

		[BindProperty]
		public string ConfirmPassword { get; set; }
		//do przechowywania
		[TempData]
		public string UserEmail { get; set; }

		[TempData]
		public string UserRole { get; set; }

        [TempData]
        public string Message { get; set; }

        [TempData]
        public string MessageType { get; set; }

        public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostRegisterAsync()
		{
			if (Password != ConfirmPassword)
			{
				//ModelState.AddModelError("", "Passwords do not match");
                Message = "Passwords do not match";
                MessageType = "danger";
                return Page();
			}

			var newUser = new User
			{
                FirstName = FirstName,
                LastName = LastName,
				Email = Email,
				Password = Password, 
				Role = "User" 
			};

			_context.Users.Add(newUser);
			await _context.SaveChangesAsync();
			await SignInUser(newUser);

            Message = "Registration successful!";
            MessageType = "success";

            return RedirectToPage("/User");
		}

		public async Task<IActionResult> OnPostLoginAsync()
		{
			var user = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
			if (user == null)
			{
				ModelState.AddModelError("", "Invalid login attempt");
                Message = "Invalid email";
                MessageType = "danger";
                return Page();
			}

            if (user.Password != Password)
            {
                Message = "Invalid password";
                MessageType = "danger";
                return Page();
            }

            await SignInUser(user);
            Message = "Login successful!";
            MessageType = "success";
            return RedirectToPage("/User");
		}

		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Message = "Logout successful!";
            MessageType = "success";
            return RedirectToPage("/User");
		}

		private async Task SignInUser(User user)
		{
			var claims = new List<Claim> //lista roszczeñ
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); //tworzy torzsamoœæ, schemat uwierzytelniania

			var authProperties = new AuthenticationProperties //tworzenie w³aœciwoœci uwirzytelniania
			{
				IsPersistent = true //ma byc trwa³e miedzy sesjami przegl¹darki
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity), authProperties); //logowanie
		}
	}
}
