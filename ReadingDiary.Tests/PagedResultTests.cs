using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReadingDiary.Api.Models;

namespace ReadingDiary.Tests;

public class PagedResultTests
{
    [Fact]
    public void TotalPages_ShouldReturnTwo_WhenSixItemsWithPageSizeFive()
    {
        var result =
            new PagedResult<string>
            {
                TotalCount = 6,
                PageSize = 5
            };

        Assert.Equal(2, result.TotalPages);
    }

    [Fact]
    public void TotalPages_ShouldReturnOne_WhenItemsFitSinglePage()
    {
        var result =
            new PagedResult<string>
            {
                TotalCount = 5,
                PageSize = 5
            };

        Assert.Equal(1, result.TotalPages);
    }
}