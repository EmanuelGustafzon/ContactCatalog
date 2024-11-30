using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using System.Collections.ObjectModel;

namespace MauiApplication
{
    public partial class MainPage : ContentPage
    {
        IContactService _contactService;

        public MainPage(IContactService contactService)
        {
            InitializeComponent();
            _contactService = contactService;
            _contactService.GetAll();
            BindingContext = this;
            AddContact();
            
        }
        public void AddContact()
        {
        }
    }

}
