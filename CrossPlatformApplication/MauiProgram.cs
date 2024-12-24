using CrossPlatformApplication.Pages;
using CrossPlatformApplication.ViewModels;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace CrossPlatformApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    //fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                });
            builder.Services.AddSingleton<IJsonService<IContact>, JsonService<IContact>>();
            builder.Services.AddSingleton<IFileService>(new FileService(FileSystem.AppDataDirectory));
            builder.Services.AddSingleton<IRepository<IContact>, ContactRepository>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<ISearchContactsService, SearchContactsService>();
            builder.Services.AddSingleton<ContactsCollectionViewModel>();
            builder.Services.AddSingleton<ContactsViewModel>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<CreateContactViewModel>();
            builder.Services.AddSingleton<CreateContact>();
            builder.Services.AddSingleton<SearchContactsViewModel>();
            builder.Services.AddSingleton<SearchContacts>();
            builder.Services.AddTransient<EditContactViewModel>();
            builder.Services.AddTransient<EditContact>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
