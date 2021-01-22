using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Data.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ShoppingCart.Application.AutoMapper;

namespace ShoppingCart.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, string connectionString) {

            services.AddDbContext<ShoppingCartDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsService, ProductService>();

            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICategoriesService, CategoriesService>();

            services.AddScoped<IMembersRepository, MembersRepository>();
            services.AddScoped<IMembersService, MembersService>();

            services.AddScoped<IOrdersRepository,OrdersRepository>();
            services.AddScoped<IOrderService, OrdersService>();

            services.AddScoped<ICartsRepository, CartsRepository>();
            services.AddScoped<ICartsService, CartsService>();

            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            AutoMapperConfiguration.RegisterMappings();
        }
    }
}
