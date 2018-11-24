using System.Collections.Generic;
using System.Threading.Tasks;
using FistRazorPageApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyUser = FistRazorPageApp.Models;

namespace FistRazorPageApp.Pages
{
    public class ListModel : PageModel
    {
        private readonly UserDbContext _userDbContext;

        public ListModel(UserDbContext userDbContext)
        {
            CurrentUser = new User();
            _userDbContext = userDbContext;
        }

       
        public IEnumerable<User> Users { get; private set; }

        public MyUser.User CurrentUser { get; set; }

        public async Task  OnGetAsync()
        {
          Users = await _userDbContext.Users.ToListAsync();
        }
        
        public async Task<IActionResult> OnPostEditAsync(int id, string username, string useremail, string password)
        {  
            MyUser.User currentUser = await _userDbContext.Users.FindAsync(id);
            if (currentUser != null)
            {
                currentUser.Email = useremail;
                currentUser.Name = username;
                currentUser.Password = password; 
                await this._userDbContext.SaveChangesAsync();
            }
            Users = await _userDbContext.Users.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            Users = await _userDbContext.Users.ToListAsync();
            MyUser.User currentUser = await _userDbContext.Users.FindAsync(id);
            if (currentUser != null)
            {
                CurrentUser = currentUser;
            }
            
                return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            MyUser.User currentUser = await _userDbContext.Users.FindAsync(id);
            if (currentUser != null)
            {
                _userDbContext.Users.Remove(currentUser);
               await _userDbContext.SaveChangesAsync();

            }
            Users = await _userDbContext.Users.ToListAsync();
            return Page();

        }

    }
}