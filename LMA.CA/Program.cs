using LMA.Business.Services.Implementations;
using LMA.Business.Services.Interfaces;
using LMA.Core.Entities;
using LMA.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace LMA.CA
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            IAuthorService authorService = new AuthorService();
            IBookService bookService = new BookService();
            IBorrowerService borrowerService = new BorrowerService();
            ILoanService loanService = new LoanService();
            bool validInput = false;

            while (!validInput)
            {
            Main_Menu:
                while (true)
                {


                    Console.WriteLine("\nMain menu");
                    Console.WriteLine("1 - Author Actions");
                    Console.WriteLine("2 - Book Actions");
                    Console.WriteLine("3 - Borrower Actions");
                    Console.WriteLine("4 - Borrow Book");
                    Console.WriteLine("5 - Return Book");
                    Console.WriteLine("6 - The most borrowed book");
                    Console.WriteLine("7 - List of Borrowers who delayed returning the book");
                    Console.WriteLine("8 - List of books borrowed by each borrower");
                    Console.WriteLine("9 - Filter books by title");
                    Console.WriteLine("10 - Filter books by author");
                    Console.WriteLine("0 - Quit");
                    string mainOption = Console.ReadLine();

                    switch (mainOption)
                    {
                        case "1":
                            while (true)
                            {
                                Console.WriteLine("\nAuthor Actions");
                                Console.WriteLine("1 - List of Authors");
                                Console.WriteLine("2 - Create Author");
                                Console.WriteLine("3 - Edit Author");
                                Console.WriteLine("4 - Delete Author");
                                Console.WriteLine("0 - Exit");
                                Console.Write("Select an option (0, 1, 2, 3, 4 or 0): ");
                                string option = Console.ReadLine();

                                switch (option)
                                {
                                    case "1":
                                        var authors = await authorService.GetAllAsync();
                                        foreach (var author in authors)
                                        {
                                            Console.WriteLine($"Author: {author.Name}");
                                        }
                                        break;

                                    case "2":
                                        Console.WriteLine("Enter the name of author:");
                                        string authorName = Console.ReadLine();
                                        await authorService.CreateAsync(new Author()
                                        {
                                            Name = authorName
                                        });
                                        Console.WriteLine("Author created successfully.");
                                        break;

                                    case "3":
                                        Console.WriteLine("Enter the ID of the author to edit:");
                                        if (int.TryParse(Console.ReadLine(), out int editId))
                                        {
                                            var authorToEdit = await authorService.GetById(editId);
                                            if (authorToEdit != null)
                                            {
                                                Console.WriteLine("Enter the new name of the author:");
                                                authorToEdit.Name = Console.ReadLine();
                                                await authorService.UpdateAsync(authorToEdit);
                                                Console.WriteLine("Author updated successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Author not found.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid ID.");
                                        }
                                        break;

                                    case "4":
                                        Console.WriteLine("Enter the ID of the author to delete:");
                                        if (int.TryParse(Console.ReadLine(), out int Id))
                                        {
                                            var authorToDelete = await authorService.GetById(Id);
                                            if (authorToDelete != null)
                                            {
                                                await authorService.DeleteAsync(Id);
                                                Console.WriteLine("Author deleted successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Author not found.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid ID.");
                                        }
                                        break;

                                    case "0":
                                        goto Main_Menu;

                                    default:
                                        Console.WriteLine("Invalid option. Please try again.");
                                        break;
                                }
                            }
                            break;

                        case "2":
                            while (true)
                            {
                                Console.WriteLine("\nBook Actions");
                                Console.WriteLine("1 - List of Books");
                                Console.WriteLine("2 - Create Books");
                                Console.WriteLine("3 - Edit Books");
                                Console.WriteLine("4 - Delete Books");
                                Console.WriteLine("0 - Exit");
                                Console.Write("Select an option (0, 1, 2, 3, 4 or 0): ");
                                string option = Console.ReadLine();

                                switch (option)
                                {
                                    case "1":
                                        var books = await bookService.GetAllAsync();
                                        foreach (var book in books)
                                        {
                                            Console.WriteLine($"Book: {book.Title} - IsAvailable: {book.IsAvailable}");
                                        }
                                        break;

                                    case "2":
                                        Console.WriteLine("Enter the title of book:");
                                        string bookTitle = Console.ReadLine();
                                        Console.WriteLine("Enter the description of book:");
                                        string bookDescription = Console.ReadLine();
                                        Console.WriteLine("Enter the published year of book:");
                                        string input = Console.ReadLine();
                                        int bookPublishedYear;
                                        if (int.TryParse(input, out bookPublishedYear))
                                        {
                                            await bookService.CreateAsync(new Book
                                            {
                                                Title = bookTitle,
                                                Description = bookDescription,
                                                PublishedYear = bookPublishedYear,
                                                IsAvailable = true

                                            });
                                            Console.WriteLine("Book created successfully.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input. Please enter a valid year.");
                                        }
                                        break;

                                    case "3":
                                        Console.WriteLine("Enter the ID of the book to edit:");
                                        if (int.TryParse(Console.ReadLine(), out int editId))
                                        {
                                            var bookToEdit = await bookService.GetById(editId);
                                            if (bookToEdit != null)
                                            {
                                                Console.WriteLine($"Current Title: {bookToEdit.Title}");
                                                Console.WriteLine("Enter the new title of the book:");
                                                bookToEdit.Title = Console.ReadLine();

                                                Console.WriteLine($"Current Description: {bookToEdit.Description}");
                                                Console.WriteLine("Enter the new description of the book:");
                                                bookToEdit.Description = Console.ReadLine();

                                                Console.WriteLine($"Current Published Year: {bookToEdit.PublishedYear}");
                                                Console.WriteLine("Enter the new published year of the book:");
                                                string inputYear = Console.ReadLine();

                                                if (int.TryParse(inputYear, out int newYear))
                                                {
                                                    bookToEdit.PublishedYear = newYear;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid year format.");
                                                }

                                                await bookService.UpdateAsync(bookToEdit);
                                                Console.WriteLine("Book updated successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Book not found.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid ID.");
                                        }
                                        break;

                                    case "4":
                                        Console.WriteLine("Enter the ID of the book to delete:");
                                        if (int.TryParse(Console.ReadLine(), out int Id))
                                        {
                                            var bookToDelete = await bookService.GetById(Id);
                                            if (bookToDelete != null)
                                            {
                                                await bookService.DeleteAsync(Id);
                                                Console.WriteLine("Book deleted successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Book not found.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid ID.");
                                        }
                                        break;

                                    case "0":
                                        goto Main_Menu;

                                    default:
                                        Console.WriteLine("Invalid option. Please try again.");
                                        break;
                                }
                            }
                            break;

                        case "3":
                            while (true)
                            {
                                Console.WriteLine("\nBorrower Actions");
                                Console.WriteLine("1 - List of Borrowers");
                                Console.WriteLine("2 - Create Borrower");
                                Console.WriteLine("3 - Edit Borrowers");
                                Console.WriteLine("4 - Delete Borrowers");
                                Console.WriteLine("0 - Exit");
                                Console.Write("Select an option (0, 1, 2, 3, 4 or 0): ");
                                string option = Console.ReadLine();

                                switch (option)
                                {
                                    case "1":
                                        var borrowers = await borrowerService.GetAllAsync();
                                        foreach (var borrower in borrowers)
                                        {
                                            Console.WriteLine($"Borrower: {borrower.Name}");
                                        }
                                        break;

                                    case "2":
                                        Console.WriteLine("Enter the name of borrower:");
                                        string borrowerName = Console.ReadLine();
                                        Console.WriteLine("Enter the email of borrower:");
                                        string borrowerEmail = Console.ReadLine();
                                        await borrowerService.CreateAsync(new Borrower
                                        {
                                            Name = borrowerName,
                                            Email = borrowerEmail
                                        });
                                        Console.WriteLine("Borrower created successfully.");
                                        break;

                                    case "3":
                                        Console.WriteLine("Enter the ID of the borrower to edit:");
                                        if (int.TryParse(Console.ReadLine(), out int editId))
                                        {
                                            var borrowerToEdit = await borrowerService.GetById(editId);
                                            if (borrowerToEdit != null)
                                            {
                                                Console.WriteLine("Enter the new name of the borrower:");
                                                borrowerToEdit.Name = Console.ReadLine();
                                                Console.WriteLine("Enter the new email of the borrower:");
                                                borrowerToEdit.Email = Console.ReadLine();
                                                await borrowerService.UpdateAsync(borrowerToEdit);
                                                Console.WriteLine("Borrower updated successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Borrower not found.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid ID.");
                                        }
                                        break;

                                    case "4":
                                        Console.WriteLine("Enter the ID of the borrower to delete:");
                                        if (int.TryParse(Console.ReadLine(), out int Id))
                                        {
                                            var borrowerToDelete = await borrowerService.GetById(Id);
                                            if (borrowerToDelete != null)
                                            {
                                                await borrowerService.DeleteAsync(Id);
                                                Console.WriteLine("Borrower deleted successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Borrower not found.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid ID.");
                                        }
                                        break;

                                    case "0":
                                        goto Main_Menu;

                                    default:
                                        Console.WriteLine("Invalid option. Please try again.");
                                        break;
                                }
                            }
                            break;

                        case "4":
                            Console.WriteLine("These books are available for taken you can select");
                            var availableBooks = await bookService.GetAllAsync(filterByAvailability: true);
                            foreach (var book in availableBooks)
                            {
                                Console.WriteLine($"Book: {book.Id} - {book.Title} - {book.IsAvailable}");
                            }

                            Console.WriteLine("Enter the ID of the book you want to borrow");
                            if (int.TryParse(Console.ReadLine(), out int BookId))
                            {
                                var bookToBorrow = await bookService.GetById(BookId);
                                if (bookToBorrow != null)
                                {

                                    Console.WriteLine("Add borrower from this list");
                                    var borrowers = await borrowerService.GetAllAsync();
                                    foreach (var item in borrowers)
                                    {
                                        Console.WriteLine($"Borrower: {item.Id} - {item.Name} ");
                                    }

                                    Console.WriteLine("Enter borrower ID");
                                    if (int.TryParse(Console.ReadLine(), out int BorrowerId))
                                    {
                                        var borrowerToTakeBook = await borrowerService.GetById(BorrowerId);
                                        if (borrowerToTakeBook != null)
                                        {
                                            await bookService.ChangeAvailableStatus(bookToBorrow);
                                        }
                                        bookToBorrow.BorrowerId = BorrowerId;
                                        //await bookService.UpdateAsync(bookToBorrow);

                                        bookToBorrow.BorrowedTimes = 0;
                                        bookToBorrow.BorrowedTimes++;
                                        await bookService.SaveChanges();

                                        Console.WriteLine("Book borrowed successfully.");

                                        await loanService.CreateAsync(new Loan
                                        {
                                            BorrowerId = BorrowerId,
                                            BookId = BookId,
                                            LoanDate = DateTime.Now,
                                            MustReturnDate = DateTime.Now.AddDays(15)

                                        });
                                    }
                                    else
                                    {
                                        Console.WriteLine("Book not found.");

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Borrower not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID.");
                            }
                            break;

                        case "5":
                            Console.WriteLine("Enter borrower ID");
                            if (int.TryParse(Console.ReadLine(), out int borrowerId))
                            {
                                var loans = await loanService.GetAllByBorrowerId(borrowerId);

                                if (loans == null || !loans.Any())
                                {
                                    Console.WriteLine("Loans with this Borrower ID were not found.");
                                    return;
                                }

                                foreach (var loan in loans)
                                {
                                    Console.WriteLine($"LoanId: {loan.Id} - BookId: {loan.BookId} - LoanDate: {loan.LoanDate} - MustReturnDate: {loan.MustReturnDate}");
                                }

                                Console.WriteLine("Select loan ID");
                                if (int.TryParse(Console.ReadLine(), out int loanId))
                                {
                                    var loan = await loanService.GetById(loanId);

                                    if (loan == null)
                                    {
                                        Console.WriteLine("Loan not found.");
                                        return;
                                    }

                                    loan.ReturnDate = DateTime.Now;
                                    await loanService.UpdateAsync(loan);
                                    Console.WriteLine("Book returned successfully!");

                                    Console.WriteLine("Select Book Id");
                                    if (int.TryParse(Console.ReadLine(), out int bookId))
                                    {
                                        var loanedBook = await bookService.GetById(bookId);

                                        if (loanedBook == null)
                                        {
                                            Console.WriteLine("Book not found.");
                                            return;
                                        }

                                        loanedBook.IsAvailable = true;
                                        await bookService.ChangeAvailableStatus(loanedBook);
                                        Console.WriteLine("Book is available now!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid book ID.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid loan ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Borrower not found.");
                            }

                            break;

                        case "6":
                            Console.WriteLine("You can see the most borrowed book(s) below:");
                            await bookService.FindAndDisplayMostBorrowedBooks();

                            break;
                        case "7":
                            Console.WriteLine("Borrowers who return late the books are the following:");
                            var allBorrowers = await borrowerService.GetAllAsync();
                            var lateReturners = borrowerService.GetLateReturners(allBorrowers);
                            if (lateReturners is null)
                            {
                                foreach (var bor in lateReturners)
                                {
                                    Console.WriteLine(($"Name: {bor.Name}, LateReturner: {bor.LateReturner}"));
                                }
                            }
                            else
                            {
                                Console.WriteLine("There is no borrower who return the book late. They are clever :)!");

                            }

                            break;
                        case "8":
                            Console.WriteLine("There are borrower with their books");
                            LMADBContext lMADBContext = new LMADBContext();
                            var loannss = lMADBContext.Loans.Include(x => x.Book).Include(x => x.Borrower).ToList();
                            foreach (var item in loannss)
                            {
                                Console.WriteLine($"Borrower Info: {item.Borrower.Name} - {item.Book.Title}");
                            }



                            break;
                        case "9":
                            Console.WriteLine("Enter the title of book");
                            string title = Console.ReadLine();
                            var items = await bookService.FilterBooksByTitleAsync(title);
                            if (items.Any())
                            {
                                foreach (var book in items)
                                {
                                    Console.WriteLine($"Book: {book.Id} - {book.Title} - {book.PublishedYear}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Books not found with the given title.");
                            }
                            break;

                        case "10":
                            Console.WriteLine("Enter the author name:");
                            string name = Console.ReadLine();
                            var datas = await bookService.FilterBooksByAuthor(name);
                            if (datas.Any())
                            {
                                foreach (var book in datas)
                                {
                                    Console.WriteLine($"Book: {book.Id} - {book.Title}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Books not found with the given author.");
                            }
                            break;

                        case "0":
                            validInput = true;
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }


                }
            }
        }
    }
}
