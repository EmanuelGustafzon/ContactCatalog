using Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;
public class Contact : IContact
{
    public string ID { get; set; } = Guid.NewGuid().ToString();

    [MinLength(1, ErrorMessage = "Name must be at least 1 character")]
    public string Name { get; set; } = null!;
    public string Lastname { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public string City { get; set; } = null!;
}
