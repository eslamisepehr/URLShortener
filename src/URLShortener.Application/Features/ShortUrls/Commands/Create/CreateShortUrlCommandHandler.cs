using ErrorOr;
using MediatR;
using URLShortener.Application.Services;
using URLShortener.Domain;

namespace URLShortener.Application.Features.ShortUrls
{
    internal class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, ErrorOr<ShortUrl>>
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IPathGeneratorService _pathGeneratorService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateShortUrlCommandHandler(
            IShortUrlRepository shortUrlRepository,
            IPathGeneratorService pathGeneratorService,
            IUnitOfWork unitOfWork)
        {
            _shortUrlRepository = shortUrlRepository;
            _pathGeneratorService = pathGeneratorService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<ShortUrl>> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
        {
            var url = request.Url.ToLower();
            var shortUrl = await _shortUrlRepository.GetByUrlAsync(url);
            if (shortUrl.Value is not null)
            {
                return shortUrl;
            }

            var path = await _pathGeneratorService.Create(request.Url);
            if (path.IsError)
            {
                return path.Errors;
            }

            shortUrl = ShortUrl.Create(request.Url, path.Value);
            if (shortUrl.IsError)
            {
                return shortUrl.Errors;
            }

            await _shortUrlRepository.AddAsync(shortUrl.Value);
            await _unitOfWork.CommitAsync(cancellationToken);

            return shortUrl;
        }
    }
}
