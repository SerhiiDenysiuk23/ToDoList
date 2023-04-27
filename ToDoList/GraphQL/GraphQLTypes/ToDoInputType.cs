using GraphQL.Types;
using Repositories.Models;

namespace ToDoList.GraphQL.GraphQLTypes
{
    public class ToDoInputType : InputObjectGraphType<ToDo>
    {
        public ToDoInputType()
        {
            Name = "ToDoInput";
            Field<IdGraphType>("id");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("description");
            Field<DateTimeGraphType>("dueDate");
            Field<StringGraphType>("status");
            Field<IntGraphType>("categoryId");
        }
    }
}
