// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WEB_APIv1.Core.DTOs;
using WEB_APIv1.Core.Models.Shop;
using WEB_APIv1.Core.Services;
using WEB_APIv1.Core.Services.Shop;
using WEB_APIv1.Server.Services.Email;
using WEB_APIv1.Server.ViewModels.Shop;

namespace WEB_APIv1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly ICustomerService _customerService;

        public CustomerController(IMapper mapper, ILogger<CustomerController> logger, IEmailSender emailSender,
            ICustomerService customerService)
        {
            _mapper = mapper;
            _logger = logger;
            _emailSender = emailSender;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allCustomers = _customerService.GetAllCustomersData();
            return Ok(_mapper.Map<IEnumerable<CustomerVM>>(allCustomers));
        }
        [HttpGet]
        public IActionResult GetCustomerPredictedOrders()
        {
            var allCustomersp = _customerService.GetCustomerPredictedOrders();
            return Ok(_mapper.Map<IEnumerable<CustomerOrderPredictionDto>>(allCustomersp));
        }

        [HttpGet("throw")]
        public IEnumerable<CustomerVM> Throw()
        {
            throw new CustomerException($"This is a test exception: {DateTime.Now}");
        }

        [HttpGet("email")]
        public async Task<string> Email()
        {
            var recipientName = "Tester";
            var recipientEmail = "test@ebenmonney.com"; //   <===== Put the recipient's email here

            var message = EmailTemplates.GetTestEmail(recipientName, DateTime.UtcNow);

            (var success, var errorMsg) = await _emailSender.SendEmailAsync(recipientName, recipientEmail,
                "Test Email from WEB_APIv1", message);

            if (success)
                return "Success";

            return $"Error: {errorMsg}";
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<CustomerVM>(customer));
        }

        // Obtener los clientes más activos
        [HttpGet("top/{count}")]
        public IActionResult GetTopActiveCustomers(int count)
        {
            var customers = _customerService.GetTopActiveCustomers(count);
            return Ok(_mapper.Map<IEnumerable<CustomerVM>>(customers));
        }
        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerVM customerDto)
        {
            if (customerDto == null)
                return BadRequest("Invalid customer data.");

            var customer = _mapper.Map<Customer>(customerDto);
            _customerService.AddCustomer(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustId }, _mapper.Map<CustomerVM>(customer));
        }

        // Actualizar un cliente
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerVM customerDto)
        {
            if (customerDto == null || id != customerDto.Id)
                return BadRequest("Invalid customer data.");

            var customer = _mapper.Map<Customer>(customerDto);
            _customerService.UpdateCustomer(customer);

            return NoContent();
        }

        // Eliminar un cliente
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _customerService.GetCustomerById(id);
            if (existingCustomer == null)
                return NotFound();

            _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
