using Microsoft.AspNetCore.Mvc;
using Assignment3.Models;
//using Assignment3.Controllers;
using Assignment3.Repositories;

namespace Assignment3.Controllers
{
    
    public class BorrowingController : Controller
    {
        public BorrowingRepository _repository;

        //public BorrowingController()
        //{
        //    var bookRepository = new BookRepository(); // Create a BookRepository instance
        //    var readerRepository = new ReaderRepository(); // Create a ReaderRepository instance
        //    _repository = new BorrowingRepository(bookRepository, readerRepository);
        //}

        public BorrowingController(BorrowingRepository repository)
        {
            _repository = repository;
        }
        // GET: /Borrowing
        [HttpGet]
        [Route("/Borrowing")]
        public IActionResult GetAllBorrowings()
        {
            var borrowing = _repository.GetAll();
            return View(borrowing); // Return the list as a View
        }

        //// GET: /Book/{id}
        [HttpGet]
        [Route("/Borrowing/{id}")]
        public IActionResult GetBorrowingById(int id)
        {
            var borrowing = _repository.GetById(id);
            if (borrowing != null)
            {
                return View(borrowing); // Return the specific book as a View
            }
            return NotFound(); // Or return a NotFound result
        }

        [HttpGet]
        [Route("/Borrowing/Add")]
        public IActionResult AddBorrowing()
        {
            return View();
        }

       
        [HttpPost]
        [Route("/Borrowing/Add")]
        public IActionResult AddBorrowing(BorrowingModel borrowing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Add(borrowing);
                    return RedirectToAction("GetAllBorrowings"); // Redirect to the list view if the operation is successful
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message); // Add the exception message to ModelState
                }
            }
            return View(borrowing); // Return to the same view with the error message if ModelState is invalid or an exception occurs
        }




        [HttpGet]
        [Route("/Borrowing/Update/{id}")]
        public IActionResult UpdateBorrowing(int id)
        {
            var borrowing = _repository.GetById(id);
            return View(borrowing);
        }

        // PUT: /Book/{id}
        [HttpPost]
        [Route("/Borrowing/Update/{id}")]
        public IActionResult UpdateBook(BorrowingModel borrowing)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(borrowing.BorrowingId, borrowing);
                return RedirectToAction("GetAllBorrowings");
            }
            return View(borrowing);
        }

        // DELETE: /Borrowing/{id}
        [HttpPost]
        [Route("/Borrowing/Delete/{id}")]
        public IActionResult DeleteBorrowing(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetAllBorrowings"); // Redirect to the list view
        }
        // GET: /Borrowing/Overdue
       
        [HttpGet]
        [Route("/Borrowing/Overdue")]
        public IActionResult GetOverdueBorrowings()
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;

            // Get all the borrowings from the repository
            var borrowings = _repository.GetAll();

            // Filter the borrowings that are overdue
            var overdueBorrowings = borrowings.Where(b => (currentDate - b.BorrowDate).TotalDays > 21);

            // Return the overdue borrowings as a response
            return View(overdueBorrowings);
        }

        [HttpGet]
        [Route("/Borrowing/ByReader")]
        public IActionResult GetBorrowingsByReader(string id)
        {
            int rId = int.Parse(id);
            var borrowings = _repository.GetByReader(rId);
            if (borrowings.Any())
            {
                return View(borrowings); // Return books of a specific genre as a View
            }
            return NotFound(); // Or return a NotFound result
        }

        [HttpPost]
        [Route("/Borrowing/Return/{id}")]
        public IActionResult ReturnBorrowing(int id)
        {
            try
            {
                
                _repository.ReturnBook(id, DateTime.Now);
                return RedirectToAction("GetAllBorrowings"); // Redirect back to the list of all borrowings
            }
            catch (KeyNotFoundException ex)
            {
                
                // For example, you might log the error and return an error view or a NotFound result
                return NotFound($"No borrowing found with ID {id}. Error: {ex.Message}");
            }
        }


    }
}

