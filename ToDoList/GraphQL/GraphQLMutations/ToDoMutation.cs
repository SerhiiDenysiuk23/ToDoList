using GraphQL;
using GraphQL.Types;
using Repositories.IRepositories;
using Repositories.Models;
using ToDoList.GraphQL.GraphQLTypes;

namespace ToDoList.GraphQL.GraphQLMutations
{
    public class ToDoMutation : ObjectGraphType
    {
        public ToDoMutation(IToDoRepository repos)
        {
            Field<ToDoType>("toDoCreate")
                .Argument<ToDoInputType>("toDo")
                .ResolveAsync(async context =>
                {
                    ToDo toDo = context.GetArgument<ToDo>("toDo");
                    try
                    {
                        return await repos.Create(toDo);
                    }catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError(ex.Message));
                        return null;
                    }
                });

            Field<ToDoType>("toDoUpdate")
                .Argument<ToDoInputType>("toDo")
                .ResolveAsync(async context =>
                {
                    ToDo toDo = context.GetArgument<ToDo>("toDo");
                    try
                    {
                        return await repos.Update(toDo);
                    }catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError(ex.Message));
                        return null;
                    }
                });

            Field<BooleanGraphType>("toDoDelete")
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
