using Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Services;

public static class ValidateModelService
{
    public static List<ValidationResult> Validate<T>(T model)
    {
        if(model == null) return [];

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);
        bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

        if (isValid == false) return validationResults;
        return [];
    }
}
