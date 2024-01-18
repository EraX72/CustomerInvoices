using CustomerInvoices.Data;
using CustomerInvoices.DTOs.Requests;
using CustomerInvoices.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
            try
            {
                var customers = _context.Customers.ToList();
                var customerDTOs = customers.Select(c => new CustomerResponseDTO
                {
                    CustomerId = c.CustomerId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Address = c.Address
                }).ToList();

                return Ok(customerDTOs);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound();
                }

                var customerDTO = new CustomerResponseDTO
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Address = customer.Address
                };

                return Ok(customerDTO);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerRequestDTO customerDTO)
        {
            try
            {
                var newCustomer = new Customer
                {
                    FirstName = customerDTO.FirstName,
                    LastName = customerDTO.LastName,
                    Email = customerDTO.Email,
                    Address = customerDTO.Address
                };

                _context.Customers.Add(newCustomer);
                _context.SaveChanges();

                var createdCustomerDTO = new CustomerResponseDTO
                {
                    CustomerId = newCustomer.CustomerId,
                    FirstName = newCustomer.FirstName,
                    LastName = newCustomer.LastName,
                    Email = newCustomer.Email,
                    Address = newCustomer.Address
                };

                return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.CustomerId }, createdCustomerDTO);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerRequestDTO customerDTO)
        {
            try
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
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
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
