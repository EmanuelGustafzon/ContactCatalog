
using Infrastructure.Models;
using System.Text.Json.Serialization;

namespace Infrastructure.Interfaces;

[JsonDerivedType(typeof(Contact), nameof(Contact))]
public interface IContact : IAddressInfo, IPerson
{
    public string ID { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

}
