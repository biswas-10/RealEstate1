

using FluentValidation;
using RealEstate.Application.DTOs.Agent;

namespace RealEstate.Application.Validator;

public class CreateAgentValidator : AbstractValidator<CreateAgentDto>
{
    public CreateAgentValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required");
        
        RuleFor(x => x.FullName)
            .MaximumLength(100)
            .WithMessage("Full name cannot exceed 100 characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.");
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email format.");
        
        RuleFor(x => x.Email)
            .MaximumLength(200)
            .WithMessage("Email cannot exceed 200 characters.");
        
        
    }
}

