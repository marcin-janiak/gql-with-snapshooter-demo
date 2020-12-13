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
    public class PeopleTests
    {
        private readonly IRequestExecutor _executor;
        private readonly ServiceProvider _serviceProvider;

        public PeopleTests()
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
        public async Task Test()
        {
            var context = _serviceProvider.GetService<PeopleContext>()!;
            context.Add(new Person
                {
                    Id = new Guid("553A403C-CD68-4C7D-A2EC-FEA6428CB7C9"),
                    Name = "Test name"
                });
            
            await context.SaveChangesAsync();

            IReadOnlyQueryRequest request =
                QueryRequestBuilder.New()
                    .SetQuery(
                        @"
query getPeople {
  allPeople {
    id
    name
    supervisorId
  	supervisor {
      id
      name
      supervisorId
    }
  }
}")
                    .SetServices(_serviceProvider)
                    .Create();


            var result = await _executor.ExecuteAsync(request);
            
            result.MatchSnapshot();
        }
    }
}