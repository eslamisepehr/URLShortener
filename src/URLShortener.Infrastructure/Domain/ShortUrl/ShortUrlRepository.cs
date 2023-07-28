using ErrorOr;
using Microsoft.EntityFrameworkCore;
using URLShortener.Domain;
using URLShortener.Infrastructure.Database;

namespace URLShortener.Infrastructure.Domain
{
    internal class ShortUrlRepository : IShortUrlRepository
    {
        private readonly AppDbContext _context;

        public ShortUrlRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShortUrl shortUrl)
        {
            await _context.ShortUrls.AddAsync(shortUrl);
        }

        public async Task<ErrorOr<ShortUrl>> GetByIdAsync(Guid id)
        {
            return await _context.ShortUrls.FindAsync(id);
        }

        public async Task<ErrorOr<ShortUrl>> GetByUrlAsync(string url)
        {
            return await _context.ShortUrls
                .Where(a => a.Url == url)
                .FirstOrDefaultAsync();
        }

        public async Task<ErrorOr<string>> GetRedirectUrlAsync(string path)
        {
            return await _context.ShortUrls
                .Where(a => a.Path == path)
                .Select(a => a.Url)
                .FirstOrDefaultAsync();
        }
    }
}
