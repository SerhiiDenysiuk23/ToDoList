using GraphQL.Types;
using Repositories.Models;

namespace ToDoList.GraphQL.GraphQLTypes
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
        }
    }
}
