namespace Customers.Api.Aggregates.Customer.Models.Dto
{
    public class CreateCustomerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Email)}: {Email}, {nameof(Address)}: {Address}";
        }
    }
}
