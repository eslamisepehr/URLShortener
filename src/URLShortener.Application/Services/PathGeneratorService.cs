using System.Security.Cryptography;
using ErrorOr;

namespace URLShortener.Application.Services
{
    internal class PathGeneratorService : IPathGeneratorService
    {
        private const string ConversionCode = "FjTG0s5dgWkbLf_8etOZqMzNhmp7u6lUJoXIDiQB9-wRxCKyrPcv4En3Y21aASHV";
        private const int MinVanityCodeLength = 5;

        public async Task<ErrorOr<string>> Create(string url)
        {
            var number = ConvertUrlToNumber(url);
            var path = GenerateUniqueRandomToken(number);
            return path;
        }

        private int ConvertUrlToNumber(string url)
        {
            var sum = 0;
            foreach (var @char in url)
            {
                sum += (int)@char;
            }
            return sum;
        }

        private string GenerateUniqueRandomToken(int uniqueId)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var bytes = new byte[MinVanityCodeLength];
                generator.GetBytes(bytes);
                var chars = bytes.Select(b => ConversionCode[b % ConversionCode.Length]);
                var token = new string(chars.ToArray());
                var reversedToken = string.Join(string.Empty, token.Reverse());
                return uniqueId + reversedToken;
            }
        }
    }
}
