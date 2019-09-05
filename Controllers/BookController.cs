using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JuniorTestProject.Models;

namespace JuniorTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookContext db;

        public BookController(BookContext context)
        {
            db = context;
        }

        /// <summary>
        ///     Get list of all books in database
        /// </summary>
        /// <returns>List of all books</returns>
        /// <response code="200">Returns all books</response>

        [HttpGet]
        public IEnumerable<Book> GetAllBooks()
        {
            return db.Books.ToList();
        }

        /// <summary>
        ///     Get one book by chosen id
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>Information about one book</returns>
        /// <response code="200">Returns book by id</response>
        /// <response code="400">If id is not a number</response>
        /// <response code="404">If book wasn't found by id</response> 

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            if (id.GetType() != typeof(int))
            {
                return BadRequest();
            }

            var book = db.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        /// <summary>
        ///     Add new book to database
        /// </summary>
        /// <param name="book"></param> 
        /// <returns>Created book</returns>
        /// <response code="201">Returns created book</response>
        /// <response code="400">If the book that you are trying to send is not valid</response>

        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] BookAddModel book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            var newBook = new Book { Title = book.Title, Author = book.Author };
            db.Add(newBook);
            db.SaveChanges();
            return Created("", newBook);
        }

        /// <summary>
        ///     Update existing book in database
        /// </summary>
        /// <param name="b"></param>
        /// <returns>Status code that will inform you that book was updated</returns>
        /// <response code="204">Returns the information about user  and token</response>
        /// <response code="400">If the id is not a number or if the book model is not valid</response>
        /// <response code="404">If book, that you want to update doesn't exist</response> 

        [HttpPut("{id}")]
        public ActionResult UpdateBookById([FromBody] Book b)
        {
            if (b == null)
            {
                return BadRequest();
            }

            var book = db.Books.FirstOrDefault(i => i.Id == b.Id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = b.Title;
            book.Author = b.Author;
            db.Books.Update(book);
            db.SaveChanges();

            return NoContent();
        }

        /// <summary>
        ///     Delete chosen book by id
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>Information that tells to you that book was deleted</returns>
        /// <response code="204">Book was deleted</response>
        /// <response code="400">If id is not a number</response>
        /// <response code="404">If book with this id wasn't found</response> 

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {

            if (id.GetType() != typeof(int))
            {
                return BadRequest();
            }

            var book = db.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            db.Books.Remove(book);
            db.SaveChanges();

            return NoContent();
        }
    }

}
