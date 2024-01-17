using CustomerInvoices.Data;
using CustomerInvoices.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInvoices.Controllers
{
    [Route("api/services")]
    public class ServiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllServices()
        {
            var services = _context.Services.ToList();
            var serviceDTOs = services.Select(s => new ServiceDTO { ServiceId = s.ServiceId, ServiceName = s.ServiceName, Description = s.Description, Price = s.Price, Duration = s.Duration }).ToList();
            return Ok(serviceDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceById(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            var serviceDTO = new ServiceDTO { ServiceId = service.ServiceId, ServiceName = service.ServiceName, Description = service.Description, Price = service.Price, Duration = service.Duration };
            return Ok(serviceDTO);
        }

        [HttpPost]
        public IActionResult CreateService([FromBody] ServiceDTO serviceDTO)
        {
            var newService = new Service { ServiceName = serviceDTO.ServiceName, Description = serviceDTO.Description, Price = serviceDTO.Price, Duration = serviceDTO.Duration };
            _context.Services.Add(newService);
            _context.SaveChanges();
            var createdServiceDTO = new ServiceDTO { ServiceId = newService.ServiceId, ServiceName = newService.ServiceName, Description = newService.Description, Price = newService.Price, Duration = newService.Duration };
            return CreatedAtAction(nameof(GetServiceById), new { id = newService.ServiceId }, createdServiceDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, [FromBody] ServiceDTO serviceDTO)
        {
            var existingService = _context.Services.Find(id);
            if (existingService == null)
            {
                return NotFound();
            }
            existingService.ServiceName = serviceDTO.ServiceName;
            existingService.Description = serviceDTO.Description;
            existingService.Price = serviceDTO.Price;
            existingService.Duration = serviceDTO.Duration;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            _context.Services.Remove(service);
            _context.SaveChanges();
            return NoContent();
        }
    }

}
