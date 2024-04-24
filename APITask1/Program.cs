using APITask1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using APITask1.Services;
// using APITask1.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<AesOperation>();  //change
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<EmailService>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
