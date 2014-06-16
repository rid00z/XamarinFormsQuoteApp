using System;
using Xamarin.Forms;
using QuoteApp.PageModels;

namespace QuoteApp.Pages
{
	public class ContactsRootPage : ContentPage
	{
        public ContactsRootPageModel ViewModel { get; set; }

		public ContactsRootPage ()
		{
			Title = "Contacts";

            var list = new ListView ();

            var cellTemplate = new DataTemplate (typeof(TextCell));

            cellTemplate.SetBinding (TextCell.DetailProperty, "Phone");
            cellTemplate.SetBinding (TextCell.TextProperty, "Name");

            list.ItemTemplate = cellTemplate;

            list.SetBinding (ListView.ItemsSourceProperty, "Contacts");
            list.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => ViewModel.ContactSelected.Execute (e.SelectedItem);

            ToolbarItems.Add(new ToolbarItem("Add", null, () => {
                ViewModel.AddContact.Execute(null);
            }));

            Content = list;
		}

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            ViewModel.Reload ();
        }
	}
}

