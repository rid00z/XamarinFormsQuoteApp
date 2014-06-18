using System;
using Xamarin.Forms;
using QuoteApp.PageModels;

namespace QuoteApp.Pages
{
    public class ContactPage : ContentPage 
    {
        public ContactPageModel PageModel { get; set; }

        public ContactPage ()
        {
            Title = "Contact";

            var name = new Entry();
            name.Placeholder = "Name";
            name.SetBinding (Entry.TextProperty, "Contact.Name");

            var phone = new Entry ();
            phone.Placeholder = "Phone";
            phone.SetBinding (Entry.TextProperty, "Contact.Phone");

            var stacker = new StackLayout ();
            stacker.Children.Add (name);
            stacker.Children.Add (phone);

            Content = stacker;

            ToolbarItems.Add(new ToolbarItem("Done", null, () => {
                PageModel.Done.Execute(null);
            }));
        }
    }
}

