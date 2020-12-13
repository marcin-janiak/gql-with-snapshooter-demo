using HotChocolate;

namespace API
{
    public class Mutation
    {
        public bool AddPerson([Service] PeopleContext context, Person person)
        {
            context.People.Add(person);
            context.SaveChanges();

            return true;
        }
    }
}