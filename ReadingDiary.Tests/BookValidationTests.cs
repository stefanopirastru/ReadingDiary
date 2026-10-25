using System.ComponentModel.DataAnnotations;
using ReadingDiary.Api.Models;

namespace ReadingDiary.Tests;

public class BookValidationTests
{
    [Fact]
    public void Rating_GreaterThanFive_ShouldFailValidation()
    {
        var book = new Book
        {
            Title = "Clean Code",
            Author = "Robert Martin",
            Status = "Reading",
            CurrentPage = 10,
            TotalPages = 100,
            Rating = 8
        };

        var context =
            new ValidationContext(book);

        var results =
            new List<ValidationResult>();

        var isValid =
            Validator.TryValidateObject(
                book,
                context,
                results,
                true);

        Assert.False(isValid);
        Assert.NotEmpty(results);
    }

    [Fact]
    public void ValidBook_ShouldPassValidation()
    {
        var book = new Book
        {
            Title = "Clean Code",
            Author = "Robert Martin",
            Status = "Reading",
            CurrentPage = 10,
            TotalPages = 100,
            Rating = 5
        };

        var context =
            new ValidationContext(book);

        var results =
            new List<ValidationResult>();

        var isValid =
            Validator.TryValidateObject(
                book,
                context,
                results,
                true);

        Assert.True(isValid);
    }

    [Fact]
    public void Book_WithInvalidRating_ShouldFailValidation()
    {
        var book = new Book
        {
            Title = "Clean Code",
            Author = "Robert Martin",
            Status = "Reading",
            CurrentPage = 10,
            TotalPages = 100,
            Rating = 8
        };

        var context = new ValidationContext(book);

        var results = new List<ValidationResult>();

        var isValid =
            Validator.TryValidateObject(
                book,
                context,
                results,
                true);

        Assert.False(isValid);

        Assert.Contains(
            results,
            r => r.ErrorMessage!.Contains("rating"));
    }
}