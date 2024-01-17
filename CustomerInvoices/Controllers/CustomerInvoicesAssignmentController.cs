using CustomerInvoices.Data;
using CustomerInvoices.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInvoices.Controllers
{
    [Route("api/customerinvoices")]
    public class CustomerInvoicesAssignmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerInvoicesAssignmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("customer/{id}")]
        public IActionResult GetServicesForCustomer(int id)
        {
            // Implement logic to get services associated with a specific customer
            var services = _context.Invoices.Where(i => i.CustomerId == id).Select(i => new ServiceDTO { /* Map properties */ }).ToList();
            return Ok(services);
        }

        [HttpGet("service/{id}")]
        public IActionResult GetCustomersForService(int id)
        {
            // Implement logic to get customers associated with a specific service
            var customers = _context.Invoices.Where(i => i.ServiceId == id).Select(i => new CustomerDTO { /* Map properties */ }).ToList();
            return Ok(customers);
        }

        [HttpPost("assign")]
        public IActionResult AssignCustomerToService([FromBody] AssignmentDTO assignmentDTO)
        {
            // Implement logic to assign a customer to a service
            // You may need to validate the existence of the customer and service
            // and handle cases where the association already exists
            // Example: _context.Invoices.Add(new Invoice { CustomerId = assignmentDTO.CustomerId, ServiceId = assignmentDTO.ServiceId });
            // _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("remove/customer/{customerId}/service/{serviceId}")]
        public IActionResult RemoveCustomerServiceAssociation(int customerId, int serviceId)
        {
            // Implement logic to remove the association between a customer and a service
            var invoice = _context.Invoices.FirstOrDefault(i => i.CustomerId == customerId && i.ServiceId == serviceId);
            if (invoice == null)
            {
                return NotFound();
            }
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
            return NoContent();
        }
    }

}
