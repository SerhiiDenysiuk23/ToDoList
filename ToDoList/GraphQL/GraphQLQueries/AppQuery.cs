using GraphQL.Types;
using ToDoList.Flags;

namespace ToDoList.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery()
        {
            Field<ToDoQuery>("toDoQuery").ResolveAsync(async _ => new { });
            Field<CategoryQuery>("categoryQuery").ResolveAsync(async _ => new { });

            Field<StringGraphType>("currentDB")
                .Resolve(_ => DBSwitchFlag.Flag.ToString());
        }
    }
}
