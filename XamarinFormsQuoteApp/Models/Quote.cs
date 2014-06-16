using System;
using SQLite.Net.Attributes;
using PropertyChanged;

namespace QuoteApp.Models
{
    [ImplementPropertyChanged]
    public class Quote
    {
        public Quote ()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int QuoteId {
            get;
            set;
        }

        public string CustomerName {
            get;
            set;
        }

        public string Total {
            get;
            set;
        }
    }
}

