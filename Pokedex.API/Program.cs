using Pokedex.Aplication.UseCases.Pokedex.GetRegion;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddTransient<GetRegionUseCase>();
builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolicy",
        policy => {
            policy.WithOrigins("http://localhost:5173", "https://localhost:5173/")
            .AllowAnyHeader()
            .AllowAnyMethod();

        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
