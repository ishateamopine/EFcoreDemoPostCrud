using FluentValidation;

namespace EFcoreDemo.Models.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int BlogId { get; set; }
        public string? BlogUrl { get; set; }
    }
    //Using Fluent Validation
    public class PostViewModelValidator : AbstractValidator<PostViewModel>
    {
        public PostViewModelValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title required hai.")
                .MaximumLength(256).WithMessage("Title max 256 characters ka hona chahiye.");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("Content required hai.")
                .MinimumLength(10).WithMessage("Content kam se kam 10 characters ka hona chahiye.");

            RuleFor(p => p.BlogId)
                .GreaterThan(0).WithMessage("Valid Blog select karo.");
        }
    }
}
