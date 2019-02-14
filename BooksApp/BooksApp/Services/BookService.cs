using System;
using System.Collections.Generic;
using System.Linq;
using BooksApp.Interfaces;
using BooksApp.Models;

namespace BooksApp.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        
        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void AddBooks(Book book)
        {
            _bookRepository.Add(book);
        }

        public void EditBook(Book book)
        {
            var oldBook = _bookRepository.Get(book.Id);

            if(oldBook == null)
            {
                throw new Exception("Not found element!");
            }

            _bookRepository.Update(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book GetBook(int id)
        {
            return _bookRepository.Get(id);
        }

        public void Remove(Book book)
        {
            var oldBook = _bookRepository.Get(book.Id);

            if (oldBook == null)
            {
                throw new Exception("Not found element!");
            }

            _bookRepository.Remove(book);
        }

        public Book SearchBooksByName(string name)
        {
            return _bookRepository.GetAll().FirstOrDefault(i=>i.Name == name);
        }

        public IEnumerable<Book> GetBooksBetweenBounds(decimal lowerBound, decimal upperBound)
        {
            if(lowerBound < 0 || upperBound < 0 )
            {
                throw new ArgumentException("Price must be greater then 0.");
            }

            if (lowerBound > upperBound)
            {
                throw new ArgumentException("Invalid arguments.");
            }

            return _bookRepository.GetAll().Select(i => i).Where(i => i.Price >= lowerBound && i.Price <= upperBound);
        }

    }
}
