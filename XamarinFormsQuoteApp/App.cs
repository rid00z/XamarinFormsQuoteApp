using System;
using Xamarin.Forms;
using QuoteApp.Pages;
using QuoteApp.Services;

namespace QuoteApp
{
    public class App
    {
        public static Page GetMainPage ()
        {	
            TinyIoC.TinyIoCContainer.Current.Register<IDatabaseService, DatabaseService> ();

            var containerPage = new RootContainerPage ();
            TinyIoC.TinyIoCContainer.Current.Register<IRootNavigation> (containerPage);
            return containerPage;
        }
    }
}

