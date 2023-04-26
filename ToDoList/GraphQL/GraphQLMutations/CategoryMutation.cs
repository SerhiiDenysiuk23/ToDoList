using GraphQL;
using GraphQL.Types;
using Repositories.IRepositories;
using Repositories.Models;
using ToDoList.GraphQL.GraphQLTypes;

namespace ToDoList.GraphQL.GraphQLMutations
{
    public class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(ICategoryRepository repos)
        {
            Field<CategoryType>("categoryCreate")
                .Argument<CategoryInputType>("category")
                .ResolveAsync(async context =>
                {
                    Category category = context.GetArgument<Category>("category");
                    try
                    {
                        return await repos.Create(category);
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
                        return await repos.Delete(id);
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
