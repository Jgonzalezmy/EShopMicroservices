using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddMediaterR(opt => {
            //opt.RegistrerServicesFromAssembly(Assembly.GetExecutingAssembly());
            //})


            return services;
        }
    }
}
