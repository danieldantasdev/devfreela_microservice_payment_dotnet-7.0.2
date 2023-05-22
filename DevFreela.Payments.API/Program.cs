using DevFreela.Payments.API.Services.Implementations;
using DevFreela.Payments.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Di
builder.Services.AddScoped<IPaymentService, PaymentService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();