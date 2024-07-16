using LMA.Business.Services.Interfaces;
using LMA.Core.Entities;
using LMA.Core.Repositories;
using LMA.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Expressions;

namespace LMA.Business.Services.Implementations;

public class BookService : IBookService
{
    private IBookRepo _bookRepo;
    public BookService()
    {
        _bookRepo = new BookRepo();
    }

    public async Task CreateAsync(Book book)
    {
        if (book == null)
        {
            throw new NullReferenceException("Book cannot be null.");
        }
        await _bookRepo.Insert(book);
        await _bookRepo.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var data = await _bookRepo.GetAsyncById(id);
        if (data == null)
        {
            throw new NullReferenceException("Data is not found.");
        }

        _bookRepo.Delete(data);
        await _bookRepo.CommitAsync();
    }


    public async Task<List<Book>> GetAllAsync()
    {
        return await _bookRepo.GetAll().ToListAsync();
    }


    public async Task<Book> GetById(int id)
    {
        var data = await _bookRepo.GetAsyncById(id);

        if (data == null)
        {
            throw new NullReferenceException("Book with this Id was not found.");
        }

        return data;
    }

    public async Task UpdateAsync(Book book)
    {
        var existdata = await _bookRepo.GetAsyncById(book.Id);
        if (existdata == null)
        {
            throw new NullReferenceException("Borrower with this Id was not found.");
        }

        existdata.Title = book.Title;
        existdata.Description = book.Description;
        existdata.PublishedYear = book.PublishedYear;
        existdata.BorrowerId = book.BorrowerId;
        existdata.IsAvailable = book.IsAvailable;

        await _bookRepo.CommitAsync();
    }

    public async Task<List<Book>> FilterBooksByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new NullReferenceException("Title cannot be null or whitespace.");
        }

        return await _bookRepo.GetAllWhere(book => book.Title.ToLower() == title.ToLower()).ToListAsync();
    }
    public async Task<List<Book>> FilterBooksByAuthor(string authorName)
    {
        if (string.IsNullOrWhiteSpace(authorName))
        {
            throw new NullReferenceException("Author name cannot be null or whitespace.");
        }

        return await _bookRepo.GetAllWhere(b => b.BookAuthors.Any(ba => ba.Author.Name.ToLower() == authorName.ToLower()), "BookAuthors.Author").ToListAsync();
    }

    public async Task<List<Book>> GetAllAsync(bool filterByAvailability)
    {
        if (filterByAvailability)
        {
            return await _bookRepo.GetAllWhere(book => book.IsAvailable).ToListAsync();
        }

        return await _bookRepo.GetAll().ToListAsync();
    }

    public async Task ChangeAvailableStatus(Book book)
    {
        var existingBook = await _bookRepo.GetAsyncById(book.Id);
        if (existingBook == null)
        {
            throw new NullReferenceException($"Book with ID {book.Id} was not found.");
        }
        existingBook.IsAvailable =! existingBook.IsAvailable;
        await _bookRepo.CommitAsync();
                     
    }

    public async Task IncreaseBorrowedTimes(Book book)
    {
        var existingBook = await _bookRepo.GetAsyncById(book.Id);
        if (existingBook == null)
        {
            throw new NullReferenceException($"Book with ID {book.Id} was not found.");
        }

        if (existingBook.IsAvailable == false)
        {
            existingBook.BorrowedTimes++;
            await _bookRepo.CommitAsync(); 
        }
    }

    public async Task<List<Book>> FindAndDisplayMostBorrowedBooks()
    {        
        var maxBorrowedTimes = await _bookRepo.GetAllWhere(b => b.BorrowedTimes.HasValue).MaxAsync(b => b.BorrowedTimes);

        var mostBorrowedBooks = await _bookRepo.GetAllWhere(b => b.BorrowedTimes == maxBorrowedTimes).ToListAsync();
        
        foreach (var book in mostBorrowedBooks)
        {
            Console.WriteLine($"Title: {book.Title}, BorrowedTimes: {book.BorrowedTimes}");
        }

        return mostBorrowedBooks;
    }
     
    public async Task SaveChanges() 
    {
        await _bookRepo.CommitAsync();    
    }



}



