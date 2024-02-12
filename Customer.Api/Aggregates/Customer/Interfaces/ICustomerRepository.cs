using Customers.Api.Aggregates.Customer.Models.Dto;

namespace Customers.Api.Aggregates.Customer.Interfaces;

public interface ICustomerRepository
{
    /// <summary>
    /// Method to get a collection of customers
    /// </summary>
    /// <returns>A collection of <see cref="CustomerDto"/></returns>
    public Task<IEnumerable<CustomerDto>?> GetCustomersAsync();

    /// <summary>
    /// Method get a customer
    /// </summary>
    /// <param name="id">Identifier for a customer</param>
    /// <returns>A <see cref="CustomerDto"/></returns>
    Task<CustomerDto?> GetCustomer(int id);

    /// <summary>
    /// Method to update a customer
    /// </summary>
    /// <param name="customerToUpdate">A <see cref="UpdateCustomerDto"/></param>
    /// <returns></returns>
    Task UpdateCustomerAsync(UpdateCustomerDto customerToUpdate);

    /// <summary>
    /// Method to create a customer
    /// </summary>
    /// <param name="customerToCreate">A <see cref="CreateCustomerDto"/></param>
    /// <returns>Identifier for the newly created customer</returns>
    Task<int> CreateCustomerAsync(CreateCustomerDto customerToCreate);

    /// <summary>
    /// Method to delete an existing customer
    /// </summary>
    /// <param name="id">Identifier for a customer</param>
    /// <returns></returns>
    Task DeleteCustomerAsync(int id);
}