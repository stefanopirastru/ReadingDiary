namespace ReadingDiary.Api.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int Rating { get; set; }

        public string? Notes { get; set; }
    }
}
