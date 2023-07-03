using Learning_Razor_Pages.Model;
using Learning_Razor_Pages.Services.UserAccess;
using Learning_Razor_Pages.Model.DefaultConnectionString;

using Microsoft.Data.SqlClient;
using System.Data;

namespace Learning_Razor_Pages;

public class UserAccess : IUserAccess{
    private readonly string _connectionString;
    public UserAccess(DefaultConnectionString databaseAccess){
        _connectionString = databaseAccess.Builder.ToString();
    }
    public (bool isEmailExists, bool isPhoneExists) IsExists(string email, string phoneNumber){
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlCommand command = new SqlCommand("CheckUserExistence", connection)){
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@_Email", email);
                command.Parameters.AddWithValue("@_PhoneNumber", phoneNumber);

                SqlParameter emailNum = new SqlParameter("@_EmailNum", SqlDbType.TinyInt);
                emailNum.Direction = ParameterDirection.Output;
                command.Parameters.Add(emailNum);

                SqlParameter phoneNumberNum = new SqlParameter("@_PhoneNumberNum", SqlDbType.TinyInt);
                phoneNumberNum.Direction = ParameterDirection.Output;
                command.Parameters.Add(phoneNumberNum);

                connection.Open();

                command.ExecuteNonQuery();

                byte emailOutNum = (byte)command.Parameters["@_EmailNum"].Value;
                byte phoneOutNum = (byte)command.Parameters["@_PhoneNumberNum"].Value;

                bool isEmailExists = emailOutNum > 0 ? true : false;
                bool isPhoneExists = phoneOutNum > 0 ? true : false;

                return (isEmailExists, isPhoneExists);
            }
        }
    }
    public bool Insert(User user){
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlCommand command = new SqlCommand("InsertUser", connection)){
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@_Name", user.Name);
                command.Parameters.AddWithValue("@_Surname", user.Surname);
                command.Parameters.AddWithValue("@_Email", user.Email);
                command.Parameters.AddWithValue("@_PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@_Age", user.Age);
                command.Parameters.AddWithValue("@_Password", user.Password);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        return true;
    }
    public bool Delete(uint id){

        return false;
    }
    public User? Update(User user){

       
        return null;
    }
    public User? Retrieve(string email){
        User? result = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            using(SqlCommand command = new SqlCommand()){

                connection.Open();

                command.Connection = connection;
                command.CommandText = "USE MainDatabase;";

                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM Users WHERE Email = @param;";
                command.Parameters.AddWithValue("@param", email);

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read()){
                    result = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetByte(5), reader.GetString(6));
                }
                
                reader.Close();
            }
        }
        return result;
    }
}