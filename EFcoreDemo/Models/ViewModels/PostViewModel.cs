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
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(256).WithMessage("Title must be 256 characters.");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("Content must be 10 characters.");

            RuleFor(p => p.BlogId)
                .GreaterThan(0).WithMessage("Select any one Valid Blog.");
        }
    }
}
