namespace Customers.Api.Aggregates.Customer.Models.Entity
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Email)}: {Email}, {nameof(Address)}: {Address}";
        }
    }
}
