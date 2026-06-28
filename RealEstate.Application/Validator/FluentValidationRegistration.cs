using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstate.Application.Validator;

public static class FluentValidationRegistration
{
    public static IServiceCollection AddFluentValidationRegistration(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(FluentValidationRegistration).Assembly);

        return services;
    }
}