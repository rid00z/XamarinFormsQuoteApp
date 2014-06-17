using System;
using Xamarin.Forms;
using QuoteApp.PageModels;

namespace QuoteApp.Pages
{
    public class RootContainerPage : MasterDetailPage, IRootNavigation
    {
        ContentPage _menuPage;
        NavigationPage _contactNavPage, _quotesNavPage;

        public RootContainerPage ()
        {
            _contactNavPage = new NavigationPage (BasePageModel.ResolveViewModel<ContactsRootPageModel> (null));
            _quotesNavPage = new NavigationPage (BasePageModel.ResolveViewModel<QuotesRootPageModel> (null));
            Detail = _contactNavPage;

            _menuPage = new ContentPage ();
            _menuPage.Title = "Menu";
            var listView = new ListView();

            listView.ItemsSource = new string[] { "Contacts", "Quotes" };

            listView.ItemSelected += (sender, args) =>
            {
                if ((string)args.SelectedItem == "Contacts")
                	Detail = _contactNavPage;
                if ((string)args.SelectedItem == "Quotes")
                	Detail = _quotesNavPage;

                IsPresented = false;
            };

            _menuPage.Content = listView;
            Master = new NavigationPage(_menuPage) { Title = "Menu" };
        }

        public void PushPage (Page page, BasePageModel model)
        {
            ((NavigationPage)Detail).PushAsync (page);
        }

        public void PopPage ()
        {
            ((NavigationPage)Detail).PopAsync ();
        }
    }
}

