using Learning_Razor_Pages.Model;

namespace Learning_Razor_Pages.Services.UserAccess;

public interface IUserAccess : IDataBaseAccess<User>{
    public (bool isEmailExists, bool isPhoneExists) IsExists(string email, string phoneNumber);
    public bool Insert(User user);
    public bool Delete(uint id);
    public User? Update(User user);
    public User? Retrieve(string email);
}