using ErrorOr;
using MediatR;

namespace URLShortener.Application.Features.ShortUrls
{
    public record GetRedirectUrlQuery(string Path) : IRequest<ErrorOr<string>>;
}
