namespace FlashcardXpApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

            });
            return services;
        }
    }
}
