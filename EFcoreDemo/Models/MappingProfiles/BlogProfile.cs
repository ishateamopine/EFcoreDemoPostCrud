using AutoMapper;
using EFcoreDemo.Models.Domain;
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
                .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count));
     
        }
    }
}
