namespace WebAPI.Data
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionName { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
