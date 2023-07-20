namespace OnlineCoffeeShop.Core.DTOs
{
    public class OrderDTO
    {
        public string Id { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public CustomerDTO? Customer { get; set; }

    }
}
