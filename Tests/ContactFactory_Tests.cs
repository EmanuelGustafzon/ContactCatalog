using Infrastructure.Factories;
using Infrastructure.Interfaces;

namespace Tests;
public class ContactFactory_Tests
{
    [Fact]
    public void CreateShould_ReturnNewContactOFTypeIContact()
    {
        IContact contact = ContactFactory.Create();
        Assert.NotNull(contact);
        Assert.NotNull(contact.ID);
        Assert.IsAssignableFrom<IContact>(contact);
    }
    [Fact]
    public void CreateOverloadWithParametersShould_ReturnNewPopulatedContactOFTypeIContact()
    {
        IContact contact = ContactFactory.Create("name", "lastname", "email", "phone", "address", "postcode", "city");
        Assert.IsAssignableFrom<IContact>(contact);
        Assert.Equal("name", contact.Name);
        Assert.Equal("lastname", contact.Lastname);
        Assert.Equal("email", contact.Email);
        Assert.Equal("phone", contact.Phone);
        Assert.Equal("address", contact.Address);
        Assert.Equal("postcode", contact.Postcode);
        Assert.Equal("city", contact.City);
    }
    [Fact]
    public void CreateObservableShould_ReturnAObservableEntity()
    {
        IContact contact = ContactFactory.Create("name", "lastname", "email", "phone", "address", "postcode", "city");
        IObservableContact observableContact = ContactFactory.CreateObservable(contact);

        Assert.IsAssignableFrom<IContact>(contact);
        Assert.Equal(contact.ID, observableContact.ID);
        Assert.Equal("name", observableContact.Name);
        Assert.Equal("lastname", observableContact.Lastname);
        Assert.Equal("email", observableContact.Email);
        Assert.Equal("phone", observableContact.Phone);
        Assert.Equal("address", observableContact.Address);
        Assert.Equal("postcode", observableContact.Postcode);
        Assert.Equal("city", observableContact.City);
    }
}
