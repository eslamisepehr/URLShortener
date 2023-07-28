using ErrorOr;
using MediatR;
using URLShortener.Domain;

namespace URLShortener.Application.Features.ShortUrls
{
    public record CreateShortUrlCommand(string Url) : IRequest<ErrorOr<ShortUrl>>;
}
