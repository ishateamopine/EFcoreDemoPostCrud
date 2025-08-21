using EFcoreDemo.Repositories.Interface;
using FluentValidation;

namespace EFcoreDemo.CQRS.Blogs.Command.Create
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator(IBlogRepository _blogRepository)
        {

            // Basic rules
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL is required.")
                .MaximumLength(200).WithMessage("URL must not exceed 200 characters.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Invalid URL format.");
        }
    }
}
