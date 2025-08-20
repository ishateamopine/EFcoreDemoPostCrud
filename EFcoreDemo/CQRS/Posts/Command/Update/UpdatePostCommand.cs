using AutoMapper;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Posts.Command.Update
{
    public record UpdatePostCommand(PostViewModel Posts) : IRequest<bool>;
}
