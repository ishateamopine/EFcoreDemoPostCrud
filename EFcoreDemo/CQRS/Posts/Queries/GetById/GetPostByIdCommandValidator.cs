using FluentValidation;

namespace EFcoreDemo.CQRS.Posts.Queries.GetById
{
    public class GetPostByIdCommandValidator : AbstractValidator<GetPostByIdCommand>
    {
        #region
        /// <summary>
        // Validator for GetPostByIdCommand.
        /// </summary>
        public GetPostByIdCommandValidator()
        {
            RuleFor(x => x.PostId)
                .GreaterThan(0)
                .WithMessage("PostId must be greater than 0.");
        }
        #endregion
    }
}
