using BooksApp.Models;
using BooksApp.Repositories;
using BooksApp.Services;
using System;

namespace BooksApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookSvc = new BookService(new BookRepository());
            bookSvc.AddBooks(new Book { Name = "h"});
            bookSvc.AddBooks(new Book { Name = "hdi" });
            bookSvc.AddBooks(new Book { Name = "hssu" });
            foreach (var item in bookSvc.GetAllBooks())
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadLine();
        }
    }
}
