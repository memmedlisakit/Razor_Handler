using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FistRazorPageApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyUser = FistRazorPageApp.Models;

namespace FistRazorPageApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserDbContext _userDbContext;
        public LoginModel(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [BindProperty()]
        public UserLoginModel UserLogin { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                MyUser.User currentUser = await _userDbContext.Users.Where(x => x.Email == UserLogin.Email && x.Password == UserLogin.Password).FirstOrDefaultAsync();

                if (currentUser ==null)
                {
                    ModelState.AddModelError("", "This user is not valid");
                }
                else
                {
                    return RedirectToPage("/List");
                }
            }
            return Page();
        }
    }
}