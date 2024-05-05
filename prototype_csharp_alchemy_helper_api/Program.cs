using System.Net;
using prototype_csharp_alchemy_helper_domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IMediatorFactory, RequestBasedMediatorFactory>();
builder.Services.AddScoped<IMediator>(provider =>
{
    IHttpContextAccessor httpContextAssessor = provider.GetRequiredService<IHttpContextAccessor>();
    IMediatorFactory mediatorFactory = provider.GetRequiredService<IMediatorFactory>();
    IMediator mediator;

    try
    {
        mediator = mediatorFactory.GetMediator(httpContextAssessor.HttpContext?.Request.Path);
    }
    catch (CouldNotParseUrlPathException badUrlError)
    {
        // Retyrn HTTP 400 Bad Request
        throw new HttpListenerException(400, badUrlError.Message);
    }
    
    return mediator;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.MapControllers();

app.Run();
