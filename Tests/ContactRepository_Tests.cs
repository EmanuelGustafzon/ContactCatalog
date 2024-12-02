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
        Mock<IJsonService<IContactEntity>> mockJsonServcie = new();
        Mock<IFileService> mockFileServcie = new();
        ContactRepository contactRepo = new(mockJsonServcie.Object, mockFileServcie.Object);

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        IContactEntity contactEntity = ContactFactory.Create(contact);
        bool result = contactRepo.Add(contactEntity);

        Assert.True(result);
    }
    [Fact]
    public void GetShould_returnAllEntities()
    {
        Mock<IJsonService<IContactEntity>> mockJsonServcie = new();
        Mock<IFileService> mockFileServcie = new();
        ContactRepository contactRepo = new(mockJsonServcie.Object, mockFileServcie.Object);

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        IContactEntity contactEntity = ContactFactory.Create(contact);
        contactRepo.Add(contactEntity);

        IEnumerable<IContact> list = contactRepo.Get();

        Assert.Single(list);
    }
    [Fact]
    public void GetOverLoadWithIDShould_returnAnIContactWithThatID()
    {
        Mock<IJsonService<IContactEntity>> mockJsonServcie = new();
        Mock<IFileService> mockFileServcie = new();
        ContactRepository contactRepo = new(mockJsonServcie.Object, mockFileServcie.Object);

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        IContactEntity contactEntity = ContactFactory.Create(contact);
        contactRepo.Add(contactEntity);

        IContactEntity? foundContact = contactRepo.Get(contactEntity.ID);

        Assert.Equal(foundContact?.ID, contactEntity.ID);
        Assert.IsAssignableFrom<IContact>(foundContact);
    }
    [Fact]
    public void UpdateShould_UpdateAContact()
    {
        Mock<IJsonService<IContactEntity>> mockJsonServcie = new();
        Mock<IFileService> mockFileServcie = new();
        ContactRepository contactRepo = new(mockJsonServcie.Object, mockFileServcie.Object);

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        IContact updatedContact = ContactFactory.Create("Erik", "svensson", "domain@gmail.com", "0701", "aaa", "123", "GBG");
        IContactEntity contactEntity = ContactFactory.Create(contact);
        IContactEntity updatedContactEntity = ContactFactory.Create(updatedContact);
        contactRepo.Add(contactEntity);
        bool result = contactRepo.Update(contactEntity.ID, updatedContactEntity);

        IContactEntity? foundContact = contactRepo.Get(contactEntity.ID); 

        Assert.True(result);
        Assert.Equal("Erik", foundContact?.Name);
        Assert.Equal("svensson", foundContact?.Lastname);
        Assert.Equal("domain@gmail.com", foundContact?.Email);
        Assert.Equal("0701", foundContact?.Phone);
        Assert.Equal("aaa", foundContact?.Address);
        Assert.Equal("123", foundContact?.Postcode);
        Assert.Equal("GBG", foundContact?.City);
    }
}
