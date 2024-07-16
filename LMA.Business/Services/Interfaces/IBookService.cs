using LMA.Core.Entities;
using System.Linq.Expressions;
namespace LMA.Business.Services.Interfaces;

public interface IBookService
{
    Task<Book> GetById(int id);
    Task DeleteAsync(int id);
    Task<List<Book>> GetAllAsync();
    Task CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task<List<Book>> FilterBooksByTitleAsync(string title);
    Task<List<Book>> FilterBooksByAuthor(string authorName);
    Task<List<Book>> GetAllAsync(bool filterByAvailability);
    Task ChangeAvailableStatus (Book book);
    Task IncreaseBorrowedTimes(Book book);
    Task<List<Book>> FindAndDisplayMostBorrowedBooks();

}
