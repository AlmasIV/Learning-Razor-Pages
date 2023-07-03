
namespace Learning_Razor_Pages.Model.HistoryOfBank;

public class HistoryOfBank {
    public DateTime DateOfOperation { get; set; }
    public string OperationType { get; set; }
    public decimal Amount { get; set; }
    public string? ToWhom { get; set; }
    public string? FromWhom { get; set; }

    public HistoryOfBank(DateTime dateOfOperation, string operationType, decimal amount, string? fromWhom = null, string? toWhom = null){
        DateOfOperation = dateOfOperation;
        OperationType = operationType;
        Amount = amount;
        ToWhom = toWhom;
        FromWhom = fromWhom;
    }
}