using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Models.Enums;
using Moq;
using Infrastructure.Models;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public class ContactService_Tests
    {
        Mock<IRepository<IContact>> _mockDataProvider = new();
        IContactService _contactService;
        List<IContact> _sampleData = [];

        public ContactService_Tests()
        {
            _contactService = new ContactService(_mockDataProvider.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfTypeIContactEntity()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "e.d@g.d", "0761888619", "address", "postcode", "city");

            _sampleData.Add(contact);
            _mockDataProvider.Setup(dp => dp.Get()).Returns(_sampleData);
            
            var list = _contactService.GetAll();

            Assert.Single(list);
        }
        [Fact]
        public void GetByID_ShouldReturnIContactEntity()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "e.d@g.d", "0761888619", "address", "postcode", "city");
            string id = contact.ID;
            _sampleData.Add(contact);

            _mockDataProvider.Setup(dp => dp.Get(id)).Returns(contact);

            IContact? foundContact = _contactService.GetByID(id);

            Assert.NotNull(foundContact);
            Assert.IsAssignableFrom<IContact>(contact);
        }
        [Fact]
        public void AddShould_validateAndAddContactOfTypeIContactEntity()
        {
            IContact validContact1 = ContactFactory.Create("Emanuel", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact validContact2 = ContactFactory.Create("Erik", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact invalidName = ContactFactory.Create("", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact invalidEmail = ContactFactory.Create("e", "lastname", "email", "0704445529", "address", "postcode", "city");
            IContact invalidPhone = ContactFactory.Create("e", "lastname", "ss@.s", "070y", "address", "postcode", "city");

            _sampleData.Add(validContact1);

            _mockDataProvider.Setup(dp => dp.Get()).Returns(_sampleData);
            _mockDataProvider.Setup(dp => dp.Add(It.IsAny<IContact>())).Returns(true);

            StatusResponse resultDuplicate = _contactService.Add(validContact1);
            StatusResponse resultNotDuplicate = _contactService.Add(validContact2);

            StatusResponse nameValidation = _contactService.Add(invalidName);
            StatusResponse emailValidation = _contactService.Add(invalidEmail);
            StatusResponse phoneValidation = _contactService.Add(invalidPhone);

            Assert.Equal((int)StatusCodes.Duplicate ,(int)resultDuplicate.StatusCode);
            Assert.Equal((int)StatusCodes.Created ,(int)resultNotDuplicate.StatusCode);

            Assert.Equal((int)StatusCodes.BadRequest ,(int)nameValidation.StatusCode);
            Assert.Equal((int)StatusCodes.BadRequest ,(int)emailValidation.StatusCode);
            Assert.Equal((int)StatusCodes.BadRequest ,(int)phoneValidation.StatusCode);

        }
        [Fact]
        public void DeleteShould_ReturnOKStatusCode_IfContactIsFound()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            _sampleData.Add(contact);

            _mockDataProvider.Setup(dp => dp.Get(contact.ID)).Returns(contact);
            _mockDataProvider.Setup(dp => dp.Delete(contact.ID)).Returns(true);

            StatusResponse result = _contactService.Delete(contact.ID);

            Assert.Equal((int)StatusCodes.OK, result.StatusCode);
     
        }
        [Fact]
        public void DeleteShould_ReturnBadRequestStatusCode_IfContactIsNotFound()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            _sampleData.Add(contact);

            _mockDataProvider.Setup(dp => dp.Get("None_existing_ID")).Returns((IContact)null!);

            StatusResponse result = _contactService.Delete("None_existing_ID");

            Assert.Equal((int)StatusCodes.NotFound, result.StatusCode);

        }
        [Fact]
        public void UpdateShould_ValidateAndUpdateContact()
        {
            IContact validContact1 = ContactFactory.Create("Emanuel", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact validContact2 = ContactFactory.Create("Erik", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact invalidName = ContactFactory.Create("", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact invalidEmail = ContactFactory.Create("e", "lastname", "email", "0704445529", "address", "postcode", "city");
            IContact invalidPhone = ContactFactory.Create("e", "lastname", "ss@.s", "070y", "address", "postcode", "city");

            _mockDataProvider.Setup(dp => dp.Get(validContact1.ID)).Returns(validContact1);
            _mockDataProvider.Setup(dp => dp.Update(It.IsAny<string>(), It.IsAny<IContact>())).Returns(true);

            StatusResponse resultOk = _contactService.Update(validContact1.ID, validContact2);

            StatusResponse nameValidation = _contactService.Update(validContact1.ID, invalidName);
            StatusResponse emailValidation = _contactService.Update(validContact1.ID, invalidEmail);
            StatusResponse phoneValidation = _contactService.Update(validContact1.ID, invalidPhone);

            Assert.Equal((int)StatusCodes.OK, (int)resultOk.StatusCode);

            Assert.Equal((int)StatusCodes.BadRequest, (int)nameValidation.StatusCode);
            Assert.Equal((int)StatusCodes.BadRequest, (int)emailValidation.StatusCode);
            Assert.Equal((int)StatusCodes.BadRequest, (int)phoneValidation.StatusCode);
        }
    }
}
