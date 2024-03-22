namespace Order.Host.Models.Requests
{
    public class UpdateItemRequest<T>
    {
        public T Item { get; set; } = default(T) !;
    }
}
