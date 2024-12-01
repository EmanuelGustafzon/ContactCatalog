
using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Infrastructure.Models;
internal class ObservableContact : ObservableObject, IObservableContact
{
    public string ID { get; set; } = null!;

    private string _name = null!;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _lastname = null!;
    public string Lastname
    {
        get => _lastname;
        set => SetProperty(ref _lastname, value);
    }
    private string _email = null!;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }
    private string _phone = null!;
    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }
    private string _address = null!;
    public string Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }
    private string _postcode = null!;
    public string Postcode
    {
        get => _postcode;
        set => SetProperty(ref _postcode, value);
    }
    private string _city = null!;
    public string City
    {
        get => _city;
        set => SetProperty(ref _city, value);
    }
}
