using System;
using QuoteApp.Dependancies;
using SQLite.Net.Platform.Generic;

namespace QuoteApp.Tests.Dependancies
{
    public class SQLiteFactory : ISQLiteFactory
    {
        public SQLite.Net.SQLiteConnection CreateConnection (string dbName)
        {
            return new SQLite.Net.SQLiteConnection (new SQLitePlatformGeneric (), "test.db");
        }

        public SQLiteFactory ()
        {
        }
    }
}

