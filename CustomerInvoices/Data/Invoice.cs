namespace CustomerInvoices.Data
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public float TotalAmount { get; set; }
        public Customer Customer { get; set; }
        public Service Service { get; set; }
    }
}
