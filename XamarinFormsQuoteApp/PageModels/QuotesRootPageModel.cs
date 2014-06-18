using System;
using QuoteApp.Models;
using System.Collections.ObjectModel;
using QuoteApp.Services;
using Xamarin.Forms;

namespace QuoteApp.PageModels
{
    public class QuotesRootPageModel : BasePageModel
    {
        IDatabaseService _databaseService;

        public QuotesRootPageModel (IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public ObservableCollection<Quote> Quotes { get; set; }

        public void Reload()
        {
            if (Quotes == null)
                Quotes = new ObservableCollection<Quote> (_databaseService.Conn.Table<Quote> ());
            else 
            {
                Quotes.Clear ();
                foreach (var quote in _databaseService.Conn.Table<Quote>())
                    Quotes.Add (quote);
            }
        }

        public Command AddQuote {
            get {
                return new Command (() => {
                    PushPageModel<QuotePageModel>();
                });
            }
        }

        public Command<Quote> QuoteSelected {
            get {
                return new Command<Quote> ((quote) => {
                    PushPageModel<QuotePageModel>(quote);
                });
            }
        }
    }
}

