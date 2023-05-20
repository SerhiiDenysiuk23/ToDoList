using GraphQL;
using GraphQL.Types;
using Repositories;
using Repositories.IRepositories;
using ToDoList.Flags;
using ToDoList.GraphQL.GraphQLTypes;
using ToDoList.Services;

namespace ToDoList.GraphQL.GraphQLQueries
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(RepositoryService service)
        {
            Field<ListGraphType<CategoryType>>("categoryGetList")
                .ResolveAsync(async context =>  {
                    return await service.CategoryRepository(context).GetList();
                });
        }
    }
}
