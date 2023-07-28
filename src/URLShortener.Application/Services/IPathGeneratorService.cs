using ErrorOr;

namespace URLShortener.Application.Services
{
    public interface IPathGeneratorService
    {
        Task<ErrorOr<string>> Create(string url);
    }
}
