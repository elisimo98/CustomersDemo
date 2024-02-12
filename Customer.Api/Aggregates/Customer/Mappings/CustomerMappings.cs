using Customers.Api.Aggregates.Customer.Models.Dto;
using Customers.Api.Aggregates.Customer.Models.Entity;

namespace Customers.Api.Aggregates.Customer.Mappings
{
    public static class CustomerMappings
    {
        /// <summary>
        /// Helper method to map a <see cref="CustomerEntity"/> to a <see cref="CustomerDto"/>
        /// </summary>
        /// <param name="customerEntity">A <see cref="CustomerEntity"/></param>
        /// <returns>A <see cref="CustomerDto"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static CustomerDto ToDto(this CustomerEntity customerEntity)
        {
            if (customerEntity == null) throw new ArgumentNullException(nameof(customerEntity));

            return new CustomerDto()
            {
                Id = customerEntity.Id,
                Name = customerEntity.Name,
                Email = customerEntity.Email,
                Address = customerEntity.Address
            };
        }

    }
}
