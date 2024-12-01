using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Tests;

public class ContactRepository_Tests
{
    [Fact]
    public void AddShould_AddContactToList()
    {
        Mock<IJsonService<IContact>> mockJsonServcie = new();
        Mock<IFileService> mockFileServcie = new();
        ContactRepository contactRepo = new(mockJsonServcie.Object, mockFileServcie.Object);

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        int result = contactRepo.Add(contact);

        Assert.Equal(200, result);
    }
    [Fact]
    public void GetShould_returnAllEntities()
    {
        Mock<IJsonService<IContact>> mockJsonServcie = new();
        Mock<IFileService> mockFileServcie = new();
        ContactRepository contactRepo = new(mockJsonServcie.Object, mockFileServcie.Object);

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        contactRepo.Add(contact);

        IEnumerable<IContact> list = contactRepo.Get();

        Assert.Single(list);
    }
    [Fact]
    public void GetOverLoadWithIDShould_returnAnIContactWithThatID()
    {
        Mock<IJsonService<IContact>> mockJsonServcie = new();
        Mock<IFileService> mockFileServcie = new();
        ContactRepository contactRepo = new(mockJsonServcie.Object, mockFileServcie.Object);

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        contactRepo.Add(contact);

        IContact? foundContact = contactRepo.Get(contact.ID);

        Assert.Equal(foundContact?.ID, contact.ID);
        Assert.IsAssignableFrom<IContact>(foundContact);
    }
    public void UpdateShould_UpdateAContact()
    {
        Mock<IJsonService<IContact>> mockJsonServcie = new();
        Mock<IFileService> mockFileServcie = new();
        ContactRepository contactRepo = new(mockJsonServcie.Object, mockFileServcie.Object);

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        IContact updatedContact = ContactFactory.Create("Erik", "svensson", "domain@gmail.com", "0701", "aaa", "123", "GBG");
        contactRepo.Add(contact);
        contactRepo.Update(contact.ID, updatedContact);

        IContact? foundContact = contactRepo.Get(contact.ID); 

        Assert.Equal("Erik", foundContact?.Name);
        Assert.Equal("svensson", foundContact?.Lastname);
        Assert.Equal("domain@gmail.com", foundContact?.Email);
        Assert.Equal("0701", foundContact?.Phone);
        Assert.Equal("aaa", foundContact?.Address);
        Assert.Equal("123", foundContact?.Postcode);
        Assert.Equal("GBG", foundContact?.City);
    }
}
