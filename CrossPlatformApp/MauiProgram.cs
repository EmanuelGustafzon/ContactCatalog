using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace CrossPlatformApp
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton<IFileService, FileService>(x => new FileService(@"C:\Users\Emanuel"));
            builder.Services.AddSingleton<IJsonService<IContact>, JsonService<IContact>>();
            builder.Services.AddSingleton<IRepository<IContact>, ContactRepository>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<ISearchContactsService, SearchContactsService>();


#endif

            return builder.Build();
        }
    }
}
