using AutoMapper;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Queries.GetById
{
    public record GetPostByIdCommand(int PostId) : IRequest<PostViewModel>;
}
