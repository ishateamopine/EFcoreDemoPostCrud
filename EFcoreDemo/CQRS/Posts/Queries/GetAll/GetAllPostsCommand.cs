using AutoMapper;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Queries.GetAll
{
    public record GetAllPostsCommand() : IRequest<IEnumerable<PostViewModel>>;
}
