namespace SchoolManagementConsumingWebApi.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Mobile { get; set; }
        public string? Email { get; set; }
        public double Amount { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
    }
}
