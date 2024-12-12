
using Infrastructure.Models;
using System.Text.Json.Serialization;

namespace Infrastructure.Interfaces;

[JsonDerivedType(typeof(Contact), nameof(Contact))]
public interface IContact
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Postcode { get; set; }
    public string City { get; set; }
}
