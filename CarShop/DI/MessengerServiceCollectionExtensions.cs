using CarShop.Interfaces;

namespace CarShop.DI
{
    public static class MessengerServiceCollectionExtensions
    {
        public static IServiceCollection AddMessanger(this IServiceCollection services)
        {
            services.AddScoped<IMessanger, Messanger>();
            services.AddTransient<MessageFactory>();
            services.AddScoped<SmtpMailClient>();
            services.AddScoped<SmtpSettings>(provider =>
            {
                return new SmtpSettings() { Host = "smtp.gmail.com", Port = 587, Login = "turchakkonstantin@gmail.com", Password = "vkeq xoaw grle npwy" };
            });

            return services;
        }
    }
}
