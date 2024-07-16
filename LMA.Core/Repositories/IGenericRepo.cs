using LMA.Core.Entities;
using System.Linq.Expressions;

namespace LMA.Core.Repositories;

public interface IGenericRepo<T> where T : Base, new()
{
    Task Insert(T entity);
    Task<T?> GetAsyncById(int id);
    Task<T?> GetWhere(Expression<Func<T, bool>> expression);
    IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression, params string[] includes);
    IQueryable<T> GetAll();
    void Delete(T entity);
    Task<int> CommitAsync();
}
