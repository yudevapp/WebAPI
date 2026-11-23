namespace WebAPI.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

       public IList<Order>? Orders { get; set; } = [];
    }
}
