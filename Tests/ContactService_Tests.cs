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
        Mock<IRepository<IContactEntity>> mockDataProvider = new();
        List<IContactEntity> sampleData = [];

        [Fact]
        public void GetAll_ShouldReturnListOfTypeIContactEntity()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "e.d@g.d", "0761888619", "address", "postcode", "city");
            IContactEntity contactEntity = ContactFactory.Create(contact);

            sampleData.Add(contactEntity);
            mockDataProvider.Setup(dp => dp.Get()).Returns(sampleData);

            IContactService contactService = new ContactService(mockDataProvider.Object);
            var list = contactService.GetAll();

            Assert.Single(list);
        }
        [Fact]
        public void GetByID_ShouldReturnIContactEntity()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "e.d@g.d", "0761888619", "address", "postcode", "city");
            IContactEntity contactEntity = ContactFactory.Create(contact);
            string id = contactEntity.ID;
            sampleData.Add(contactEntity);

            mockDataProvider.Setup(dp => dp.Get(id)).Returns(contactEntity);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            IContact? foundContact = contactService.GetByID(id);

            Assert.NotNull(foundContact);
            Assert.IsAssignableFrom<IContactEntity>(contact);
        }
        [Fact]
        public void AddShould_validateAndAddContactOfTypeIContactEntity()
        {
            IContact validContact1 = ContactFactory.Create("Emanuel", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact validContact2 = ContactFactory.Create("Erik", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact invalidName = ContactFactory.Create("", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
            IContact invalidEmail = ContactFactory.Create("e", "lastname", "email", "0704445529", "address", "postcode", "city");
            IContact invalidPhone = ContactFactory.Create("e", "lastname", "ss@.s", "070y", "address", "postcode", "city");

            IContactEntity validEntity1 = ContactFactory.Create(validContact1);
            sampleData.Add(validEntity1);

            mockDataProvider.Setup(dp => dp.Get()).Returns(sampleData);
            mockDataProvider.Setup(dp => dp.Add(It.IsAny<IContactEntity>())).Returns(true);

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
            IContactEntity contactEntity = ContactFactory.Create(contact);
            sampleData.Add(contactEntity);

            mockDataProvider.Setup(dp => dp.Get(contactEntity.ID)).Returns(contactEntity);
            mockDataProvider.Setup(dp => dp.Delete(contactEntity.ID)).Returns(true);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            int result = contactService.Delete(contactEntity.ID);

            Assert.Equal((int)StatusCodes.OK, result);
     
        }
        [Fact]
        public void DeleteShould_ReturnBadRequestStatusCode_IfContactIsNotFound()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            IContactEntity contactEntity = ContactFactory.Create(contact);
            sampleData.Add(contactEntity);

            mockDataProvider.Setup(dp => dp.Get("None_existing_ID")).Returns((IContactEntity)null!);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            int result = contactService.Delete("None_existing_ID");

            Assert.Equal((int)StatusCodes.NotFound, result);

        }
        [Fact]
        public void UpdateShould_UpdateContact_IfNotLeadingToDuplicate()
        {
            IContact contact1 = ContactFactory.Create("Emanuel", "lastname", "e@d.s", "0761888619", "address", "postcode", "city");
            IContact contact2 = ContactFactory.Create("Erik", "lastname", "e@d.s", "0761888619", "address", "postcode", "city");
            IContact contact3 = ContactFactory.Create("Elsa", "lastname", "e@d.s", "0761888619", "address", "postcode", "city");
            IContactEntity entity1 = ContactFactory.Create(contact1);
            IContactEntity entity2 = ContactFactory.Create(contact2);
            sampleData.Add(entity1);
            sampleData.Add(entity2);

            mockDataProvider.Setup(dp => dp.Get()).Returns(sampleData);
            mockDataProvider.Setup(dp => dp.Update(It.IsAny<string>(), It.IsAny<IContactEntity>())).Returns(true);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            StatusResponse resultDuplicate = contactService.Update(entity1.ID, contact2);
            StatusResponse resultNotDuplicate = contactService.Update(entity1.ID, contact3);
            StatusResponse resultNoNameChange = contactService.Update(entity1.ID, contact1);

            Assert.Equal((int)StatusCodes.Duplicate, (int)resultDuplicate.StatusCode);
            Assert.Equal((int)StatusCodes.OK, (int)resultNotDuplicate.StatusCode);
            Assert.Equal((int)StatusCodes.OK, (int)resultNoNameChange.StatusCode);
        }
    }
}
