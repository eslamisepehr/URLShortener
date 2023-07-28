﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using URLShortener.Application.Features.ShortUrls;
using URLShortener.Contracts;
using URLShortener.Domain;

namespace URLShortener.API.Controllers
{
    public class ShortUrlController : APIController
    {
        private readonly IMediator _mediator;

        public ShortUrlController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/{path}")]
        public async Task<IActionResult> RedirectToPath(string path)
        {
            var result = await _mediator.Send(new GetRedirectUrlQuery(path));
            return result.Match(
                value => Redirect(value),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("/Create")]
        [ProducesDefaultResponseType(typeof(ShortUrl))]
        public async Task<IActionResult> CreateShortUrl(CreateShortUrlRequest request)
        {
            var result = await _mediator.Send(new CreateShortUrlCommand(request.Url));
            return result.Match(
                value => Ok(value),
                errors => Problem(errors));
        }
    }
}
