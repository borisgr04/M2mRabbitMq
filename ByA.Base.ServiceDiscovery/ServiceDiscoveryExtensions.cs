using System;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ByA.Base.ServiceDiscovery
{
    public static class ServiceDiscoveryExtensions
    {
        public static void AddConsulServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceConfig = configuration.GetServiceConfig();

            if (serviceConfig == null)
            {
                throw new ServiceDiscoveryException(nameof(serviceConfig));
            }

            var consulClient = CreateConsulClient(serviceConfig);

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => consulClient);
        }

        private static ConsulClient CreateConsulClient(ServiceConfig serviceConfig)
        {
            return new ConsulClient(config =>
            {
                config.Address = serviceConfig.ServiceDiscoveryAddress;
            });
        }
    }

    [Serializable]
    public class ServiceDiscoveryException : Exception
    {
        public ServiceDiscoveryException() { }
        public ServiceDiscoveryException(string message) : base(message) { }
        public ServiceDiscoveryException(string message, Exception inner) : base(message, inner) { }
        protected ServiceDiscoveryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}