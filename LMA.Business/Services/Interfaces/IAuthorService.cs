using LMA.Core.Entities;

namespace LMA.Business.Services.Interfaces;

public interface IAuthorService
{
    Task<Author> GetById(int id);
    Task DeleteAsync(int id);
    Task<List<Author>> GetAllAsync();
    Task CreateAsync(Author author);
    Task UpdateAsync(Author author);

}
