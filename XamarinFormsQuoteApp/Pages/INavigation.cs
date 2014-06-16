using System;
using Xamarin.Forms;
using QuoteApp.PageModels;

namespace QuoteApp.Pages
{
    public interface IRootNavigation 
    {
        void PushPage(Page page, BasePageModel model);
        void PopPage();
    }
}

