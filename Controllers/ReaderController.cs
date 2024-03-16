using Microsoft.AspNetCore.Mvc;
using Assignment3.Models;
using Assignment3.Repositories;

namespace Assignment3.Controllers
{
    public class ReaderController : Controller
    {
        public ReaderRepository _repository;

        public ReaderController(ReaderRepository repository)
        {
            _repository = repository;
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
        //[HttpPost]
        //[Route("/Reader/Delete/{id}")]
        //public IActionResult DeleteReader(int id)
        //{
        //    _repository.Delete(id);
        //    return RedirectToAction("GetAllReaders");
        //}
        [HttpPost]
        [Route("/Reader/Delete/{id}")]
        public IActionResult DeleteReader(int id)
        {
            var reader = _repository.GetById(id);
            if (reader == null)
            {
                return NotFound(); // Or return a view that informs the book wasn't found.
            }

            if (reader.BorrowedBooks.Count > 0)
            {
                // Book is borrowed and can't be deleted. Store a warning message.
                TempData["Warning"] = "Cannot delete reader with borrowed books.";
                return RedirectToAction("GetAllReaders");
            }

            _repository.Delete(id);
            TempData["Success"] = "Reader deleted successfully.";
            return RedirectToAction("GetAllReaders");
        }

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

        [HttpGet]
        [Route("/Reader/{id}/BorrowedBooks")]
        public IActionResult GetBorrowedBooks(int id)
        {
            var borrowedBooks = _repository.GetBorrowedBooks(id);
            // Optionally, use AutoMapper or manual mapping to convert BorrowingModel instances to a ViewModel if needed
            return View(borrowedBooks);
        }

    }
}

