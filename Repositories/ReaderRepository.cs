﻿using Assignment3.Models;

namespace Assignment3.Repositories
{
    public class ReaderRepository
    {
        public static List<ReaderModel> _items = new List<ReaderModel>()
        {
          new ReaderModel(){ReaderId = 1, Name = "John Doe", Email = "johnd@yahoo.com", ZipCode = 12345 },
          new ReaderModel(){ReaderId = 2, Name = "Jane Doe", Email = "janed@yahoo.com",ZipCode = 99345},
          new ReaderModel(){ReaderId = 3, Name = "John Smith", Email = "johnsmith@yahoo.com",ZipCode = 12345}
        };
        public List<BorrowingModel> GetBorrowedBooks(int readerId)
        {
            // Find the reader with the given ID
            var reader = _items.FirstOrDefault(r => r.ReaderId == readerId);
            return reader?.BorrowedBooks ?? new List<BorrowingModel>();
        }

        public void Add(ReaderModel item)
        {
            var maxId = _items.Max(item => item.ReaderId);
            item.ReaderId = maxId + 1;
            _items.Add(item);
        }

        public IEnumerable<ReaderModel> GetAll()
        {
            return _items;
        }

        public ReaderModel GetById(int id)
        {
            return _items.FirstOrDefault(item => item.ReaderId == id);
        }
        public List<ReaderModel> GetByZipCode(int zipcode)
        {
            return _items.Where(item => item.ZipCode == zipcode).ToList();
        }

        public void Update(int ReaderId, ReaderModel updatedReader)
        {
            if (ReaderId != updatedReader.ReaderId)
                return;
            var reader = _items.FirstOrDefault(b => b.ReaderId == ReaderId);
            if (reader != null)
            {
                // Update properties
                reader.Name = updatedReader.Name;
                reader.Email = updatedReader.Email;
                reader.ZipCode = updatedReader.ZipCode;

            }
            else
            {
                throw new KeyNotFoundException($"No book found with ID {updatedReader.ReaderId}");
            }
        }

        public bool Delete(int id)
        {
            var reader = _items.FirstOrDefault(b => b.ReaderId == id);
            if (reader != null)
            {
                if (reader.BorrowedBooks.Count > 0)
                {
                    return false;
                }
                _items.Remove(reader);
                return true;
            }
            else
            {
                throw new KeyNotFoundException($"No reader found with ID {id}");
            }
        }

        //public bool Delete(int id)
        //{
        //    var book = _items.FirstOrDefault(b => b.BookId == id);
        //    if (book != null)
        //    {
        //        if (book.IsAvailable == "No")
        //        {
        //            // Book is not available for deletion (it's borrowed)
        //            return false;
        //        }

        //        _items.Remove(book);
        //        return true;
        //    }
        //    else
        //    {
        //        throw new KeyNotFoundException($"No book found with ID {id}");
        //    }

        //}

        //public void Delete(int id)
        //{
        //    var reader = _items.FirstOrDefault(b => b.ReaderId == id);
        //    if (reader != null)
        //    {
        //        if (reader.BorrowedBooks.Count > 0)
        //        {
        //            throw new InvalidOperationException("Cannot delete reader with borrowed books.");
        //        }
        //        _items.Remove(reader);
        //    }
        //    else
        //    {
        //        throw new KeyNotFoundException($"No reader found with ID {id}");
        //    }
        //}
    }
}