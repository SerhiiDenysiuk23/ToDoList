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
    public class ToDoMutation : ObjectGraphType
    {
        public ToDoMutation(RepositoryService service)
        {
            Field<ToDoType>("toDoCreate")
                .Argument<ToDoInputType>("toDo")
                .ResolveAsync(async context =>
                {
                    ToDo toDo = context.GetArgument<ToDo>("toDo");

                    try
                    {
                        return await service.ToDoRepository(context).Create(toDo);
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
                        return await service.ToDoRepository(context).Update(toDo);
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
                        return await service.ToDoRepository(context).Delete(id);
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
