namespace EFcoreDemo.CQRS.Common.Interface
{
    public interface IPostValidator
    {
        Task ValidateDuplicateTitleAsync(string title);

        Task<string?> ValidateUpdateTitleAsync(int postId, string title);
    }
}
