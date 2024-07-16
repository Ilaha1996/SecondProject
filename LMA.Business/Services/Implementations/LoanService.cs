using LMA.Business.Services.Interfaces;
using LMA.Core.Entities;
using LMA.Core.Repositories;
using LMA.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMA.Business.Services.Implementations;

public class LoanService : ILoanService
{
    private ILoanRepo _loanRepo;

    public LoanService()
    {
        _loanRepo = new LoanRepo();
    }

    public async Task CreateAsync(Loan loan)
    {
        if (loan == null)
        {
            throw new NullReferenceException("Loan cannot be null.");
        }
        await _loanRepo.Insert(loan);
        await _loanRepo.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var data = await _loanRepo.GetAsyncById(id);
        if (data == null)
        {
            throw new NullReferenceException("Data is not found.");
        }

        _loanRepo.Delete(data);
        await _loanRepo.CommitAsync();
    }

    public async Task<List<Loan>> GetAllAsync()
    {
        return await _loanRepo.GetAll().ToListAsync();
    }

    public async Task<Loan> GetById(int id)
    {
        var data = await _loanRepo.GetAsyncById(id);

        if (data == null)
        {
            throw new NullReferenceException("Loan with this Id was not found.");
        }

        return data;
    }

    public async Task UpdateAsync(Loan loan)
    {
        var existdata = await _loanRepo.GetAsyncById(loan.Id);
        if (existdata == null)
        {
            throw new NullReferenceException("Loan with this Id was not found.");
        }

        existdata.BorrowerId = loan.BorrowerId;
        existdata.LoanDate = DateTime.UtcNow.AddHours(4);
        existdata.MustReturnDate = loan.MustReturnDate;
        existdata.ReturnDate = loan.ReturnDate;

        await _loanRepo.CommitAsync();
    }



    public async Task<List<Loan>> GetAllByBorrowerId(int borrowerId)
    {
        var query = await _loanRepo.GetAllWhere(loan => loan.BorrowerId == borrowerId, null).ToListAsync();

        if (query == null || !query.Any())
        {
            throw new NullReferenceException("Loans with this BorrowerId were not found.");
        }

        return query;
    }      
}

