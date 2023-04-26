using GraphQL.Types;
using Repositories.IRepositories;
using ToDoList.GraphQL.GraphQLTypes;

namespace ToDoList.GraphQL.GraphQLQueries
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(ICategoryRepository repos)
        {
            Field<ListGraphType<CategoryType>>("categoryGetList")
                .ResolveAsync(async context => await repos.GetList());
        }
    }
}
