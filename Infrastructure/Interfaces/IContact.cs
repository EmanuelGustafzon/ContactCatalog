
using Infrastructure.Models;
using System.Text.Json.Serialization;

namespace Infrastructure.Interfaces;

[JsonDerivedType(typeof(Contact), nameof(Contact))]
public interface IContact
{
    string ID { get; set; }
    string Name { get; set; }
    string Lastname { get; set; }
    string Email { get; set; }
    string Phone { get; set; }
    string Address { get; set; }
    string Postcode { get; set; }
    string City { get; set; }

}
