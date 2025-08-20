using FluentValidation;

namespace EFcoreDemo.CQRS.Posts.Command.Delete
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.PostId)
                .GreaterThan(0).WithMessage("PostId must be greater than 0.");
        }
    }
}
