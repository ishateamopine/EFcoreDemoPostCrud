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
}
