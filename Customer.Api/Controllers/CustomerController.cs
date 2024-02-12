using Customers.Api.Aggregates.Customer.Interfaces;
using Customers.Api.Aggregates.Customer.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();

            if (customers == null)
                return NotFound();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            if (id <= 0)
                return BadRequest();

            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
                return NotFound();

            return customer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, UpdateCustomerDto customerToUpdate)
        {
            if (customerToUpdate.Id <= 0)
                return BadRequest();

            try
            {
                await _customerRepository.UpdateCustomerAsync(customerToUpdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                // optional: handle retry
                throw;
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CreateCustomerDto customerToCreate)
        {
            var id = await _customerRepository.CreateCustomerAsync(customerToCreate);
            var customer = await _customerRepository.GetCustomer(id);
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id <= 0)
                return BadRequest();

            await _customerRepository.DeleteCustomerAsync(id);

            return NoContent();
        }

    }
}
