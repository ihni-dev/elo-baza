﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EloBaza.WebApi.Extensions
{
    public static class CorsExtensions
    {
        private const string DevelopmentPolicy = "Development";
        private const string ProductionPolicy = "Production";

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(DevelopmentPolicy,
                    builder =>
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });
        }

        public static IApplicationBuilder UseDevelopmentCors(this IApplicationBuilder builder)
        {
            return builder.UseCors(DevelopmentPolicy);
        }
    }
}