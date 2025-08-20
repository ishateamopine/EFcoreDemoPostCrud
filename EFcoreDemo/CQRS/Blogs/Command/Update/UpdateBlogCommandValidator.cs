using FluentValidation;

namespace EFcoreDemo.CQRS.Blogs.Command.Update
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(x => x.BlogId)
                .GreaterThan(0)
                .WithMessage("BlogId must be greater than 0.");

            RuleFor(x => x.Url)
                .NotEmpty()
                .WithMessage("Url is required.")
                .MaximumLength(200)
                .WithMessage("Url must not exceed 200 characters.");
        }
    }
}
