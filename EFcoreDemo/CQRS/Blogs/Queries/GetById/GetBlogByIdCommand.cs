using AutoMapper;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Queries.GetById
{
    public record GetBlogByIdCommand(int BlogId) : IRequest<BlogViewModel?>;
}
