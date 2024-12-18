using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace Tests;

public class ValidateModel_Tests
{
    [Fact]
    public void ValidateContactShould_ValidateAllDataAnnotations()
    {
        IContact validContact = ContactFactory.Create("Emanuel", "lastname", "w@s.s", "0704445529", "address", "postcode", "city");
        IContact invalidContact = ContactFactory.Create("", "lastname", "w@", "h", "address", "postcode", "city");

        List<ValidationResult> validResult = ValidateModelService.Validate(validContact);
        List<ValidationResult> invalidResult = ValidateModelService.Validate(invalidContact);

        Assert.Empty(validResult);
        Assert.Equal(3, invalidResult.Count);
        Assert.NotEmpty(invalidResult);
    }
}
