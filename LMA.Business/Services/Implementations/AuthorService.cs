using LMA.Business.Services.Interfaces;
using LMA.Core.Entities;
using LMA.Core.Repositories;
using LMA.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMA.Business.Services.Implementations;

public class AuthorService : IAuthorService
{
    private IAuthorRepo _authorRepo;
    public AuthorService()
    {
        _authorRepo = new AuthorRepo();
    }
    public async Task CreateAsync(Author author)
    {
        if (author == null)
        {
            throw new NullReferenceException("Author cannot be null.");
        }
        await _authorRepo.Insert(author);
        await _authorRepo.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var data = await _authorRepo.GetAsyncById(id);
        if (data == null)
        {
            throw new NullReferenceException("Data is not found.");
        }

        _authorRepo.Delete(data);
        await _authorRepo.CommitAsync();
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await _authorRepo.GetAll().ToListAsync();
    }

    public async Task<Author> GetById(int id)
    {
        var data = await _authorRepo.GetAsyncById(id);

        if (data == null)
        {
            throw new NullReferenceException("Author with this Id was not found.");
        }

        return data;
    }

    public async Task UpdateAsync(Author author)
    {
        var existdata = await _authorRepo.GetAsyncById(author.Id);
        if (existdata == null)
        {
            throw new NullReferenceException("Author with this Id was not found.");
        }

        existdata.Name = author.Name;
              
        await _authorRepo.CommitAsync();
    }
}
