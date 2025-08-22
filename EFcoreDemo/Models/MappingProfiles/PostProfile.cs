using AutoMapper;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Models.ViewModels;

namespace EFcoreDemo.Models.MappingProfiles
{
    public class PostProfile : Profile
    {
        #region
        /// <summary>
        // Mapping profile for PostViewModel and Post.
        /// </summary>
        public PostProfile()
        {
            // PostViewModel → Post
            CreateMap<PostViewModel, Post>().ReverseMap();
        }
        #endregion
    }
}