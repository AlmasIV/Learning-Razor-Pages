namespace Learning_Razor_Pages.Services;

public interface IDataBaseAccess<T> where T: class 
{
    public bool Insert(T obj);
    public bool Delete(uint id);
    public T? Update(T obj);
    public T? Retrieve(string email);
}