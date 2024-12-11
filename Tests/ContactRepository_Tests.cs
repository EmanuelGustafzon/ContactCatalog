using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Tests;

public class ContactRepository_Tests
{
    Mock<IJsonService<IContact>> _mockJsonServcie = new();
    Mock<IFileService> _mockFileServcie = new();
    readonly ContactRepository _contactRepo;

    public ContactRepository_Tests()
    {
        _contactRepo = new(_mockJsonServcie.Object, _mockFileServcie.Object);
    }
    [Fact]
    public void AddShould_AddContactToList()
    {
        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        bool result = _contactRepo.Add(contact);

        Assert.True(result);
    }
    [Fact]
    public void GetShould_returnAllEntities()
    {

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        _contactRepo.Add(contact);

        IEnumerable<IContact> list = _contactRepo.Get();

        Assert.Single(list);
    }
    [Fact]
    public void GetOverLoadWithIDShould_returnAnIContactWithThatID()
    {

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        _contactRepo.Add(contact);

        IContact? foundContact = _contactRepo.Get(contact.ID);

        Assert.Equal(foundContact?.ID, contact.ID);
        Assert.IsAssignableFrom<IContact>(foundContact);
    }
    [Fact]
    public void UpdateShould_UpdateAContact()
    {

        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        IContact updatedContact = ContactFactory.Create("Erik", "svensson", "domain@gmail.com", "0701", "aaa", "123", "GBG");
        _contactRepo.Add(contact);
        bool result = _contactRepo.Update(contact.ID, updatedContact);

        IContact? foundContact = _contactRepo.Get(contact.ID); 

        Assert.True(result);
        Assert.Equal("Erik", foundContact?.Name);
        Assert.Equal("svensson", foundContact?.Lastname);
        Assert.Equal("domain@gmail.com", foundContact?.Email);
        Assert.Equal("0701", foundContact?.Phone);
        Assert.Equal("aaa", foundContact?.Address);
        Assert.Equal("123", foundContact?.Postcode);
        Assert.Equal("GBG", foundContact?.City);
    }
    [Fact]
    public void PopulateFromFile_ShouldReadFromFileAndPopulateList_AndReturnTrue()
    {
        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        List<IContact> sampleList = [];
        sampleList.Add(contact);

        _mockFileServcie.Setup(fs => fs.ReadFile(It.IsAny<string>())).Returns("json");
        _mockJsonServcie.Setup(js => js.Deserialize(It.IsAny<string>())).Returns(sampleList);

        bool result = _contactRepo.PopulateListFromFile();
        Assert.True(result);
        _mockFileServcie.Verify(x => x.ReadFile(It.IsAny<string>()), Times.AtLeastOnce);
        _mockJsonServcie.Verify(x => x.Deserialize(It.IsAny<string>()), Times.AtLeastOnce);
    }
    [Fact]
    public void SaveChangesShould_SerilizeAndSaveListToFile()
    {
        string json = "{\"$id\": \"1\", \"$values\": [{\"$id\": \"2\", \"$type\": \"Contact\", \"ID\": \"0ac60420-0af5-4a12-9ebb-8a18d2641a6c\", \"Name\": \"Emanuel\", \"Lastname\": \"Larsson\", \"Email\": \"email\", \"Phone\": \"Phone\", \"Address\": \"address\", \"Postcode\": \"Postcode\", \"City\": \"City\"}]}";
        IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
        List<IContact> sampleList = [];
        sampleList.Add(contact);

        _mockJsonServcie.Setup(js => js.Serialize(It.IsAny<List<IContact>>())).Returns(json);
        _mockFileServcie.Setup(fs => fs.WriteFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        bool result = _contactRepo.SaveChanges();
        Assert.True(result);
        _mockJsonServcie.Verify(x => x.Serialize(It.IsAny<List<IContact>>()), Times.Once);
        _mockFileServcie.Verify(x => x.WriteFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}
