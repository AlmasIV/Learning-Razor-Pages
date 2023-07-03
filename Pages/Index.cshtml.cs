using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Learning_Razor_Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
