using GraphQL;
using GraphQL.Types;
using Repositories;
using Repositories.IRepositories;
using Repositories.Models;
using ToDoList.Flags;
using ToDoList.GraphQL.GraphQLTypes;
using ToDoList.Services;

namespace ToDoList.GraphQL.GraphQLQueries
{
    public class ToDoQuery : ObjectGraphType
    {
        public ToDoQuery(RepositoryService service)
        {
            Field<ListGraphType<ToDoType>>("toDoGetList")
                .ResolveAsync(async context =>
                {
                    var repository = service.ToDoRepository(context);
                    var a = repository;
                    return await repository.GetList();
                });
        }
    }
}
