using System.ComponentModel.DataAnnotations;

namespace DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class ScopeIdAttribute : Attribute
{
}