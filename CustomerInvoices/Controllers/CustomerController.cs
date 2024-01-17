using CustomerInvoices.Data;
using CustomerInvoices.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInvoices.Controllers
{
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            var customerDTOs = customers.Select(c => new CustomerDTO { CustomerId = c.CustomerId, FirstName = c.FirstName, LastName = c.LastName, Email = c.Email, Address = c.Address }).ToList();
            return Ok(customerDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerDTO = new CustomerDTO { CustomerId = customer.CustomerId, FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email, Address = customer.Address };
            return Ok(customerDTO);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            var newCustomer = new Customer { FirstName = customerDTO.FirstName, LastName = customerDTO.LastName, Email = customerDTO.Email, Address = customerDTO.Address };
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            var createdCustomerDTO = new CustomerDTO { CustomerId = newCustomer.CustomerId, FirstName = newCustomer.FirstName, LastName = newCustomer.LastName, Email = newCustomer.Email, Address = newCustomer.Address };
            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.CustomerId }, createdCustomerDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerDTO customerDTO)
        {
            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.FirstName = customerDTO.FirstName;
            existingCustomer.LastName = customerDTO.LastName;
            existingCustomer.Email = customerDTO.Email;
            existingCustomer.Address = customerDTO.Address;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }

}
