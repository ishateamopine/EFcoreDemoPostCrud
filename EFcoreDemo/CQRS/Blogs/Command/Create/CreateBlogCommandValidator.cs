using EFcoreDemo.Repositories.Interface;
using FluentValidation;

namespace EFcoreDemo.CQRS.Blogs.Command.Create
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        #region
        // This class is used to validate the CreateBlogCommand.
        public CreateBlogCommandValidator(IBlogRepository _blogRepository)
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL is required.")
                .MaximumLength(200).WithMessage("URL must not exceed 200 characters.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Invalid URL format.");
        }
        #endregion
    }
}
