using System.ComponentModel.DataAnnotations;

namespace ReadingDiary.Api.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters.")]
        public string Author { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        [Range(0, 10000,
            ErrorMessage = "Current page must be greater than or equal to 0.")]
        public int CurrentPage { get; set; }

        [Range(1, 10000,
            ErrorMessage = "Total pages must be at least 1.")]
        public int TotalPages { get; set; }

        [Range(1, 5,
            ErrorMessage = "Please enter a rating between 1 and 5.")]
        public int Rating { get; set; }

        public string? Notes { get; set; }
    }
}
