var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure redis
 var conn = Environment.GetEnvironmentVariable("RedisConnection", EnvironmentVariableTarget.Process);
Console.WriteLine($"Connection string: {conn}");
var multiplexer = StackExchange.Redis.ConnectionMultiplexer.Connect(conn);
builder.Services.AddSingleton<StackExchange.Redis.IConnectionMultiplexer>(multiplexer);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// swagger
app.UseSwagger();
app.UseSwaggerUI(o => {
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApp");
    o.RoutePrefix = string.Empty;
});

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
