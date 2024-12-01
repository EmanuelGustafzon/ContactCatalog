using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Models.Enums;
using Moq;

namespace Tests
{
    public class ContactService_Tests
    {
        Mock<IRepository<IContact>> mockDataProvider = new();
        List<IContact> sampleData = [];

        [Fact]
        public void GetAll_ShouldReturnListOfTypeIContact()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            sampleData.Add(contact);
            mockDataProvider.Setup(dp => dp.Get()).Returns(sampleData);

            IContactService contactService = new ContactService(mockDataProvider.Object);
            var list = contactService.GetAll();

            Assert.Single(list);
        }
        [Fact]
        public void GetByID_ShouldReturnIContact()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            string id = contact.ID;
            sampleData.Add(contact);

            mockDataProvider.Setup(dp => dp.Get(id)).Returns(contact);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            IContact? foundContact = contactService.GetByID(id);

            Assert.NotNull(foundContact);
            Assert.IsAssignableFrom<IContact>(contact);
        }
        [Fact]
        public void AddShould_AddNewContact_IfNotLeadingToDuplicate()
        {
            IContact contact1 = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            IContact contact2 = ContactFactory.Create("Erik", "lastname", "email", "phone", "address", "postcode", "city");
            sampleData.Add(contact1);

            mockDataProvider.Setup(dp => dp.Get()).Returns(sampleData);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            int ResultDuplicate = contactService.Add(contact1);
            int ResultNotDuplicate = contactService.Add(contact2);

            Assert.Equal((int)StatusCodes.Duplicate, ResultDuplicate);
            Assert.Equal((int)StatusCodes.OK, ResultNotDuplicate);
        }
        [Fact]
        public void DeleteShould_ReturnOKStatusCode_IfContactIsFound()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            sampleData.Add(contact);

            mockDataProvider.Setup(dp => dp.Get(contact.ID)).Returns(contact);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            int result = contactService.Delete(contact.ID);

            Assert.Equal((int)StatusCodes.OK, result);
     
        }
        [Fact]
        public void DeleteShould_ReturnBadRequestStatusCode_IfContactIsNotFound()
        {
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            sampleData.Add(contact);

            mockDataProvider.Setup(dp => dp.Get("None_existing_ID")).Returns((IContact)null!);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            int result = contactService.Delete("None_existing_ID");

            Assert.Equal((int)StatusCodes.BadRequest, result);

        }
        [Fact]
        public void UpdateShould_UpdateContact_IfNotLeadingToDuplicate()
        {
            IContact contact1 = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            IContact contact2 = ContactFactory.Create("Erik", "lastname", "email", "phone", "address", "postcode", "city");
            IContact contact3 = ContactFactory.Create("Elsa", "lastname", "email", "phone", "address", "postcode", "city");
            sampleData.Add(contact1);
            sampleData.Add(contact2);

            mockDataProvider.Setup(dp => dp.Get()).Returns(sampleData);

            IContactService contactService = new ContactService(mockDataProvider.Object);

            int ResultDuplicate = contactService.Update(contact1.ID, contact2);
            int ResultNotDuplicate = contactService.Update(contact1.ID, contact3);
            int ResultNoNameChange = contactService.Update(contact1.ID, contact1);

            Assert.Equal((int)StatusCodes.Duplicate, ResultDuplicate);
            Assert.Equal((int)StatusCodes.OK, ResultNotDuplicate);
            Assert.Equal((int)StatusCodes.OK, ResultNoNameChange);
        }
    }
}
