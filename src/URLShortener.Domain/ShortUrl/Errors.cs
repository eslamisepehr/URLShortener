using ErrorOr;

namespace URLShortener.Domain
{
    public static class Errors
    {
        public static class ShortUrl
        {
            public static Error InvalidUrl => Error.Validation(
                code: $"{nameof(ShortUrl)}.{nameof(InvalidUrl)}",
                description: "Url must be valid in correct format.");

            public static Error InvalidShortUrl => Error.Validation(
                code: $"{nameof(ShortUrl)}.{nameof(InvalidShortUrl)}",
                description: "Short Url must be valid in correct format.");
        }
    }
}
