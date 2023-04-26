using GraphQL.Types;
using Repositories.Models;

namespace ToDoList.GraphQL.GraphQLTypes
{
    public class ToDoType : ObjectGraphType<ToDo>
    {
        public ToDoType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Title);
            Field(x => x.Description, nullable: true);
            Field(x => x.DueDate, nullable: true);
            Field(x => x.Status, nullable: true);
            Field(x => x.CategoryId, nullable: true);
            Field(x => x.Category, type: typeof(CategoryType), nullable: true);
        }
    }
}
