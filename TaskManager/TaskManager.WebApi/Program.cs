using Microsoft.EntityFrameworkCore;
using TaskManager.BusinessLogic.Services.Authentication;
using TaskManager.BusinessLogic.Services.Tasks;
using TaskManager.BusinessLogic.Services.Users;
using TaskManager.DataAccess;
using TaskManager.IBusinessLogic;
using TaskManager.IDataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DbContext, DataBaseContext>();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<DataBaseContext>(options =>
               options.UseNpgsql(configuration.GetConnectionString("BBDD_Connection_String")));

builder.Services.AddScoped<IAuthenticationDataAccess, AuthenticationDataAccess>();
builder.Services.AddScoped<ITodoTaskDataAccess, TodoTaskDataAccess>();
builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILogoutService, LogoutService>();
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(c =>
{
    c.AddPolicy("CORSmystore", options =>
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORSmystore");

app.UseAuthorization();

app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();
