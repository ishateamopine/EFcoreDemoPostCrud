using AutoMapper;
using EFcoreDemo.Models.ViewModels;
using EFcoreDemo.Repositories.Interface;
using MediatR;

namespace EFcoreDemo.CQRS.Blogs.Queries.GetAll
{
    public class GetAllBlogsCommandHandler : IRequestHandler<GetAllBlogsQueryWithDetails, IEnumerable<BlogViewModel>>
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBlogsCommandHandler(IBlogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region GetAll
        /// <summary>
        // Retrieves all blog entries with their details.
        /// </summary>
        public async Task<IEnumerable<BlogViewModel>> Handle(GetAllBlogsQueryWithDetails request, CancellationToken cancellationToken)
        {
            var blogs = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
        }
        #endregion
    }
}
