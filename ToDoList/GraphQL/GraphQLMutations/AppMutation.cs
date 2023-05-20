using GraphQL;
using GraphQL.Types;
using ToDoList.Enums;
using ToDoList.Flags;

namespace ToDoList.GraphQL.GraphQLMutations
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation()
        {
            Field<ToDoMutation>("toDoMutation")
                .Resolve(_ => new { });
            Field<CategoryMutation>("categoryMutation")
                .Resolve(_ => new { });

            Field<StringGraphType>("ChangeDB")
                .Argument<StringGraphType>("dbSwitchFlag")
                .Resolve(context =>
                {
                    string flag = context.GetArgument<string>("dbSwitchFlag");
                    DBSwitchFlag.Flag = Enum.Parse<DataBases>(flag);
                    return DBSwitchFlag.Flag.ToString();
                });
        }
    }
}
