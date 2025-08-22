using FluentValidation;

namespace EFcoreDemo.CQRS.Blogs.Command.Delete
{
    public class DeleteBlogCommandValidator : AbstractValidator<DeleteBlogCommand>
    {
        #region
        /// <summary>
        // Validator for DeleteBlogCommand
        /// </summary>
        public DeleteBlogCommandValidator()
        {
            RuleFor(x => x.BlogId)
                .GreaterThan(0).WithMessage("Invalid BlogId. BlogId must be greater than 0.");
        }
        #endregion
    }
}
