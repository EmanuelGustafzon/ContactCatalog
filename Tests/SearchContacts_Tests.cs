using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Moq;

namespace Tests
{
    public class SearchContacts_Tests
    {
        [Fact]
        public void SearchContactShould_ReturnEnumerableOfMostSimilarresult()
        {
            Mock<IContactService> mockDataProvider = new();
            List<IContact> sampleData = [];
            IContact Emanuel = ContactFactory.Create("Emanuel", "Gustafzon", "email", "phone", "address", "postcode", "city");
            IContact Anna = ContactFactory.Create("Anna", "Svensson", "email", "phone", "address", "postcode", "city");
            IContact AnnaLisa = ContactFactory.Create("Anna-Lisa", "Larsson", "email", "phone", "address", "postcode", "city");

            sampleData.Add(Emanuel);
            sampleData.Add(Anna);
            sampleData.Add(AnnaLisa);

            mockDataProvider.Setup(dp => dp.GetAll()).Returns(sampleData);
            ISearchContactsService searchService = new SearchContactsService(mockDataProvider.Object);

            IEnumerable<IContact>? result = searchService.SearchContact("Annita");

            Assert.Equal(2, result?.Count());
            Assert.Equal("Anna", result?.First().Name);
            Assert.NotNull(result);
        }
    }
}
