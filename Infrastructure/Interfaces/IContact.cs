﻿
using Infrastructure.Models;
using System.Text.Json.Serialization;

namespace Infrastructure.Interfaces;

[JsonDerivedType(typeof(Contact), nameof(Contact))]
[JsonDerivedType(typeof(ObservableContact), nameof(ObservableContact))]
[JsonDerivedType(typeof(ContactEntity), nameof(ContactEntity))]
public interface IContact
{
    string Name { get; set; }
    string Lastname { get; set; }
    string Email { get; set; }
    string Phone { get; set; }
    string Address { get; set; }
    string Postcode { get; set; }
    string City { get; set; }

}
