using ErrorOr;

namespace URLShortener.Domain
{
    public interface IShortUrlRepository
    {
        Task<ErrorOr<ShortUrl>> GetByIdAsync(Guid id);
        Task<ErrorOr<ShortUrl>> GetByUrlAsync(string url);
        Task AddAsync(ShortUrl shortUrl);
        Task<ErrorOr<string>> GetRedirectUrlAsync(string path);
    }
}
