namespace CustomerInvoices.DTOs.Requests
{
    public class ServiceRequestDTO
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Duration { get; set; }
    }
}
