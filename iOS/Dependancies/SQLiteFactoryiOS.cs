using System;
using QuoteApp.Dependancies;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using XamarinFormsQuoteApp.iOS.Dependancies;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteFactoryiOS))]

namespace XamarinFormsQuoteApp.iOS.Dependancies
{
    public class SQLiteFactoryiOS : ISQLiteFactory 
    {
        public SQLiteConnection CreateConnection (string dbName)
        {
            var path = System.IO.Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), dbName);
            return new SQLite.Net.SQLiteConnection (new SQLitePlatformIOS (), path);
        }
    }
}

