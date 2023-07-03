using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using Learning_Razor_Pages.Services.DepositAccess;
using Learning_Razor_Pages.Model.HistoryOfBank;
using Learning_Razor_Pages.Services.HistoryOfOperations;
using Learning_Razor_Pages.Services.UserAccess;
using Learning_Razor_Pages.Model;

namespace Learning_Razor_Pages
{
    [Authorize]
    public class MyBankModel : PageModel
    {
        [BindProperty, Required, Range(100, 999_999_999_999_999.99)]
        public decimal Amount { get; set; }

        public decimal Balance = 0;
        public List<HistoryOfBank>? History;
        private string? _userEmail = null;

        // Services injected by DI.
        private readonly IDepositAccess depositAccessService;
        private readonly IHistoryOfOperations historyOfOperationsService;
        private readonly IUserAccess userAccessService;
        public MyBankModel(IDepositAccess depositAccess, IHistoryOfOperations historyOfOperations, IUserAccess userAccess){
            depositAccessService = depositAccess;
            historyOfOperationsService = historyOfOperations;
            userAccessService = userAccess;
        }

        // Page handlers.
        public void OnGet()
        {
            _InitializeUserEmailAndBalance();
            _UpdateHistory();
        }
        public IActionResult OnPostReplenishMoney(){
            if(!ModelState.IsValid){
                return Page();
            }
            _InitializeUserEmailAndBalance();
            depositAccessService.Replenish(_userEmail!, Amount);
            _InitializeUserEmailAndBalance();
            _UpdateHistory();
            return Page();
        }
        public IActionResult OnPostWithdrawMoney(){
            if(!ModelState.IsValid){
                return Page();
            }
            _InitializeUserEmailAndBalance();
            if(!_IsEnoughMoney()){
                return Page();
            }
            depositAccessService.Withdraw(_userEmail!, Amount);
            _InitializeUserEmailAndBalance();
            _UpdateHistory();
            return Page();
        }

        public IActionResult OnPostTransferMoney(){
            _InitializeUserEmailAndBalance();
            string? toWhom = Request.Form["toWhom"];
            if(string.IsNullOrEmpty(toWhom)){
                TempData["ReceiverNull"] = "The receiver's email can't be empty";
                return Page();
            }
            User? receiver = userAccessService.Retrieve(toWhom);
            if(receiver is null){
                TempData["NoSuchUser"] = "The receiver doesn't exist.";
                return Page();
            }
            if(receiver.Email == _userEmail){
                TempData["YourOwnEmail"] = "You can't transfer to yourself.";
                return Page();
            }
            if(_IsEnoughMoney()){
                depositAccessService.Transfer(_userEmail!, receiver.Email!, Amount);
                _InitializeUserEmailAndBalance();
                _UpdateHistory();
            }
            return Page();
        }

        // Helper methods.
        private void _InitializeUserEmailAndBalance(){
            _userEmail = _userEmail == null ? User.FindFirstValue(ClaimTypes.Email) : _userEmail;
            Balance = depositAccessService.GetBalance(_userEmail!);
        }
        private void _UpdateHistory(){
            History = historyOfOperationsService.GetHistoryOfBank(_userEmail!);
        }
        private bool _IsEnoughMoney(){
            if(Balance < Amount){
                TempData["NotEnough"] = "You don't have enough money.";
                return false;
            }
            return true;
        }
    }
}
