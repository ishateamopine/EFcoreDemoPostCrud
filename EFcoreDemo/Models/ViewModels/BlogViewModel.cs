using FluentValidation;

namespace EFcoreDemo.Models.ViewModels
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        public string? Url { get; set; }
        public int PostCount { get; set; }
        public string? RssUrl { get; set; } // Sirf RssBlog ke liye
        public List<PostViewModel> Posts { get; set; } = new();
    }
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
