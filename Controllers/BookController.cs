using Microsoft.AspNetCore.Mvc;
using Assignment3.Models;
using Assignment3.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment3.Controllers
{
    public class BookController : Controller
    {
        public BookRepository _repository;

        public BookController()
        {
            _repository = new BookRepository();
        }

        // GET: /Book
        [HttpGet]
        [Route("/Book")]
        public IActionResult GetAllBooks()
        {
            var books = _repository.GetAll();
            return View(books); // Return the list as a View
        }

        [HttpGet]
        [Route("/Book/Id")]
        public IActionResult GetBookById(string id)
        {
            int bId = int.Parse(id);
            var book = _repository.GetById(bId);
            if (book != null)
            {
                return View(new List<BookModel> { book }); // Wrap the book in a list
            }
            return NotFound(); // Or return a NotFound result
        }


        [HttpGet]
        [Route("/Book/Add")]
        public IActionResult AddBook()
        {
            return View();
        }

        // POST: /Book
        [HttpPost]
        [Route("/Book/Add")]
        public IActionResult AddBook(BookModel book)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(book);
                return RedirectToAction("GetAllBooks"); // Redirect to the list view
            }
            return View(book);

        }

        [HttpGet]
        [Route("/Book/Update/{id}")]
        public IActionResult UpdateBook(int id)
        {
            var book = _repository.GetById(id);
            return View(book);
        }

        // PUT: /Book/{id}
        [HttpPost]
        [Route("/Book/Update/{id}")]
        public IActionResult UpdateBook(BookModel book)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(book.BookId, book);
                return RedirectToAction("GetAllBooks");
            }
            return View(book);
        }

        // POST: /Book/Delete/{id}
        [HttpPost]
        [Route("/Book/Delete/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _repository.GetById(id);
            if (book == null)
            {
                return NotFound(); // Or return a view that informs the book wasn't found.
            }

            if (book.IsAvailable == "No")
            {
                // Book is borrowed and can't be deleted. Store a warning message.
                TempData["Warning"] = "The book is currently borrowed and cannot be deleted.";
                return RedirectToAction("GetAllBooks");
            }

            _repository.Delete(id);
            TempData["Success"] = "Book deleted successfully.";
            return RedirectToAction("GetAllBooks");
        }


        [HttpGet]
        [Route("/Book/Genre")]
        public IActionResult GetBooksByGenre(string genre)
        {
            var books = _repository.GetByGenre(genre);
            if (books.Any())
            {
                return View(books); // Return books of a specific genre as a View
            }
            return NotFound(); // Or return a NotFound result
        }


    }
}
