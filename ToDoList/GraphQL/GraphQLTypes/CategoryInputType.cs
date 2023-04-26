using GraphQL.Types;
using Repositories.Models;

namespace ToDoList.GraphQL.GraphQLTypes
{
    public class CategoryInputType : InputObjectGraphType<Category>
    {
        public CategoryInputType()
        {
            Name = "CategoryInput";
            Field<IdGraphType>("id");
            Field<StringGraphType>("name");
        }
    }
}
