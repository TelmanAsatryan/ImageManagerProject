using ImageManager.Entities.ImageStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.DAL.Configurations.ImageStoreConfigurations
{
    public class ImageStoreConfiguration : IEntityTypeConfiguration<ImageStorage>
    {
        public void Configure(EntityTypeBuilder<ImageStorage> builder)
        {
            builder.ToTable("ImageStorege");
            builder.HasKey(h => h.Id);
        }
    }
}
