using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using QuoteApp.Pages;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace QuoteApp.PageModels
{
    public abstract class BasePageModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public BasePageModel PreviousPageModel { get; set; }

        public virtual void ReverseInit(object value) { }

        protected void PushPageModel<T> () where T : BasePageModel
        {
            PushPageModel<T> (null);
        }

        public static Page ResolvePageModel<T>(Dictionary<string, string> data)
            where T : BasePageModel
        {
            var pageModel = TinyIoC.TinyIoCContainer.Current.Resolve<T>();

            return ResolvePageModel<T>(data, pageModel);
        }

        public static Page ResolvePageModel<T>(object data, BasePageModel pageModel)
            where T : BasePageModel
        {
            var name = typeof(T).Name.Replace ("Model", string.Empty);
            var pageType = Type.GetType ("QuoteApp.Pages." + name);
            var page = (Page)TinyIoC.TinyIoCContainer.Current.Resolve (pageType);

            page.BindingContext = pageModel;

            var initMethod = TinyIoC.TypeExtensions.GetMethod (typeof(T), "Init");
            if (initMethod != null) {
            if (initMethod.GetParameters ().Length > 0) 
            {
            	initMethod.Invoke (pageModel, new object[] { data });
            }
            else 
            	initMethod.Invoke (pageModel, null);
            }

            var vmProperty = TinyIoC.TypeExtensions.GetProperty(pageType, "PageModel");
            if (vmProperty != null)
            vmProperty.SetValue (page, pageModel);

            var vmPageBindingContext = TinyIoC.TypeExtensions.GetProperty(pageType, "BindingContext");
            if (vmPageBindingContext != null)
            vmPageBindingContext.SetValue (page, pageModel);

            var initMethodPage = TinyIoC.TypeExtensions.GetMethod (pageType, "Init"); 
            if (initMethodPage != null)
            initMethodPage.Invoke (page, null);

            return page;
        }

        protected void PushPageModel<T> (object data) where T : BasePageModel
        {
            BasePageModel pageModel = TinyIoC.TinyIoCContainer.Current.Resolve<T>();;

            var page = ResolvePageModel<T> (data, pageModel);

            pageModel.PreviousPageModel = this;

            IRootNavigation rootNav = TinyIoC.TinyIoCContainer.Current.Resolve<IRootNavigation> ();
            rootNav.PushPage (page, pageModel);
        }

        protected void PopPageModel()
        {
            IRootNavigation rootNav = TinyIoC.TinyIoCContainer.Current.Resolve<IRootNavigation> ();
            rootNav.PopPage ();
        }

        protected void PopPageModel(object data)
        {
            if (PreviousPageModel != null && data != null) {
                var initMethod = TinyIoC.TypeExtensions.GetMethod (PreviousPageModel.GetType(), "ReverseInit"); 
                if (initMethod != null) {
                	initMethod.Invoke (PreviousPageModel, new object[] { data });
                }
            }
            IRootNavigation tabbedNav = TinyIoC.TinyIoCContainer.Current.Resolve<IRootNavigation> ();
            tabbedNav.PopPage ();
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
