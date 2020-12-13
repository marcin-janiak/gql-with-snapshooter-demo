using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Query
    {
        public IQueryable<Person> allPeople([Service] PeopleContext context) => context.People;

        public Task<Person> Person([Service] PeopleContext context, Guid id)
        {
            return context.People.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public string Hello([Service] IResolverContext context)
        {
            context.ReportError(new Error("Hello world from error", "001"));

            return "hello";
        }

    }
}