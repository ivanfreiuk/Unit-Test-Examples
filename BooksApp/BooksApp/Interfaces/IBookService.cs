using BooksApp.Models;
using System.Collections.Generic;

namespace BooksApp.Interfaces
{
    public interface IBookService
    {
        Book GetBook(int id);
        IEnumerable<Book> GetAllBooks();
        void AddBooks(Book book);
        void Remove(Book book);
        void EditBook(Book book);
    }
}
