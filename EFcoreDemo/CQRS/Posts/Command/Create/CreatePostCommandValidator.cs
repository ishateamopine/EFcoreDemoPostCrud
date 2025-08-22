using FluentValidation;

namespace EFcoreDemo.CQRS.Posts.Command.Create
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        #region
        /// <summary>
        // Validator for CreatePostCommand.
        /// </summary>
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.posts.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(256).WithMessage("Title must not exceed 256 characters.");

            RuleFor(x => x.posts.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.posts.BlogId)
                .GreaterThan(0).WithMessage("BlogId must be greater than 0.");
        }
        #endregion
    }
}
