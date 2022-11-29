using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using TwentiBeauti_BackEnd_DotNet.Data;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {

            //you can configure your custom policy
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//string StrConnection = "server=localhost;user=root;database=twenti;port=3306;password=";
//MySqlConnection connection = new MySqlConnection(StrConnection);
//try
//{
//    connection.Open();
//    if (connection.State == ConnectionState.Open)
//        Console.WriteLine("Connection opened successfully!");
//}
//catch
//{
//    Console.WriteLine("have error");
//}
app.Run();
