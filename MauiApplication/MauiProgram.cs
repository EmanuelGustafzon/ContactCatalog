using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace MauiApplication
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
            builder.Services.AddSingleton<IJsonService<IContact>, JsonService<IContact>>();
            builder.Services.AddSingleton<IFileService, FileService>(provider => 
                new FileService(@"C:\Users\Emanuel"));
            builder.Services.AddSingleton<IRepository<IContact>, ContactRepository>();
            builder.Services.AddSingleton<IContactService, ContactService>();
#endif

            return builder.Build();
        }
    }
}
