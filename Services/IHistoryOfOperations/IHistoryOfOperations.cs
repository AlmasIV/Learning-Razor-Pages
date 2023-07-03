using Learning_Razor_Pages.Model.HistoryOfBank;

namespace Learning_Razor_Pages.Services.HistoryOfOperations;

public interface IHistoryOfOperations {
    public List<HistoryOfBank> GetHistoryOfBank(string userEmail);
    public void InsertHistoryOfBank(string userEmail, string operationType, decimal amount, string? toWhom = null);
}