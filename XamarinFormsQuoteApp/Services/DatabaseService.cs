using System;
using QuoteApp.Dependancies;
using QuoteApp.Models;
using SQLite.Net;

namespace QuoteApp.Services
{
    public class DatabaseService : IDatabaseService 
    {
        SQLite.Net.SQLiteConnection _connection;

        public DatabaseService() :
            this(Xamarin.Forms.DependencyService.Get<ISQLiteFactory>())
        {
        }

        public DatabaseService(ISQLiteFactory factory)
        {
            _connection = factory.CreateConnection("quote.db");
            Setup ();
        }

        void Setup()
        {
            _connection.CreateTable<Contact> ();
            _connection.CreateTable<Quote> ();
        }

        public SQLiteConnection Conn
        {
            get { return _connection; }
        }

    }
}

