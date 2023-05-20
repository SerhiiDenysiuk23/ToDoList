using GraphQL;
using GraphQL.Types;
using Repositories;
using Repositories.IRepositories;
using Repositories.Models;
using ToDoList.Flags;
using ToDoList.GraphQL.GraphQLTypes;
using ToDoList.Services;

namespace ToDoList.GraphQL.GraphQLMutations
{
    public class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(RepositoryService service)
        {
            Field<CategoryType>("categoryCreate")
                .Argument<CategoryInputType>("category")
                .ResolveAsync(async context =>
                {
                    Category category = context.GetArgument<Category>("category");
                    try
                    {
                        return await service.CategoryRepository(context).Create(category);
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError(ex.Message));
                        return null;
                    }
                });

            Field<BooleanGraphType>("categoryDelete")
                .Argument<IdGraphType>("id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("id");
                    try
                    {
                        return await service.CategoryRepository(context).Delete(id);
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError(ex.Message));
                        return null;
                    }
                });
        }
    }
}
