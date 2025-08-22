using FluentValidation;

namespace EFcoreDemo.CQRS.Blogs.Queries.GetById
{
    public class GetBlogByIdQueryValidator : AbstractValidator<GetBlogByIdCommand>
    {
        #region
        /// <summary>
        // Validator for GetBlogByIdCommand.
        /// </summary>
        public GetBlogByIdQueryValidator()
        {
            RuleFor(x => x.BlogId)
                .GreaterThan(0)
                .WithMessage("BlogId must be greater than 0.");
        }
        #endregion
    }
}
