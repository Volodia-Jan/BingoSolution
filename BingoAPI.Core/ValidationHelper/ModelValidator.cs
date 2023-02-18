using System.ComponentModel.DataAnnotations;

namespace BingoAPI.Core.ValidationHelper;
internal static class ModelValidator
{
    public static void Validate(object? model)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        ValidationContext validationContext = new ValidationContext(model);
        List<ValidationResult> validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);
        if (!isValid)
        {
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}
