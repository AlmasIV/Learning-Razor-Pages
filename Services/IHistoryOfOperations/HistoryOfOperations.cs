using Microsoft.Data.SqlClient;
using System.Data;

using Learning_Razor_Pages.Model.HistoryOfBank;
using Learning_Razor_Pages.Model.DefaultConnectionString;

namespace Learning_Razor_Pages.Services.HistoryOfOperations;

class HistoryOfOperations : IHistoryOfOperations
{
    private readonly string _connectionString;
    public HistoryOfOperations(DefaultConnectionString databaseAccess){
        _connectionString = databaseAccess.Builder.ToString();
    }
    public List<HistoryOfBank> GetHistoryOfBank(string userEmail)
    {
        List<HistoryOfBank> result = new List<HistoryOfBank>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlCommand command = new SqlCommand("GetHistoryOfBank", connection)){
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@_Email", userEmail);

                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while(dataReader.Read()){
                    result.Add(new HistoryOfBank(dataReader.GetDateTime(0), dataReader.GetString(1), dataReader.GetDecimal(2), dataReader.GetValue(3).ToString(), dataReader.GetValue(4).ToString()));
                }

                dataReader.Close();
            }
        }
        return result;
    }

    public void InsertHistoryOfBank(string userEmail, string operationType, decimal amount, string? toWhom = null)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlCommand command = new SqlCommand("InsertHistoryOfBank", connection)){
                command.CommandType = CommandType.StoredProcedure;  

                command.Parameters.AddWithValue("@_Email", userEmail);
                command.Parameters.AddWithValue("@_OperationType", operationType);
                command.Parameters.AddWithValue("@_Amount", amount);
                
                if(toWhom is not null){
                    command.Parameters.AddWithValue("@_FromWhom", userEmail);
                    command.Parameters.AddWithValue("@_toWhom", toWhom);
                }

                connection.Open();
                
                command.ExecuteNonQuery();

                if(toWhom is not null){
                    command.Parameters["@_Email"].Value = toWhom;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}