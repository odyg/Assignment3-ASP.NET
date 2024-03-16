using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class ReaderModel
    {
        [Key]
        public int ReaderId
        {
            get; set;
        }
        [Required, StringLength(100)]
        public string Name
        {
            get; set;
        }
        [Required, EmailAddress]
        public string Email
        {
            get; set;
        }
        [Required]
        public int ZipCode
        {
            get; set;
        }
        // Assuming you have a way to connect BorrowingModels to this reader
        public List<BorrowingModel> BorrowedBooks { get; set; } = new List<BorrowingModel>();
    }
}
