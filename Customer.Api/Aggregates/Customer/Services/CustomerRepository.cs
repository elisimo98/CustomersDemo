using Customers.Api.Aggregates.Customer.Interfaces;
using Customers.Api.Aggregates.Customer.Mappings;
using Customers.Api.Aggregates.Customer.Models.Dto;
using Customers.Api.Aggregates.Customer.Models.Entity;
using Customers.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Aggregates.Customer.Services
{
    /// <inheritdoc />
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ILogger<CustomerRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>?> GetCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            _logger.LogTrace("Found Customers: {customers}", string.Join(", ", customers));
            return !customers.Any() ? null : customers.Select(x => x.ToDto());
        }

        public async Task<CustomerDto?> GetCustomer(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            var customer = await _context.Customers.FindAsync(id);
            _logger.LogTrace("Found Customer: {customer}", customer);
            return customer?.ToDto();
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto customerToUpdate)
        {
            if (customerToUpdate == null) throw new ArgumentNullException(nameof(customerToUpdate));

            var existingCustomer = await _context.Customers.FindAsync(customerToUpdate.Id);
            if (existingCustomer == null)
               throw new Exception($"Could not find Customer '{customerToUpdate.Id}'");

            existingCustomer.Name = customerToUpdate.Name;
            existingCustomer.Email = customerToUpdate.Email;
            existingCustomer.Address = customerToUpdate.Address;

            _logger.LogTrace("Updating Customer with details: {existingCustomer}", existingCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateCustomerAsync(CreateCustomerDto customerToCreate)
        {
            if (customerToCreate == null) throw new ArgumentNullException(nameof(customerToCreate));

            var newCustomer = new CustomerEntity
            {
                Name = customerToCreate.Name,
                Email = customerToCreate.Email,
                Address = customerToCreate.Address
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return newCustomer.Id;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new Exception($"Could not find Customer with Id '{id}'");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

    }
}
