using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

using Learning_Razor_Pages.Model;
using Learning_Razor_Pages.Services;
using Learning_Razor_Pages.Services.UserAccess;

using Microsoft.AspNetCore.Authorization;

namespace Learning_Razor_Pages
{
    [AllowAnonymous]
    public class LogInModel : PageModel
    {
        [BindProperty(SupportsGet = false), Required(ErrorMessage = "The email address is required."), EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        [BindProperty(SupportsGet = false), Required(ErrorMessage = "The password is required.")]
        public string? Password { get; set; }


        private readonly IUserAccess userAccessService;
        private readonly ICookiesAuthentication cookiesAuthentication;
        public LogInModel(IUserAccess userAccess, ICookiesAuthentication _cookiesAuthentication){
            userAccessService = userAccess;
            cookiesAuthentication = _cookiesAuthentication;
        }
        public void OnGet(){}
        
        public async Task<IActionResult> OnPost(){
            if(!ModelState.IsValid){
                return Page();
            }
            
            User? user = userAccessService.Retrieve(Email!);
            if(user is not null){
                if(user.Password == Password){
                    await cookiesAuthentication.SignInAsync(user.Email!);
                    return RedirectToPage("../Index");
                }
                else{
                    TempData["IncorrectPassword"] = "The password is incorrect.";
                    return Page();
                }
            }
            else{
                TempData["IncorrectEmail"] = "The email address isn't correct or you are not registered.";
                return Page();
            }
        }
    }
}