﻿using Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;
internal class Contact : IContact
{
    public string Name { get; set; } = null!;
    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public string City { get; set; } = null!;
}
