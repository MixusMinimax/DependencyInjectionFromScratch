using System.ComponentModel.DataAnnotations;

namespace DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class ScopeIdAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return value is not Guid
            ? new ValidationResult($"The parameter must be of type {nameof(Guid)}")
            : base.IsValid(value, validationContext);
    }
}