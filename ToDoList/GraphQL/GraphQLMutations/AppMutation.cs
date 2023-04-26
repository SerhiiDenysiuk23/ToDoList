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
                .Argument<EnumerationGraphType<DataBases>>("dbSwitchFlag")
                .Resolve(context =>
                {
                    DataBases flag = context.GetArgument<DataBases>("dbSwitchFlag");
                    DBSwitchFlag.Flag = flag;
                    return DBSwitchFlag.Flag.ToString();
                });
        }
    }
}
