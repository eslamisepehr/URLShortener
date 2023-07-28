// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortener.Domain;

namespace URLShortener.Infrastructure.Domain
{
    internal class ShortUrlEntityTypeConfiguration : IEntityTypeConfiguration<ShortUrl>
    {
        public void Configure(EntityTypeBuilder<ShortUrl> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Url)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.Path)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
