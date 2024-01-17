using CustomerInvoices.Data;
using CustomerInvoices.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInvoices.Controllers
{
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllInvoices()
        {
            var invoices = _context.Invoices.ToList();
            var invoiceDTOs = invoices.Select(i => new InvoiceDTO { InvoiceId = i.InvoiceId, CustomerId = i.CustomerId, ServiceId = i.ServiceId, InvoiceDate = i.InvoiceDate, TotalAmount = i.TotalAmount }).ToList();
            return Ok(invoiceDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoiceById(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }
            var invoiceDTO = new InvoiceDTO { InvoiceId = invoice.InvoiceId, CustomerId = invoice.CustomerId, ServiceId = invoice.ServiceId, InvoiceDate = invoice.InvoiceDate, TotalAmount = invoice.TotalAmount };
            return Ok(invoiceDTO);
        }

        [HttpPost]
        public IActionResult CreateInvoice([FromBody] InvoiceDTO invoiceDTO)
        {
            var newInvoice = new Invoice { CustomerId = invoiceDTO.CustomerId, ServiceId = invoiceDTO.ServiceId, InvoiceDate = invoiceDTO.InvoiceDate, TotalAmount = invoiceDTO.TotalAmount };
            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();
            var createdInvoiceDTO = new InvoiceDTO { InvoiceId = newInvoice.InvoiceId, CustomerId = newInvoice.CustomerId, ServiceId = newInvoice.ServiceId, InvoiceDate = newInvoice.InvoiceDate, TotalAmount = newInvoice.TotalAmount };
            return CreatedAtAction(nameof(GetInvoiceById), new { id = newInvoice.InvoiceId }, createdInvoiceDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvoice(int id, [FromBody] InvoiceDTO invoiceDTO)
        {
            var existingInvoice = _context.Invoices.Find(id);
            if (existingInvoice == null)
            {
                return NotFound();
            }
            existingInvoice.CustomerId = invoiceDTO.CustomerId;
            existingInvoice.ServiceId = invoiceDTO.ServiceId;
            existingInvoice.InvoiceDate = invoiceDTO.InvoiceDate;
            existingInvoice.TotalAmount = invoiceDTO.TotalAmount;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            var invoice = _context.Invoices.Find(id);
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
