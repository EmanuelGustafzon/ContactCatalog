using Infrastructure.Interfaces;

namespace Infrastructure.Models;
internal class Contact : IContact
{
    public string ID { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public string City { get; set; } = null!;
}
