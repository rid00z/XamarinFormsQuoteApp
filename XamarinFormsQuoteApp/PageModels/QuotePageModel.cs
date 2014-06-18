using System;
using QuoteApp.Models;
using QuoteApp.Services;
using Xamarin.Forms;

namespace QuoteApp.PageModels
{
    public class QuotePageModel : BasePageModel
    {
        IDatabaseService _databaseService;
        public Quote Quote { get; set; }

        public QuotePageModel (IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void Init(object data)
        {
            Quote = data as Quote;
            if (Quote == null)
                Quote = new Quote ();
        }

        public Command Done
        {
            get {
                return new Command (() => {
                    if (Quote.QuoteId == 0)
                    	_databaseService.Conn.Insert(Quote);
                    else 
                    	_databaseService.Conn.Update(Quote);
                    PopPageModel();
                });
            }
        }
    }
}

