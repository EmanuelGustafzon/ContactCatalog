namespace Infrastructure.Interfaces;

public interface IAddressInfo
{
    public string Address { get; set; }
    public string Postcode { get; set; }
    public string City { get; set; }
}