using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using JuniorTestProject.Models;
using JuniorTestProject.Controllers;
using Microsoft.EntityFrameworkCore;

namespace JuniorTestProject.Tests
{
    [TestFixture]
    class BookControllerTest
    {
        Mock<BookContext> dbContext;

        [Test]
        public void ShouldGetAllBooks()
        {
            var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase("ApplicationDatabase").Options;
            var context = new BookContext(options);

            context.Books.AddRange(new Book { Id = 1, Title = "Айвенго", Author = "Вальтер Скотт" },
               new Book { Id = 2, Title = "Пригоди Тома Сойєра", Author = "Марк Твен" });
            context.SaveChanges();

            BookController controller = new BookController(dbContext.Object);

            var expected = new List<Book>
            {
                 new Book { Id = 1, Title = "Айвенго", Author = "Вальтер Скотт" },
                 new Book { Id = 2, Title = "Пригоди Тома Сойєра", Author = "Марк Твен" }
            } as IEnumerable<Book>;

            var result = controller.GetAllBooks() as IEnumerable<Book>;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldGetBookById()
        {
            var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase("ApplicationDatabase").Options;
            var context = new BookContext(options);

            BookController controller = new BookController(context);

            Book expected = new Book { Id = 1, Title = "Айвенго", Author = "Вальтер Скотт" };

            Book actual = controller.GetBookById(1);

            Assert.AreEqual(expected, actual);
        }
    }
}
