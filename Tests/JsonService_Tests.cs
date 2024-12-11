using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace Tests
{
    public class JsonService_Tests
    {
        [Fact]
        public void Serialize_ShouldSerilize_ListOfPolymorphedObjectsToJson()
        {
            IJsonService<IContact> jsonService = new JsonService<IContact>();

            List<IContact> list = [];
            IContact contact = ContactFactory.Create("Emanuel", "lastname", "email", "phone", "address", "postcode", "city");
            list.Add(contact);

            string serilizedList =  jsonService.Serialize(list);
            Assert.NotEmpty(serilizedList);
            Assert.Contains("\"Name\": \"Emanuel\"", serilizedList);

        }
        [Fact]
        public void Deserialize_ShouldDeSerilize_JsonToListOfPolymorphedObjects()
        {
            IJsonService<IContact> jsonService = new JsonService<IContact>();

            string json = "{\"$id\": \"1\", \"$values\": [{\"$id\": \"2\", \"$type\": \"Contact\", \"ID\": \"0ac60420-0af5-4a12-9ebb-8a18d2641a6c\", \"Name\": \"Emanuel\", \"Lastname\": \"Larsson\", \"Email\": \"email\", \"Phone\": \"Phone\", \"Address\": \"address\", \"Postcode\": \"Postcode\", \"City\": \"City\"}]}";
            List<IContact>? list = jsonService.Deserialize(json);
            
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Equal("Emanuel", list.First().Name);
            Assert.IsAssignableFrom<IContact>(list.First());
        }
    }
}
