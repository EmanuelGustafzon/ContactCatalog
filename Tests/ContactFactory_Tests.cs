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
    public void CreateWithIContactAsArgumentShould_ReturnAIContactEntity()
    {
        IContact contact = ContactFactory.Create("name", "lastname", "email", "phone", "address", "postcode", "city");
        IContactEntity contactEntity = ContactFactory.Create(contact);
        Assert.IsAssignableFrom<IContactEntity>(contactEntity);
        Assert.Equal("name", contactEntity.Name);
        Assert.Equal("lastname", contactEntity.Lastname);
        Assert.Equal("email", contactEntity.Email);
        Assert.Equal("phone", contactEntity.Phone);
        Assert.Equal("address", contactEntity.Address);
        Assert.Equal("postcode", contactEntity.Postcode);
        Assert.Equal("city", contactEntity.City);
    }
    [Fact]
    public void CreateObservableShould_ReturnAObservableEntity()
    {
        IContact contact = ContactFactory.Create("name", "lastname", "email", "phone", "address", "postcode", "city");
        IContactEntity contactEntity = ContactFactory.Create(contact);
        IObservableContact observableContact = ContactFactory.CreateObservable(contactEntity);

        Assert.IsAssignableFrom<IContact>(contact);
        Assert.Equal(contactEntity.ID, observableContact.ID);
        Assert.Equal("name", observableContact.Name);
        Assert.Equal("lastname", observableContact.Lastname);
        Assert.Equal("email", observableContact.Email);
        Assert.Equal("phone", observableContact.Phone);
        Assert.Equal("address", observableContact.Address);
        Assert.Equal("postcode", observableContact.Postcode);
        Assert.Equal("city", observableContact.City);
    }
}
