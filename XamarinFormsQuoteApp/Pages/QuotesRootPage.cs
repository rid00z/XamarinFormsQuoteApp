using System;
using Xamarin.Forms;
using QuoteApp.PageModels;

namespace QuoteApp.Pages
{
    public class QuotesRootPage : ContentPage 
    {
        public QuotesRootPageModel ViewModel { get; set; }

        public QuotesRootPage ()
        {
            Title = "Quotes";
        }

        public void Init()
        {
            var listView = new ListView ();

            var template = new DataTemplate (typeof(TextCell));

            template.SetBinding (TextCell.TextProperty, "CustomerName");
            template.SetBinding (TextCell.DetailProperty, "Total");

            listView.ItemTemplate = template;

            listView.SetBinding (ListView.ItemsSourceProperty, "Quotes");
            listView.SetBinding (ListView.SelectedItemProperty, "QuoteSelected");

            ToolbarItems.Add(new ToolbarItem("Add", null, () => {
                ViewModel.AddQuote.Execute(null);
            }));

            Content = listView;
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            ViewModel.Reload ();
        }
    }
}

