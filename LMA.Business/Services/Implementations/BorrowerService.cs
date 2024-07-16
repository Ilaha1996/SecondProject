using LMA.Business.Services.Interfaces;
using LMA.Core.Entities;
using LMA.Core.Repositories;
using LMA.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMA.Business.Services.Implementations;

public class BorrowerService : IBorrowerService
{
    private IBorrowerRepo _borrowerRepo;
    public BorrowerService()
    {
        _borrowerRepo = new BorrowerRepo();
    }
    public async Task CreateAsync(Borrower borrower)
    {
        if (borrower == null) 
        {
            throw new NullReferenceException("Borrower cannot be null.");
        }
       await _borrowerRepo.Insert(borrower);
       await _borrowerRepo.CommitAsync();       
    }

    public async Task DeleteAsync(int id)
    {
        var data = await _borrowerRepo.GetAsyncById(id);
        if (data == null)
        {
            throw new NullReferenceException("Data is not found.");
        }

        _borrowerRepo.Delete(data);
        await _borrowerRepo.CommitAsync();
    }


    public async Task<List<Borrower>> GetAllAsync()
    {
        return await _borrowerRepo.GetAll().ToListAsync();
    }

    public async Task<Borrower> GetById(int id)
    {
       
        var data = await _borrowerRepo.GetAsyncById(id);

        if (data == null)
        {
            throw new NullReferenceException("Borrower with this Id was not found.");
        }

        return data;
    }


    public async Task UpdateAsync(Borrower borrower)
    {
        var existdata = await _borrowerRepo.GetAsyncById(borrower.Id);
        if (existdata == null)
        {
            throw new NullReferenceException("Borrower with this Id was not found.");
        }

        existdata.Name = borrower.Name;
        existdata.Email = borrower.Email;
        

        await _borrowerRepo.CommitAsync();
    }

    public List<Borrower> GetLateReturners(List<Borrower> allBorrowers, bool isLate = true)
    {
        return allBorrowers.Where(b => b.LateReturner == isLate).ToList();
    }

    public async Task<List<Borrower>> GetBorrowersWithBorrowedBooks()
    {
        var borrowersWithBooks = await _borrowerRepo.GetAll()
                    .Include(b => b.Loans)
                    .ThenInclude(l => l.Book)
                    .ToListAsync();

        return borrowersWithBooks;
    }

}

