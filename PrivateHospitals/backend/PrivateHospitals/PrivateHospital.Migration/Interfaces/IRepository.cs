namespace PrivateHospital.Migration.Dto.Interfaces;

public interface IRepository<T> where T: class
{
    Task AddAsync(T entity);
}