namespace Learning_Razor_Pages.Services;

public interface ICookiesAuthentication {
    public Task SignInAsync(string emailAddress);
    public Task SignOutAsync();
}