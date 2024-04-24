namespace APITask1
{
    public class Pagination
    {
      
            public int PageNumber { get; set; } = 1; // Default to first page
            public int PageSize { get; set; } = 10; // Default to 10 items per page

            public int MaxPageSize { get; set; } = 100; // Optional limit on page size

            public int ValidatedPageSize
            {
                get { return (PageSize > MaxPageSize) ? MaxPageSize : PageSize; }
            }
        }

    }

