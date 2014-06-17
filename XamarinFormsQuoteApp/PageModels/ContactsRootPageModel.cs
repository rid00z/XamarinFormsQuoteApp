using System;
using System.Collections.ObjectModel;
using QuoteApp.Models;
using QuoteApp.Services;
using Xamarin.Forms;
using System.Windows.Input;

namespace QuoteApp.PageModels
{
    public class ContactsRootPageModel : BasePageModel
    {
        IDatabaseService _databaseService;

        public ContactsRootPageModel (IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public void Init()
        {
        }

        public void Reload ()
        {
            if (Contacts == null)
                Contacts = new ObservableCollection<Contact> (_databaseService.Conn.Table<Contact> ());
            else {
                //TODO: something smarter
                Contacts.Clear ();
                foreach (var cont in _databaseService.Conn.Table<Contact>()) {
                	Contacts.Add (cont);
                }
            }
        }

        public Command AddContact
        {
            get {
                return new Command(() => {
                    PushViewModel<ContactPageModel>();
                });
            }
        }

        public Command<Contact> ContactSelected
        {
            get {
                return new Command<Contact>((contact) => {
                    PushViewModel<ContactPageModel>(contact);
                });
            }
        }
    }
}

