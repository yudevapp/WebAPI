namespace WebAPI.Data
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
