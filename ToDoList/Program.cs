using GraphQL.Server;
using GraphQL.MicrosoftDI;
using Repositories.MSSQLRepositories;
using Repositories.XMLRepositories;
using ToDoList.Flags;
using ToDoList.Enums;
using Repositories.IRepositories;
using GraphQL;
using ToDoList.GraphQL.GraphQLSchema;
using GraphQL.Types;
using Repositories;
using ToDoList.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

string conn = builder.Configuration.GetConnectionString("MSSQLConnString");
string path = builder.Configuration.GetConnectionString("XMLPath");


builder.Services.AddSingleton<RepositoryFactory>(new RepositoryFactory(conn, path));

builder.Services.AddSingleton<RepositoryService>(new RepositoryService(conn, path));


//builder.Services.AddTransient<IToDoRepository>(provider => DBSwitchFlag.Flag switch
//    {
//        DataBases.SQL => new MSSQLToDoRepository(conn),
//        DataBases.XML => new XMLToDoRepository(path),
//        _ => new MSSQLToDoRepository(conn)
//    }
//    );

//builder.Services.AddTransient<ICategoryRepository>(provider => DBSwitchFlag.Flag switch
//    {
//        DataBases.SQL => new MSSQLCategoryRepository(conn),
//        DataBases.XML => new XMLCategoryRepository(path),
//        _ => new MSSQLCategoryRepository(conn)
//    }
//    );

builder.Services.AddCors(
    builder => {
        builder.AddPolicy("DefaultPolicy", option =>
        {
            option.AllowAnyMethod();
            option.AllowAnyOrigin();
            option.AllowAnyHeader();
        });
    }
);

builder.Services.AddGraphQL(config => config
                .AddSystemTextJson()
                .AddErrorInfoProvider(options => options.ExposeExceptionStackTrace = true)
                .AddSchema<AppSchema>()
                .AddGraphTypes(typeof(AppSchema).Assembly)
                );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("DefaultPolicy");

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDo}/{action=Index}/{id?}");

app.UseGraphQL<ISchema>();

app.UseGraphQLAltair();

app.Run();
