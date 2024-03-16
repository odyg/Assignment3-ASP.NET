using Assignment3.Models;
//using System.Linq;
//using System.Collections.Generic;

namespace Assignment3.Repositories
{
    public class BookRepository
    {
        public static List<BookModel> _items = new List<BookModel>() 
        {
           new BookModel { BookId = 1, Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "9780061120084", PublicationYear = 1960, Genre = "Classic Fiction", IsAvailable = "Yes" },
           new BookModel { BookId = 2, Title = "1984", Author = "George Orwell", ISBN = "9780451524935", PublicationYear = 1949, Genre = "Dystopian Fiction", IsAvailable = "Yes" },
           new BookModel { BookId = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565", PublicationYear = 1925, Genre = "Classic Fiction", IsAvailable = "No" },
           new BookModel { BookId = 4, Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", ISBN = "9780590353427", PublicationYear = 1997, Genre = "Fantasy", IsAvailable = "Yes" },
           new BookModel { BookId = 5, Title = "The Hobbit", Author = "J.R.R. Tolkien", ISBN = "9780618260300", PublicationYear = 1937, Genre = "Fantasy", IsAvailable = "No" },
           new BookModel { BookId = 6, Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "9780679783268", PublicationYear = 1813, Genre = "Classic Romance", IsAvailable = "Yes" },
           new BookModel { BookId = 7, Title = "The Catcher in the Rye", Author = "J.D. Salinger", ISBN = "9780316769488", PublicationYear = 1951, Genre = "Literary Fiction", IsAvailable = "Yes" },
           new BookModel { BookId = 8, Title = "The Lord of the Rings: The Fellowship of the Ring", Author = "J.R.R. Tolkien", ISBN = "9780544003415", PublicationYear = 1954, Genre = "Fantasy", IsAvailable = "Yes" },
           new BookModel { BookId = 9, Title = "The Da Vinci Code", Author = "Dan Brown", ISBN = "9780307474278", PublicationYear = 2003, Genre = "Mystery Thriller", IsAvailable = "No" },
           new BookModel { BookId = 10, Title = "Sapiens: A Brief History of Humankind", Author = "Yuval Noah Harari", ISBN = "9780062316097", PublicationYear = 2011, Genre = "Non-Fiction", IsAvailable = "Yes" }
        };

        public void Add(BookModel item)
        {
            var maxId = _items.Max(item => item.BookId);
            item.BookId = maxId + 1;
            _items.Add(item);
        }

        public IEnumerable<BookModel> GetAll()
        {
            return _items;
        }

        public BookModel GetById(int id)
        {
            return _items.FirstOrDefault(item => item.BookId == id);
        }
        public List<BookModel> GetByGenre(string genre)
        {
            return _items.Where(item => item.Genre == genre).ToList();
        }

        public void Update(int BookId, BookModel updatedBook)
        {
            if (BookId != updatedBook.BookId)
                return;
            var book = _items.FirstOrDefault(b => b.BookId == BookId);
            if (book != null)
            {
                // Update properties
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.ISBN = updatedBook.ISBN;
                book.PublicationYear = updatedBook.PublicationYear;
                book.Genre = updatedBook.Genre;
                book.IsAvailable = updatedBook.IsAvailable;
            }
            else
            {
                throw new KeyNotFoundException($"No book found with ID {updatedBook.BookId}");
            }
        }

        public bool Delete(int id)
        {
            var book = _items.FirstOrDefault(b => b.BookId == id);
            if (book != null)
            {
                if (book.IsAvailable == "No")
                {
                    // Book is not available for deletion (it's borrowed)
                    return false;
                }

                _items.Remove(book);
                return true;
            }
            else
            {
                throw new KeyNotFoundException($"No book found with ID {id}");
            }
        }
        public void UpdateBookAvailability(int bookId, bool isAvailable)
        {
            var book = _items.FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                book.IsAvailable = isAvailable ? "Yes" : "No";
            }
            else
            {
                throw new KeyNotFoundException($"No book found with ID {bookId}");
            }
        }
        public void SetBookAsAvailable(int bookId)
        {
            var book = _items.FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                book.IsAvailable = "Yes";
            }
            else
            {
                throw new KeyNotFoundException($"No book found with ID {bookId}");
            }
        }

    }
}
