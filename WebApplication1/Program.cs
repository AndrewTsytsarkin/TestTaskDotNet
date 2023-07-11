using Microsoft.EntityFrameworkCore;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("PersonContext");
builder.Services.AddDbContext<PersonDbContext>(options =>options.UseNpgsql(connection));

services.AddTransient<AbstractPersonService,PersonService >();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

}
using (var scope = app.Services.CreateScope())
{
    var appServices = scope.ServiceProvider;

    var context = appServices.GetRequiredService<PersonDbContext>();
    // закоментировать следующие три строки после первого запуска приложения( или раскоментировать при первом)
    //context.Database.EnsureDeleted();
    //var res = context.Database.EnsureCreated();
    //DbInitializer.Initialize(context);
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class DbInitializer
{
    public static void Initialize(PersonDbContext context)
    {
        // Look for any students.
        if (context.Persons.Any())
        {
            return;
        }
        var persons = new PersonEntity[]
        {
                new(){FirstName="Carson",SecondName="Alexander",Position="CEO" },
                new  (){FirstName="Meredith",SecondName="Alonso" ,Position="Lead"},              

        };

        context.Persons.AddRange(persons);
        context.SaveChanges();
    }
}