using System;
using SQLite.Net.Attributes;
using PropertyChanged;

namespace QuoteApp.Models
{
    [ImplementPropertyChanged]
    public class Contact
    {
        public Contact ()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ContactId {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public string Phone {
            get;
            set;
        }
    }
}

