using LeoMongo;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;
using MongoDBDemoApp.Core.Workloads.Posts;

namespace MongoDBDemoApp;

public class Startup
{
    private const string Origin = "_allowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AppSettings>(Configuration.GetSection(AppSettings.Key));
        services.AddAutoMapper(typeof(MapperProfile));

        // configure fwk
        services.AddLeoMongo<MongoConfig>();

        // for bigger assemblies it would be alright to register those via reflection by naming convention!
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICommentService, CommentService>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddControllers();

        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy(Origin,
                builder =>
                {
                    builder.WithOrigins("http://localhost:5000",
                            "http://localhost:4200") // Angular CLI
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ErrorLoggingMiddleware>();

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(Origin);

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}

public sealed class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}