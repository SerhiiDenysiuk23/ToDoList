
using Repositories.MSSQLRepositories;
using Repositories.XMLRepositories;
using ToDoList.Flags;
using ToDoList.Enums;
using Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string conn = builder.Configuration.GetConnectionString("MSSQLConnString");
string path = builder.Configuration.GetConnectionString("XMLPath");


builder.Services.AddScoped<IToDoRepository>(provider => DBSwitchFlag.Flag switch
    {
        DataBases.SQL => new MSSQLToDoRepository(conn),
        DataBases.XML => new XMLToDoRepository(path),
        _ => new MSSQLToDoRepository(conn)
    }
    );

builder.Services.AddScoped<ICategoryRepository>(provider => DBSwitchFlag.Flag switch
    {
        DataBases.SQL => new MSSQLCategoryRepository(conn),
        DataBases.XML => new XMLCategoryRepository(path),
        _ => new MSSQLCategoryRepository(conn)
    }
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDo}/{action=Index}/{id?}");

app.Run();
