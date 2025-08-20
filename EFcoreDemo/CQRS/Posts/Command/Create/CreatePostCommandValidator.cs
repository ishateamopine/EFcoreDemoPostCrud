using FluentValidation;

namespace EFcoreDemo.CQRS.Posts.Command.Create
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.Posts.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(256).WithMessage("Title must not exceed 256 characters.");

            RuleFor(x => x.Posts.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.Posts.BlogId)
                .GreaterThan(0).WithMessage("BlogId must be greater than 0.");
        }
    }
}
