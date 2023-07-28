using ErrorOr;
using Microsoft.Extensions.Caching.Memory;
using URLShortener.Domain;

namespace URLShortener.Infrastructure.Domain
{
    internal class CachedShortUrlRepository : IShortUrlRepository
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IMemoryCache _memoryCache;

        public CachedShortUrlRepository(
            IShortUrlRepository shortUrlRepository,
            IMemoryCache memoryCache)
        {
            _shortUrlRepository = shortUrlRepository;
            _memoryCache = memoryCache;
        }

        public async Task AddAsync(ShortUrl shortUrl)
        {
            await _shortUrlRepository.AddAsync(shortUrl);
        }

        public async Task<ErrorOr<ShortUrl>> GetByIdAsync(Guid id)
        {
            return await _shortUrlRepository.GetByIdAsync(id);
        }

        public async Task<ErrorOr<ShortUrl>> GetByUrlAsync(string url)
        {
            var key = $"Url:{url}";
            return await _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromHours(2));
                    return _shortUrlRepository.GetByUrlAsync(url);
                });
        }

        public async Task<ErrorOr<string>> GetRedirectUrlAsync(string path)
        {
            var key = $"Path:{path}";
            return await _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromHours(2));
                    return _shortUrlRepository.GetRedirectUrlAsync(path);
                });
        }
    }
}
