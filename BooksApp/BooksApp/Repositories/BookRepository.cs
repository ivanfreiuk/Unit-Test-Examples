using BooksApp.Interfaces;
using BooksApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace BooksApp.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly List<Book> _books;

        public BookRepository()
        {
            _books = new List<Book>();
        }

        public void Add(Book entity)
        {
            entity.Id = _books.Any() ? _books.Max(i => i.Id) + 1 : 1;
            _books.Add(entity);
        }

        public Book Get(int id)
        {
            return _books.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public void Remove(Book entity)
        {
            _books.Remove(entity);
        }

        public void Update(Book entity)
        {
            var index = _books.FindIndex(item => item.Id == entity.Id);
            _books[index] = entity;
        }
    }
}
