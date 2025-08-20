using AutoMapper;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Models.ViewModels;

namespace EFcoreDemo.Models.MappingProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            // PostViewModel → Post
            CreateMap<PostViewModel, Post>().ReverseMap();
        }
    }
}