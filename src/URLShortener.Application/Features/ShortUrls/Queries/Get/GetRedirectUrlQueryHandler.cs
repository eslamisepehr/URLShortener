using ErrorOr;
using MediatR;
using URLShortener.Domain;

namespace URLShortener.Application.Features.ShortUrls
{
    internal class GetRedirectUrlQueryHandler : IRequestHandler<GetRedirectUrlQuery, ErrorOr<string>>
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public GetRedirectUrlQueryHandler(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<ErrorOr<string>> Handle(GetRedirectUrlQuery request, CancellationToken cancellationToken)
        {
            var result = await _shortUrlRepository.GetRedirectUrlAsync(request.Path);
            return result;
        }
    }
}
