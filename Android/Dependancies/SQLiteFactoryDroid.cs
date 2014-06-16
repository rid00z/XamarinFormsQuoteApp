using System;
using QuoteApp.Dependancies;
using SQLite.Net.Platform.XamarinAndroid;
using XamarinFormsQuoteApp.Android.Dependancies;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteFactoryDroid))]

namespace XamarinFormsQuoteApp.Android.Dependancies
{
    public class SQLiteFactoryDroid : ISQLiteFactory
    {
        public SQLite.Net.SQLiteConnection CreateConnection (string dbName)
        {
            var path = System.IO.Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), dbName);
            return new SQLite.Net.SQLiteConnection (new SQLitePlatformAndroid (), path);
        }
    }
}

