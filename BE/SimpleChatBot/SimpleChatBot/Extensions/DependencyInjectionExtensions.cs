using SimpleChatBot.Business.Boundaries.Accounts;
using SimpleChatBot.Business.Boundaries.Chatbots;
using SimpleChatBot.Business.Interactors.Accounts;
using SimpleChatBot.Business.Interactors.Chatbots;
using SimpleChatBot.Business.Services;
using SimpleChatBot.Business.Services.Interfaces;
using SimpleChatBot.Databases.Repositories;
using SimpleChatBot.Databases.Repositories.Interfaces;

namespace SimpleChatBot.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection DependencyGroup(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ISendMailRepository, SendMailRepository>();

            //Services
            services.AddScoped<IHelperService, HelperService>();
            services.AddScoped<IEncodeService, EncodeService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IChatBotService, ChatbotService>();

            //Account Interactors
            services.AddScoped<IKeyActiveInteractor, KeyActiveInteractor>();
            services.AddScoped<ICheckSpamGetKeyInteractor, CheckSpamGetKeyInteractor>();
            services.AddScoped<ILoginInteractor, LoginInteractor>();

            //Chatbot Interactors
            services.AddScoped<ISendMessageInteractor, SendMessageInteractor>();


            return services;
        }
    }
}
