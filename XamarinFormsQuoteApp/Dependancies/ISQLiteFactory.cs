using System;

namespace QuoteApp.Dependancies
{
    public interface ISQLiteFactory
    {
        SQLite.Net.SQLiteConnection CreateConnection(string dbName);
    }
}

