using System;
using System.Threading.Tasks;
using API;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;
using Xunit;

namespace Tests
{
    public class SchemaTests
    {
        private readonly IRequestExecutor _executor;
        private readonly ServiceProvider _serviceProvider;

        public SchemaTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PeopleContext>(options => { options.UseInMemoryDatabase("PeopleContext"); });

            services.AddGraphQLCore();
            
            _serviceProvider = services.BuildServiceProvider();

            _executor = new SchemaBuilder().AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddServices(_serviceProvider)
                .Create()
                .MakeExecutable();
        }

        [Fact]
        public void Schema_ShouldBeCreated()
        {
            _executor.Schema.ToString().MatchSnapshot();
        }

    }
}