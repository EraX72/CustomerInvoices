namespace CustomerInvoices.DTOs.Responses
{
    public class InvoiceResponseDTO
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public float TotalAmount { get; set; }
    }
}
