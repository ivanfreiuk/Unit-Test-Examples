using BooksApp.Interfaces;
using BooksApp.Models;
using BooksApp.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BooksApp.UnitTests
{
    [TestFixture]
    public class BookServiceTests
    {
        public List<Book> CreateBooks()
        {
           return new List<Book>
            {
                new Book
                {
                    Id=1,
                    Name = "AAA",
                    FullNameAuthor = "BBB",
                    Price = 200
                },
                new Book
                {
                    Id=2,
                    Name = "YYY",
                    FullNameAuthor = "ZZZ",
                    Price = 250
                },
                new Book
                {
                    Id=3,
                    Name = "OOO",
                    FullNameAuthor = "CCC",
                    Price = 130
                }
            };
        }

        [Test]
        public void AddBook_GoodBook_InvokesMethod()
        {
            // Arrange
            var mockBookRepository = new Mock<IRepository<Book>>();
            mockBookRepository.Setup(i => i.Add(It.IsAny<Book>()));
            var bookSvc = new BookService(mockBookRepository.Object);
            // Act
            var book = new Book { Id = 4, Name = "FFF", FullNameAuthor = "NNN", Price = 300 };
            bookSvc.AddBooks(book);
            // Assert
            mockBookRepository.Verify(i => i.Add(book), Times.Once);
        }

        [Test]
        public void GetBook_BookId_ReturnsBook()
        {
            var books = CreateBooks();
            var stubBookRepository = new Mock<IRepository<Book>>();
            var expectedBook = new Book
            {
                Id = 2,
                Name = "YYY",
                FullNameAuthor = "ZZZ",
                Price = 250
            };
            stubBookRepository.Setup(i => i.Get(It.IsAny<int>())).Returns(expectedBook);
            var bookSvc = new BookService(stubBookRepository.Object);

            var actualBook = bookSvc.GetBook(2);
                        
            Assert.AreEqual(expectedBook.Name, actualBook.Name);
        }

        [Test]
        public void GetAllBooks_WhenCalled_ReturnsAllBooks()
        {
            var books = CreateBooks();
            var stubBookRepository = new Mock<IRepository<Book>>();
            stubBookRepository.Setup(i => i.GetAll()).Returns(books);
            var bookSvc = new BookService(stubBookRepository.Object);

            var actualBooks = bookSvc.GetAllBooks().ToList();

            Assert.AreEqual(books.Count, actualBooks.Count);            
        }

        [Test]
        public void EditBook_BadBook_ThrowsException()
        {
            // Arrage
            var mockBookRepository = new Mock<IRepository<Book>>();
            mockBookRepository.Setup(i => i.Get(It.IsAny<int>())).Returns(default(Book));
            var bookSvc = new BookService(mockBookRepository.Object);
            // Act          
            
            // Assert
            var ex = Assert.Catch<Exception>(()=> bookSvc.EditBook(new Book()));
            StringAssert.Contains("Not found element!", ex.Message);
        }

        [Test]
        public void RemoveBook_BadBook_ThrowsException()
        {
            // Arrage
            var mockBookRepository = new Mock<IRepository<Book>>();
            mockBookRepository.Setup(i => i.Get(It.IsAny<int>())).Returns(default(Book));
            var bookSvc = new BookService(mockBookRepository.Object);
            // Act          

            // Assert
            var ex = Assert.Catch<Exception>(() => bookSvc.Remove(new Book()));
            StringAssert.Contains("Not found element!", ex.Message);
        }

        [Test]
        public void GetBooksBetweenBounds_GoodBounds_ReturnsBooks()
        {
            var books = CreateBooks();
            var stubBookRepository = new Mock<IRepository<Book>>();
            stubBookRepository.Setup(i => i.GetAll()).Returns(books);
            var bookSvc = new BookService(stubBookRepository.Object);
                        
            var actualBooks = bookSvc.GetBooksBetweenBounds(100,210).ToList();
            
            Assert.AreEqual(2, actualBooks.Count);
        }

    }
}
