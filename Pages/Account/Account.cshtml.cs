using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Learning_Razor_Pages.Services.UserAccess;
using Learning_Razor_Pages.Model;
using System.Security.Claims;

namespace Learning_Razor_Pages
{
    [Authorize]
    public class AccountModel : PageModel
    {
        public User user { get; private set; }
        private readonly IUserAccess userAccessService;
        public AccountModel(IUserAccess userAccess){
            userAccessService = userAccess;
        }
        public void OnGet()
        {
            user = userAccessService.Retrieve(User.FindFirstValue(ClaimTypes.Email)!)!;
        }
    }
}
