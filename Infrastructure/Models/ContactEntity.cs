using Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ContactEntity : IContactEntity
{
    public string ID { get; set; } = Guid.NewGuid().ToString();

    [MinLength(1, ErrorMessage = "Name is Required")]
    public string Name { get; set; } = null!;
    public string Lastname { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public string City { get; set; } = null!;
}
