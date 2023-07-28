namespace URLShortener.Contracts
{
    public record CreateShortUrlResponse(Guid Id, string Url, string ShortUrl);
}
