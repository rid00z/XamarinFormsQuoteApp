using System;
using SQLite.Net;

namespace QuoteApp.Services
{
	public interface IDatabaseService
	{
        SQLiteConnection Conn { get; }
	}

}

