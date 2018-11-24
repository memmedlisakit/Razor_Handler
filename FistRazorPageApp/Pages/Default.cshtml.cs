
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
    public class DefaultModel : PageModel
    {
        private readonly UserDbContext _userDbContext;
        public DefaultModel(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [BindProperty()]
        public User RegisterUser { get; set; }

        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
           
            if (ModelState.IsValid)
            {
              MyUser.User currentUser = await _userDbContext.Users.Where(x => x.Email == RegisterUser.Email).SingleOrDefaultAsync();
                if(currentUser==null)
                {
                   await _userDbContext.Users.AddAsync(RegisterUser);
                   await _userDbContext.SaveChangesAsync();
                    return RedirectToPage("/Login");
                }
                else
                {
                    ModelState.AddModelError("", "this user already exists");
                }
            }
            return Page();
        }
    }
}