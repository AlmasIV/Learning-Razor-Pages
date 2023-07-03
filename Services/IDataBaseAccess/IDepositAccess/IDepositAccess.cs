namespace Learning_Razor_Pages.Services.DepositAccess;

public interface IDepositAccess {
    public decimal GetBalance(string userEmail);
    public void Replenish(string userEmail, decimal amount, bool isTransfer = false);
    public void Withdraw(string userEmail, decimal amount, bool isTransfer = false);
    public void Transfer(string userEmail, string toWhom, decimal amount);
}