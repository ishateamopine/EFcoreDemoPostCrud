using AutoMapper;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Queries.GetAll
{
    public record GetAllBlogsQueryWithDetails() : IRequest<IEnumerable<BlogViewModel>>;
}
