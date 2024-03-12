using Microsoft.AspNetCore.Mvc;
using Assignment3.Models;
using Assignment3.Repositories;

namespace Assignment3.Controllers
{
    
    public class ReaderController : Controller
    {
        public ReaderRepository _repository;

        public ReaderController()
        {
            _repository = new ReaderRepository();
        }
        // GET: /Reader
        [HttpGet]
        [Route("/Reader")]
        public IActionResult GetAllReaders()
        {
                var reader = _repository.GetAll();
                return View(reader); // Return the list as a View
        }

        // GET: /Reader/{id}
        [HttpGet]
        [Route("/Reader/Id")]
        public IActionResult GetReaderById(string id)
        {
            int rId = int.Parse(id);
            var reader = _repository.GetById(rId);
            if (reader != null)
            {
                //return View(reader);
                return View(new List<ReaderModel> { reader });// Return the specific reader as a View
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/Reader/Add")]
        public IActionResult AddReader()
        {
            return View();
        }

        [HttpPost]
        [Route("/Reader/Add")]
        public IActionResult AddReader(ReaderModel reader)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(reader);
                return RedirectToAction("GetAllReaders"); // Redirect to the list view
            }
            return View(reader);
        }

        [HttpGet]
        [Route("/Reader/Update/{id}")]
        public IActionResult UpdateReader(int id)
        {
            var reader = _repository.GetById(id);
            return View(reader);
        }

        [HttpPost]
        [Route("/Reader/Update/{id}")]
        public IActionResult UpdateReader(ReaderModel reader)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(reader.ReaderId, reader);
                return RedirectToAction("GetAllReaders");
            }
            return View(reader);
        }

        // DELETE: /Reader/{id}
        [HttpPost]
        [Route("/Reader/Delete/{id}")]
        public IActionResult DeleteReader(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetAllReaders");
        }
        //// GET: /Reader/RegisteredAfter/{date} This is the format 2021-01-01 for the date yyyy-mm-dd
        //[HttpGet("RegisteredAfter/{date}")]
        //public IActionResult GetReadersRegisteredAfter(DateTime date)
        //{
        //    return Ok($"Readers registered after {date.ToShortDateString()} (yyyy-mm-dd) returned.");
        //}
        [HttpGet]
        [Route("/Reader/ZipCode")]
        public IActionResult GetReaderByZipCode(string zipcode)
        {
            int zip = int.Parse(zipcode);
            var readers = _repository.GetByZipCode(zip);
            if (readers.Any())
            {
                return View(readers); // Return books of a specific genre as a View
            }
            return NotFound(); // Or return a NotFound result
        }
    }
}

//public BookRepository _repository;

//public BookController()
//{
//    _repository = new BookRepository();
//}

//// GET: /Book
//[HttpGet]
//[Route("/Book")]
//public IActionResult GetAllBooks()
//{
//    var books = _repository.GetAll();
//    return View(books); // Return the list as a View
//}

//// GET: /Book/{id}
//[HttpGet]
//[Route("/Book/{id}")]
//public IActionResult GetBookById(int id)
//{
//    var book = _repository.GetById(id);
//    if (book != null)
//    {
//        return View(book); // Return the specific book as a View
//    }
//    return NotFound(); // Or return a NotFound result
//}

//[HttpGet]
//[Route("/Book/Add")]
//public IActionResult AddBook()
//{
//    return View();
//}

//// POST: /Book
//[HttpPost]
//[Route("/Book/Add")]
//public IActionResult AddBook(BookModel book)
//{
//    if (ModelState.IsValid)
//    {
//        _repository.Add(book);
//        return RedirectToAction("GetAllBooks"); // Redirect to the list view
//    }
//    return View(book);

//}

//[HttpGet]
//[Route("/Book/Update/{id}")]
//public IActionResult UpdateBook(int id)
//{
//    var book = _repository.GetById(id);
//    return View(book);
//}

//// PUT: /Book/{id}
//[HttpPost]
//[Route("/Book/Update/{id}")]
//public IActionResult UpdateBook(BookModel book)
//{
//    if (ModelState.IsValid)
//    {
//        _repository.Update(book.BookId, book);
//        return RedirectToAction("GetAllBooks");
//    }
//    return View(book);
//}

//// POST: /Book/Delete/{id}
//[HttpPost]
//[Route("/Book/Delete/{id}")]
//public IActionResult DeleteBook(int id)
//{
//    _repository.Delete(id);
//    return RedirectToAction("GetAllBooks"); // Redirect to the list view
//}


//[HttpGet]
//[Route("/Book/Genre")]
//public IActionResult GetBooksByGenre(string genre)
//{
//    var books = _repository.GetByGenre(genre);
//    if (books.Any())
//    {
//        return View(books); // Return books of a specific genre as a View
//    }
//    return NotFound(); // Or return a NotFound result
//}
