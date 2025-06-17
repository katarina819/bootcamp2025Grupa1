using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using MoviesApp.Repository;
using MoviesApp.Repository.Common;
using MoviesApp.Service;
using MoviesApp.Service.Common;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.Register(c => new NpgsqlConnection(connectionString))
        .AsSelf()
        .InstancePerLifetimeScope();
    containerBuilder.RegisterType<MovieService>().As<IMovieService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterAssemblyTypes(typeof(MovieRepository).Assembly)
        .Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
});

// Add services to the container.



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
