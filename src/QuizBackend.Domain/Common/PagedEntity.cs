namespace QuizBackend.Domain.Common;

public class PagedEntity<T>
{
    public ICollection<T> Items { get; set; } = [];
    public int TotalItems { get; set; }

    public PagedEntity(ICollection<T> items, int totalItems)
    {
        Items = items;
        TotalItems = totalItems;
    }
}