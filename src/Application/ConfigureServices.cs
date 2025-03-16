using FlashcardXpApi.Application.Common.Mapper;
using FluentValidation;

namespace FlashcardXpApi.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(Program).Assembly;
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
