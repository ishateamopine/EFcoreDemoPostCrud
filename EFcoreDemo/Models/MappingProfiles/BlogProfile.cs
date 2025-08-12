using AutoMapper;
using EFcoreDemo.Models;
using EFcoreDemo.Models.ViewModels;

namespace EFcoreDemo.Models.MappingProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            // Post -> PostViewModel-using auto mapper
            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.BlogUrl, opt => opt.MapFrom(src => src.Blog.Url));

            // Blog -> BlogViewModel
            CreateMap<Blog, BlogViewModel>()
                .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count))
                .ForMember(dest => dest.RssUrl, opt => opt.Ignore())
                .Include<RssBlog, BlogViewModel>();

            // RssBlog -> BlogViewModel
            CreateMap<RssBlog, BlogViewModel>()
                .ForMember(dest => dest.RssUrl, opt => opt.MapFrom(src => src.RssUrl));
        }
    }
}
