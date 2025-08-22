using FluentValidation;

namespace EFcoreDemo.CQRS.Posts.Command.Update
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        #region
        /// <summary>
        // Validator for UpdatePostCommand.
        /// </summary>
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.posts.PostId)
                .GreaterThan(0).WithMessage("PostId must be greater than 0.");

            RuleFor(x => x.posts.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(256).WithMessage("Title cannot exceed 256 characters.");

            RuleFor(x => x.posts.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
        #endregion
    }
}
