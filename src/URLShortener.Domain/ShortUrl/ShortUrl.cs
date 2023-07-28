using ErrorOr;

namespace URLShortener.Domain
{
    public class ShortUrl
    {
        public Guid Id { get; }
        public string Url { get; }
        public string Path { get; }
        public DateTime CreateDateTime { get; }

        private ShortUrl()
        {
        }

        private ShortUrl(Guid id, string url, string path, DateTime createDateTime)
        {
            Id = id;
            Url = url;
            Path = path;
            CreateDateTime = createDateTime;
        }

        public static ErrorOr<ShortUrl> Create(string url, string shortUrl)
        {
            var errors = new List<Error>();

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                errors.Add(Errors.ShortUrl.InvalidUrl);
            }

            if (string.IsNullOrEmpty(shortUrl))
            {
                errors.Add(Errors.ShortUrl.InvalidShortUrl);
            }

            if (errors.Any())
            {
                return errors;
            }

            return new ShortUrl(Guid.NewGuid(), url, shortUrl, DateTime.UtcNow);
        }
    }
}
