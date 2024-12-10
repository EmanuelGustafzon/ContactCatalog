using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Models.Enums;
using Moq;
using Infrastructure.Models;

namespace Tests
{
    public class ContactService_Tests
    {
        Mock<IRepository<IContact>> mockDataProvider = new();
        List<IContact> sampleData = [];

        [Fact]
        public void GetAll_ShouldReturnListOfTypeIContactEntity()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "e.d@g.d", "0761888619", "address", "postcode", "city");

            sampleData.Add(contact);
            mockDataProvider.Setup(dp => dp.Get()).Returns(sampleData);

            IContactService contactService = new ContactService(mockDataProvider.Object);
            var list = contactService.GetAll();

            Assert.Single(list);
        }
        [Fact]
        public void GetByID_ShouldReturnIContactEntity()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "e.d@g.d", "0761888619", "address", "postcode", "city");
            string id = contact.ID;
            sampleData.Add(contact);

            mockDataProvider.Setup(dp => dp.Get(id)).Returns(contact);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            IContact? foundContact = contactService.GetByID(id);

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

            sampleData.Add(validContact1);

            mockDataProvider.Setup(dp => dp.Get()).Returns(sampleData);
            mockDataProvider.Setup(dp => dp.Add(It.IsAny<IContact>())).Returns(true);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            StatusResponse resultDuplicate = contactService.Add(validContact1);
            StatusResponse resultNotDuplicate = contactService.Add(validContact2);

            StatusResponse nameValidation = contactService.Add(invalidName);
            StatusResponse emailValidation = contactService.Add(invalidEmail);
            StatusResponse phoneValidation = contactService.Add(invalidPhone);

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
            sampleData.Add(contact);

            mockDataProvider.Setup(dp => dp.Get(contact.ID)).Returns(contact);
            mockDataProvider.Setup(dp => dp.Delete(contact.ID)).Returns(true);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            StatusResponse result = contactService.Delete(contact.ID);

            Assert.Equal((int)StatusCodes.OK, result.StatusCode);
     
        }
        [Fact]
        public void DeleteShould_ReturnBadRequestStatusCode_IfContactIsNotFound()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            sampleData.Add(contact);

            mockDataProvider.Setup(dp => dp.Get("None_existing_ID")).Returns((IContact)null!);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            StatusResponse result = contactService.Delete("None_existing_ID");

            Assert.Equal((int)StatusCodes.NotFound, result.StatusCode);

        }
        [Fact]
        public void UpdateShould_UpdateContact_IfNotLeadingToDuplicate()
        {
            IContact validContact1 = ContactFactory.Create("Emanuel", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact validContact2 = ContactFactory.Create("Erik", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact invalidName = ContactFactory.Create("", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact invalidEmail = ContactFactory.Create("e", "lastname", "email", "0704445529", "address", "postcode", "city");
            IContact invalidPhone = ContactFactory.Create("e", "lastname", "ss@.s", "070y", "address", "postcode", "city");

            mockDataProvider.Setup(dp => dp.Get(validContact1.ID)).Returns(validContact1);
            mockDataProvider.Setup(dp => dp.Update(It.IsAny<string>(), It.IsAny<IContact>())).Returns(true);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            StatusResponse resultOk = contactService.Update(validContact1.ID, validContact2);

            StatusResponse nameValidation = contactService.Update(validContact1.ID, invalidName);
            StatusResponse emailValidation = contactService.Update(validContact1.ID, invalidEmail);
            StatusResponse phoneValidation = contactService.Update(validContact1.ID, invalidPhone);

            Assert.Equal((int)StatusCodes.OK, (int)resultOk.StatusCode);

            Assert.Equal((int)StatusCodes.BadRequest, (int)nameValidation.StatusCode);
            Assert.Equal((int)StatusCodes.BadRequest, (int)emailValidation.StatusCode);
            Assert.Equal((int)StatusCodes.BadRequest, (int)phoneValidation.StatusCode);
        }
    }
}
