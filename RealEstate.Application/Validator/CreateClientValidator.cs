using FluentValidation;
using RealEstate.Application.DTOs.Client;

namespace RealEstate.Application.Validator;

public class CreateClientValidator : AbstractValidator<CreateClientDto>
{
    public CreateClientValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(100)
            .WithMessage(
                "Full name cannot exceed 100 characters.");
        
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MaximumLength(20)
            .WithMessage(
                "Phone number cannot exceed 20 characters.")
            .Matches(@"^\+?[0-9]+$")
            .WithMessage(
                "Phone number must contain only digits.");
    }
}