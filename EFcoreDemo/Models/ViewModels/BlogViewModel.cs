using FluentValidation;

namespace EFcoreDemo.Models.ViewModels
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        public string? Url { get; set; }
        public int PostCount { get; set; }
        public List<PostViewModel> Posts { get; set; } = new();
    }
    // Using Fluent Validation
    public class BlogViewModelValidator : AbstractValidator<BlogViewModel>
    {
        public BlogViewModelValidator()
        {
            RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required")
            .MinimumLength(5).WithMessage("Url must be at least 5 characters long");
        }
    }
}
