using Microsoft.EntityFrameworkCore;
using ReadingDiary.Api.Models;

namespace ReadingDiary.Api.Data;

public class ReadingDiaryDbContext : DbContext
{
    public ReadingDiaryDbContext(DbContextOptions<ReadingDiaryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
}