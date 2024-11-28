namespace PrivateHospital.Migration.Dto.Interfaces;

public interface IRepository<T> where T: class
{
    Task<T> GetByExternalId(string externalId);
    Task AddAsync(T entity);
}