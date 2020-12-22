using System;
using Microsoft.Extensions.DependencyInjection;

namespace NeoTalent.Infrastructure
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            // services.AddScoped(typeof(EmailService));

            services.AddScoped<ISugestionsService, SugestionsService>();

            services.AddScoped<IItemsService, ItemsService>();
            services.AddScoped<IDiscountCodesService, DiscountCodesService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;

        }
    }
}