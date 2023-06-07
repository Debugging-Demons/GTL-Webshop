using MediatR;
using Webshop.Order.Application;
using Webshop.Order.Application.Contracts;
using Webshop.Order.Persistence;
using AutoMapper;
using System.Reflection;
using Webshop.Order.Application.Mapper;
using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // my services
        builder.Services.AddSingleton<Container<PurchaseOrder>, Container<PurchaseOrder>>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IDataContext, DataContext>();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Dispatcher).Assembly));
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        builder.Services.AddScoped<IDispatcher>(sp => new Dispatcher(sp.GetService<IMediator>()!));

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
    }
}