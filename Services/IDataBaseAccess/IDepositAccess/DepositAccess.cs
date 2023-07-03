using Microsoft.Data.SqlClient;
using System.Data;

using Learning_Razor_Pages.Model.DefaultConnectionString;
using Learning_Razor_Pages.Services.HistoryOfOperations;

namespace Learning_Razor_Pages.Services.DepositAccess;

public class DepositAccess : IDepositAccess
{
    private readonly string _connectionString;
    private readonly IHistoryOfOperations historyOfOperationsService;
    public DepositAccess(DefaultConnectionString databaseAccess, IHistoryOfOperations historyOfOperations){
        _connectionString = databaseAccess.Builder.ToString();
        historyOfOperationsService = historyOfOperations;
    }
    public decimal GetBalance(string userEmail)
    {
        decimal result = 0;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlCommand command = new SqlCommand("GetBalance", connection)){
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@_Email", userEmail);

                SqlParameter parameter = new SqlParameter("@_Balance", SqlDbType.Decimal);
                parameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(parameter);

                connection.Open();

                command.ExecuteNonQuery();

                result = (decimal)command.Parameters["@_Balance"].Value;
            }
        }
        return result;
    }

    public void Replenish(string userEmail, decimal amount, bool isTransfer = false)
    {   
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlCommand command = new SqlCommand("Replenish", connection)){
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@_Email", userEmail);
                command.Parameters.AddWithValue("@_Amount", amount);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        if(!isTransfer){
            historyOfOperationsService.InsertHistoryOfBank(userEmail, "Replenishment", amount);
        }
    }

    public void Withdraw(string userEmail, decimal amount, bool isTransfer = false)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlCommand command = new SqlCommand("Withdraw", connection)){
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@_Email", userEmail);
                command.Parameters.AddWithValue("@_Amount", amount);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        if(!isTransfer){
            historyOfOperationsService.InsertHistoryOfBank(userEmail, "Withdrawal", amount);
        }
    }
    public void Transfer(string userEmail, string toWhom, decimal amount){
        Withdraw(userEmail, amount, true);
        Replenish(toWhom, amount, true);
        historyOfOperationsService.InsertHistoryOfBank(userEmail, "Transaction", amount, toWhom);
    }
}