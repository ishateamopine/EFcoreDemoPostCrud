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
}
