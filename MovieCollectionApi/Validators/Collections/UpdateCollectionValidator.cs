using FluentValidation;
using MovieCollectionApi.Dto;

namespace MovieCollectionApi.Validators;

public class UpdateCollectionValidator : AbstractValidator<UpdateCollectionDto>
{
    public UpdateCollectionValidator()
    {
        RuleFor(collection => collection.Title)
            .NotEmpty()
            .NotNull()
                .WithMessage("Title can not be empty or null")
            .MaximumLength(125)
            .MinimumLength(3)
                .WithMessage("Title must be between 3 - 125 characters long.");
    }
}
