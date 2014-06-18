using System;
using QuoteApp.Services;
using QuoteApp.Models;
using Xamarin.Forms;

namespace QuoteApp.PageModels
{
    public class ContactPageModel : BasePageModel
    {
        IDatabaseService _dataService;

        public ContactPageModel (IDatabaseService dataService)
        {
            _dataService = dataService;
        }

        public Contact Contact { get; set; }

        public void Init(object data)
        {
            if (data != null) {
                Contact = (Contact)data;
            } else {
                Contact = new Contact ();
            }
        }

        public Command Done
        {
            get 
            { 
                return new Command (() =>
                    {
                        if (Contact.ContactId == 0)
                            _dataService.Conn.Insert(Contact);
                        else 
                            _dataService.Conn.Update(Contact);
                        PopPageModel();
                    }
                );
            }
        }
    }
}

