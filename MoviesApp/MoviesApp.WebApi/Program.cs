using Autofac;
using Autofac.Extensions.DependencyInjection;
using MoviesAppRepository;
using MoviesAppService;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    // Registracije za repozitorije i servise
    containerBuilder.RegisterType<GenreRepository>()
    .As<IGenreRepository>()       // registrira kao interfejs
    .WithParameter("connectionString", connectionString)
    .InstancePerLifetimeScope();


    containerBuilder.RegisterType<GenreService>()
    .As<IGenreService>()          // registrira kao interfejs
    .InstancePerLifetimeScope();

    containerBuilder.RegisterType<MovieRepository>()
                    .WithParameter("connectionString", connectionString)
                    .InstancePerLifetimeScope();

    containerBuilder.RegisterType<MovieService>()
                    .InstancePerLifetimeScope();
});


// Dodaj ostale servise
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
