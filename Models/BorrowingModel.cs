using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class BorrowingModel
    {
      
            [Key]
            public int BorrowingId { get; set; }

            [Required]
            public int BookId { get; set; } // Foreign key from Book model

            [Required]
            public int ReaderId { get; set; } // Foreign key from Reader model

            [DataType(DataType.Date)]
            public DateTime BorrowDate { get; set; }

            [DataType(DataType.Date)]
            public DateTime? ReturnDate { get; set; } // Nullable for cases when the book hasn't been returned yet
        
    }
}
