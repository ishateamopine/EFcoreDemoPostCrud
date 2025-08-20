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


            // Business logic: URL must be unique
            RuleFor(x => x.Url)
                .MustAsync(async (url, cancellation) =>
                {
                    var exists = await _blogRepository.UrlExistsAsync(url, cancellation);
                    return !exists; // true = valid, false = fail
                })
                .WithMessage("This URL already exists. Please choose a different one.");
        }
    }
}
