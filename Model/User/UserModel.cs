using System.ComponentModel.DataAnnotations;

namespace Learning_Razor_Pages.Model;

public class User {

    [Required, MinLength(2), MaxLength(20)]
    public string? Name { get; set; }
    [Required, MinLength(2), MaxLength(20)]
    public string? Surname { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
    [Required, Phone, MinLength(3), MaxLength(15)]
    public string? PhoneNumber { get; set; }
    [Required, Range(16, 100)]
    public byte? Age { get; set; }
    [Required, MinLength(8), MaxLength(32)]
    public string? Password { get; set; }
    
    public User(){ }

    public User(string? Name, string? Surname, string? Email, string? PhoneNumber, byte? Age, string? Password){
        this.Name = Name;
        this.Surname = Surname;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.Age = Age;
        this.Password = Password;
    }
}