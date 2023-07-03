using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Learning_Razor_Pages.Model;
using Learning_Razor_Pages.Services.UserAccess;

namespace Learning_Razor_Pages
{
    [AllowAnonymous]
    public class SignUpModel : PageModel
    {
        [BindProperty(SupportsGet = false)]
        public User? user { get; set; }

        private readonly IUserAccess userAccessService;

        public SignUpModel(IUserAccess userAccess){
            userAccessService = userAccess;
        }

        public void OnGet()
        {
    
        }
        public IActionResult OnPost(){
            if(!ModelState.IsValid){
                return Page();
            }
            (bool isEmailExists, bool isPhoneExists) uniqueness = userAccessService.IsExists(user.Email, user.PhoneNumber);
            if(uniqueness.isEmailExists || uniqueness.isPhoneExists){
                if(uniqueness.isEmailExists){
                    TempData["EmailExists"] = "The email is already in use.";
                }
                if(uniqueness.isPhoneExists){
                    TempData["PhoneExists"] = "The phone number is already in use.";
                }
                return Page();
            }
            userAccessService.Insert(user);
            return RedirectToPage("LogIn");
        }
    }
}
