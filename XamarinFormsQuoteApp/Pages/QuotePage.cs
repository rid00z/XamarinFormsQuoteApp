using System;
using Xamarin.Forms;
using QuoteApp.PageModels;

namespace QuoteApp.Pages
{
    public class QuotePage : ContentPage
    {
        public QuotePageModel PageModel { get; set; }

        public QuotePage ()
        {
        }

        public void Init()
        {
            Title = "Quote";

            var customerName = new Entry ();
            customerName.SetBinding (Entry.TextProperty, "Quote.CustomerName");
            customerName.Placeholder = "Customer Name";

            var total = new Entry ();
            total.SetBinding (Entry.TextProperty, "Quote.Total");
            total.Placeholder = "Total";

            var stacker = new StackLayout ();
            stacker.Children.Add (customerName);
            stacker.Children.Add (total);

            Content = stacker;

            ToolbarItems.Add(new ToolbarItem("Done", null, () => {
                PageModel.Done.Execute(null);
            }));

        }
    }
}

