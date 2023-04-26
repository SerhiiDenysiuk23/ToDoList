using GraphQL.Types;
using ToDoList.GraphQL.GraphQLMutations;
using ToDoList.GraphQL.GraphQLQueries;

namespace ToDoList.GraphQL.GraphQLSchema
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<AppQuery>();
            Mutation = provider.GetRequiredService<AppMutation>();
        }
    }
}
