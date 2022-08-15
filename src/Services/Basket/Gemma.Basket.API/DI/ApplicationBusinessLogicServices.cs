﻿using Gemma.Basket.API.Services;
using Gemma.Basket.API.Services.Interfaces;

namespace Gemma.Basket.API.DI
{
    public static class ApplicationBusinessLogicServices
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
            return services;
        }
    }
}
