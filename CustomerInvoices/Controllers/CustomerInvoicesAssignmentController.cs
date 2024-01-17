using CustomerInvoices.Data;
using CustomerInvoices.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
            try
            {
                var services = _context.Invoices
                    .Where(i => i.CustomerId == id)
                    .Select(i => new ServiceDTO
                    {
                        ServiceId = i.ServiceId,
                        ServiceName = i.Service.ServiceName,
                        Description = i.Service.Description,
                        Price = i.Service.Price,
                        Duration = i.Service.Duration
                    })
                    .ToList();

                return Ok(services);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("service/{id}")]
        public IActionResult GetCustomersForService(int id)
        {
            try
            {
                var customers = _context.Invoices
                    .Where(i => i.ServiceId == id)
                    .Select(i => new CustomerDTO
                    {
                        CustomerId = i.CustomerId,
                        FirstName = i.Customer.FirstName,
                        LastName = i.Customer.LastName,
                        Email = i.Customer.Email,
                        Address = i.Customer.Address
                    })
                    .ToList();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("assign")]
        public IActionResult AssignCustomerToService([FromBody] AssignmentDTO assignmentDTO)
        {
            try
            {
               
                var existingCustomer = _context.Customers.Find(assignmentDTO.CustomerId);
                var existingService = _context.Services.Find(assignmentDTO.ServiceId);

                if (existingCustomer == null || existingService == null)
                {
                    return NotFound("Customer or Service not found.");
                }

                
                if (_context.Invoices.Any(i => i.CustomerId == assignmentDTO.CustomerId && i.ServiceId == assignmentDTO.ServiceId))
                {
                    return BadRequest("Customer already assigned to this service.");
                }

                
                var newInvoice = new Invoice
                {
                    CustomerId = assignmentDTO.CustomerId,
                    ServiceId = assignmentDTO.ServiceId,
                    InvoiceDate = DateTime.UtcNow,
                    TotalAmount = existingService.Price 
                };

                _context.Invoices.Add(newInvoice);
                _context.SaveChanges();

                return Ok("Customer assigned to service successfully.");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("remove/customer/{customerId}/service/{serviceId}")]
        public IActionResult RemoveCustomerServiceAssociation(int customerId, int serviceId)
        {
            try
            {
                var invoice = _context.Invoices
                    .FirstOrDefault(i => i.CustomerId == customerId && i.ServiceId == serviceId);

                if (invoice == null)
                {
                    return NotFound("Association not found.");
                }

                _context.Invoices.Remove(invoice);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
        

            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }
}
