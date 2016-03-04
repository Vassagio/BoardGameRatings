using BoardGameRatings.WebSite.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameRatings.WebSite.Tests.Models.Repositories
{
    public class RepositoryFixture
    {
        private readonly ServiceCollection _serviceCollection;

        public RepositoryFixture()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddEntityFramework().AddInMemoryDatabase();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase();            
            ContextOptions = optionsBuilder.Options;
        }

        internal DbContextOptions<ApplicationDbContext> ContextOptions { get; }
        internal ApplicationDbContext Context { get; private set; }

        public void Begin()
        {
            var serviceProvider = _serviceCollection.BuildServiceProvider();
            Context = new ApplicationDbContext(serviceProvider, ContextOptions);
        }

        public void End()
        {
            Context.Dispose();
        }
    }
}