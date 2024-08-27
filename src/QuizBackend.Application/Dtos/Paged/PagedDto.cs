namespace QuizBackend.Application.Dtos.Paged
{
    public class PagedDto<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }

        public PagedDto(List<T> items, int totalItems, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
        }

    }
}
