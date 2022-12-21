using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Services;

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
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddControllersWithViews().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddScoped<IVnPayService, VnPayService>();

builder.Services.AddScoped<IMailService, MailService>();



//var emailConfig = Configuration
 //       .GetSection("MailSettings")
  //      .Get<MailSettings>();
//builder.Services.AddSingleton(emailConfig);


builder.Services.AddControllers();


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
string StrConnection = "server=26.54.3.122;user=root;database=twenti;port=3306;password=";
MySqlConnection connection = new MySqlConnection(StrConnection);
try
{
    connection.Open();
    if (connection.State == ConnectionState.Open)
        Console.WriteLine("Connection opened successfully!");
}
catch
{
    Console.WriteLine("have error");
}
app.Run();