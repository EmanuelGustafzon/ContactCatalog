using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using System.Collections.ObjectModel;

namespace MauiApplication
{
    public partial class MainPage : ContentPage
    {
        IContactService _contactService;
        public ObservableCollection<IContact> Contacts => _contactService.GetObservableList();

        public MainPage(IContactService contactService)
        {
            InitializeComponent();
            _contactService = contactService;
            _contactService.GetAll();
            var list = _contactService.GetObservableList();
            list.Add()
            BindingContext = this;
            
        }
    }

}
