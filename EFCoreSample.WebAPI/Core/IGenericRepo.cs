namespace EFCoreSample.WebAPI.Core;

public interface IGenericRepo<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<bool> Add(T obj);
    Task<bool> Delete(T obj);
    Task <bool> Update(T obj);
}