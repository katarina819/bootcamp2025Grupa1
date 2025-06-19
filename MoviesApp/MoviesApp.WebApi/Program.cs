using Autofac;
using Autofac.Extensions.DependencyInjection;
using MoviesApp.Repository;
using MoviesApp.Repository.Common;
using MoviesApp.Service;
using MoviesApp.Service.Common;
using MoviesAppRepository;
using MoviesAppService;
using MoviesApp.Mapping;
using Npgsql;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Use Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Autofac registrations
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.Register(c => new NpgsqlConnection(connectionString))
        .AsSelf()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterType<DirectorService>().As<IDirectorService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<DirectorRepository>().As<IDirectorRepository>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<MovieService>().As<IMovieService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerLifetimeScope();
    
    containerBuilder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
    
    containerBuilder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<LanguageRepository>().As<ILanguageRepository>().InstancePerLifetimeScope();

    containerBuilder.RegisterAssemblyTypes(typeof(MovieRepository).Assembly)
        .Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
});

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(DirectorProfile).Assembly);

// Add controllers & swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
