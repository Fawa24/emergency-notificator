using Core.Domain.Entities;
using Core.Domain.RepositoriesContracts;
using Core.Services;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
	.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();
builder.Services.AddScoped<NotificationRepository>();
builder.Services.AddSingleton<ConnectionFactory>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapControllers();
app.UseAuthorization();

app.Run();
