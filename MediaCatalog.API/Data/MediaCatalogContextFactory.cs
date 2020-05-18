using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MediaCatalog.API.Data
{
    public class MediaCatalogContextFactory
    {
        public MediaCatalogContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new MediaCatalogContext(new DbContextOptionsBuilder<MediaCatalogContext>().Options, config);
        }
    }
}