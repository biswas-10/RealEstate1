using FluentValidation;
using RealEstate.Application.DTOs.Property;

namespace RealEstate.Application.Validator;

public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyDto>
{
    public UpdatePropertyValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address cannot be empty.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
    }
}