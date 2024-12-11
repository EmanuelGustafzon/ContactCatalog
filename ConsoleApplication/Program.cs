using ConsoleApplication.Interfaces;
using ConsoleApplication.Services;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IJsonService<IContact>, JsonService<IContact>>();
        services.AddSingleton<IFileService>(new FileService(filePath: "Data"));
        services.AddSingleton<IRepository<IContact>, ContactRepository>();
        services.AddSingleton<IContactService, ContactService>();
        services.AddSingleton<IMenu, MenuService>();
    }).Build();

IMenu menu = host.Services.GetRequiredService<IMenu>();

menu.ViewMenu();








