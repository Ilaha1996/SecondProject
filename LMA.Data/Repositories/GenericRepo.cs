using LMA.Core.Entities;
using LMA.Core.Repositories;
using LMA.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMA.Data.Repositories;

public class GenericRepo<T> : IGenericRepo<T> where T : Base, new()
{
    private LMADBContext _dbContext;

    public GenericRepo()
    {
         _dbContext = new LMADBContext();
    }
    public async Task<int> CommitAsync()
    {
      return await _dbContext.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public IQueryable<T> GetAll()
    {
        return _dbContext.Set<T>();
    }

    public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (includes != null && includes.Length > 0) 
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }         
                    
        }
        if (expression is not null) 
        {
            query = query.Where(expression);
        }
        return query;
    }

    public async Task<T?> GetAsyncById(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);

    }

    public async Task<T?> GetWhere(Expression<Func<T, bool>> expression)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        if (expression is not null)
        {
            query = query.Where(expression);
        }
        return await query.FirstOrDefaultAsync();
    }
    public async Task Insert(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }
}
