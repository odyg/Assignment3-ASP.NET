using Assignment3.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Repositories
{
    public class BorrowingRepository
    {
        private BookRepository _bookRepository;

        public BorrowingRepository(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public static List<BorrowingModel> _items = new List<BorrowingModel>()
        {
           new BorrowingModel { BorrowingId = 1, BookId = 3, ReaderId = 2, BorrowDate = new DateTime(2023, 3, 21)},
           new BorrowingModel { BorrowingId = 2, BookId = 5, ReaderId = 2, BorrowDate = new DateTime(2023, 3, 21)},
           new BorrowingModel { BorrowingId = 3, BookId = 9, ReaderId = 2, BorrowDate = new DateTime(2023, 1, 21)}

        };


        public void Add(BorrowingModel item)
        {
            var maxId = _items.Count;
            item.BorrowingId = maxId + 1;

            // Check if the book has already been borrowed
            if (_items.Any(i => i.BookId == item.BookId))
            {
                throw new InvalidOperationException("This book has already been borrowed.");
            }

            _items.Add(item);
            _bookRepository.UpdateBookAvailability(item.BookId, false);
        }

        public IEnumerable<BorrowingModel> GetAll()
        {
            return _items;
        }

        public BorrowingModel GetById(int id)
        {
            return _items.FirstOrDefault(item => item.BorrowingId == id);
        }
        //public BorrowingModel GetById(int id)
        //{
        //    return _items.FirstOrDefault(item => item.BorrowingId == id);
        //}
        public List<BorrowingModel> GetByReader(int ReaderId)
        {
            return _items.Where(item => item.ReaderId == ReaderId).ToList();
        }

        public void Update(int BorrowingId, BorrowingModel updatedBorrowing)
        {
            if (BorrowingId != updatedBorrowing.BorrowingId)
                return;
            var borrowing = _items.FirstOrDefault(b => b.BorrowingId == BorrowingId);
            if (borrowing != null)
            {
                // Update properties
                borrowing.BookId = updatedBorrowing.BookId;
                borrowing.ReaderId = updatedBorrowing.ReaderId;
                borrowing.BorrowDate = updatedBorrowing.BorrowDate;
                borrowing.ReturnDate = updatedBorrowing.ReturnDate;
            }
            else
            {
                throw new KeyNotFoundException($"No book found with ID {updatedBorrowing.BorrowingId}");
            }
        }

        public void Delete(int id)
        {
            var borrowing = _items.FirstOrDefault(b => b.BorrowingId == id);
            if (borrowing != null)
            {
                _bookRepository.SetBookAsAvailable(borrowing.BookId); // Set the book as available
                _items.Remove(borrowing);
            }
            else
            {
                throw new KeyNotFoundException($"No borrowing record found with ID {id}");
            }
        }
        public void ReturnBook(int borrowingId, DateTime returnDate)
        {
            var borrowing = _items.FirstOrDefault(b => b.BorrowingId == borrowingId);
            if (borrowing != null)
            {
                borrowing.ReturnDate = returnDate; // Set the return date
                _bookRepository.SetBookAsAvailable(borrowing.BookId); // Set the book as available
            }
            else
            {
                throw new KeyNotFoundException($"No borrowing record found with ID {borrowingId}");
            }
        }


    }
}
