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

var builder = WebApplication.CreateBuilder(args);

// Kestrel konfiguracija za HTTP na portu 5174
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenLocalhost(7123); // HTTP na localhost:5174
//});

// Use Autofac kao DI container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Connection string iz appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Autofac registracije servisa i repozitorija
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

// AutoMapper
builder.Services.AddAutoMapper(typeof(DirectorProfile).Assembly);

// Controllers i Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS - dopuštaj pristup sa svih lokacija (možeš ogranièiti na React app URL)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5174")  // ovdje port React appa
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp"); // Omoguæi CORS samo za React app

app.UseHttpsRedirection(); // ako koristiš HTTPS na backendu
app.UseAuthorization();

app.MapControllers();

app.Run();
