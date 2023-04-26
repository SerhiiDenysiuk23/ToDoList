using GraphQL;
using GraphQL.Types;
using Repositories.IRepositories;
using Repositories.Models;
using ToDoList.GraphQL.GraphQLTypes;

namespace ToDoList.GraphQL.GraphQLQueries
{
    public class ToDoQuery : ObjectGraphType
    {
        public ToDoQuery(IToDoRepository repos)
        {
            Field<ListGraphType<ToDoType>>("toDoGetList")
                .ResolveAsync(async context => await repos.GetList());
        }
    }
}
