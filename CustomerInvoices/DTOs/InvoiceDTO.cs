namespace CustomerInvoices.DTOs
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public float TotalAmount { get; set; }
    }
}
