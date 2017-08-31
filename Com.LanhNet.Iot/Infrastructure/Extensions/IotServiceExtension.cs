using System;
using Microsoft.Extensions.DependencyInjection;
using Com.LanhNet.Iot.Domain.Model;
using Com.LanhNet.Iot.Domain.Services;
using Com.LanhNet.Iot.Infrastructure.Repositories;
using Com.LanhNet.Iot.Infrastructure.Factories;

namespace Com.LanhNet.Iot.Infrastructure.Extensions
{
    public static class IotServiceExtension
    {
        public static IIotApiService AddIotService(this IServiceCollection services, Action<IotFactory> configFunc)
        {
            IIotRepository repository = new IotRepository();
            IotFactory factory = new IotFactory(repository);                      
            configFunc(factory);
            IotService iotService = new IotService(repository, factory);
            services.AddSingleton<IIotApiService>(iotService);
            services.AddSingleton<IIotManageService>(iotService);
            return iotService;
        }
    }
}
