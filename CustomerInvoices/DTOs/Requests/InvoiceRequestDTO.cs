namespace CustomerInvoices.DTOs.Requests
{
    public class InvoiceRequestDTO
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public float TotalAmount { get; set; }
    }
}
