using FluentValidation;

namespace EFcoreDemo.CQRS.Posts.Command.Update
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Posts.PostId)
                .GreaterThan(0).WithMessage("PostId must be greater than 0.");

            RuleFor(x => x.Posts.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(256).WithMessage("Title cannot exceed 256 characters.");

            RuleFor(x => x.Posts.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}
