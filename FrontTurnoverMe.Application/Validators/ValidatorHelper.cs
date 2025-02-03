using System.ComponentModel.DataAnnotations;

namespace FrontTurnoverMe.Application.Validators;

public static class ValidatorHelper
{
    public static void Validate<T>(T obj) where T : class, IValidatableObject
    {
        var context = new ValidationContext(obj, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(obj, context, results, true);

        if (results.Any())
        {
            throw new ValidationException(
                $"Validation for {typeof(T).Name} failed: {string.Join(", ", results.Select(x => x.ErrorMessage))}");
        }

        foreach (var property in obj.GetType().GetProperties())
        {
            if (typeof(IValidatableObject).IsAssignableFrom(property.PropertyType))
            {
                if (property.GetValue(obj) is IValidatableObject value)
                {
                    Validate(value);
                }
            }
            else if (typeof(IEnumerable<IValidatableObject>).IsAssignableFrom(property.PropertyType))
            {
                if (property.GetValue(obj) is IEnumerable<IValidatableObject> values)
                {
                    foreach (var item in values)
                    {
                        Validate(item);
                    }
                }
            }
        }
    }
}