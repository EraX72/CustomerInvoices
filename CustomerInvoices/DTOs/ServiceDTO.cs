namespace CustomerInvoices.DTOs
{
    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Duration { get; set; }
    }
}
