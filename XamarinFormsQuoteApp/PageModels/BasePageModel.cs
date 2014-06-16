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
        public BasePageModel PreviousViewModel { get; set; }

		public virtual void ReverseInit(object value) { }

		protected void PushViewModel<T> () where T : BasePageModel
		{
			PushViewModel<T> (null);
		}

		public static Page ResolveViewModel<T>(Dictionary<string, string> data)
			where T : BasePageModel
		{
            var viewModel = TinyIoC.TinyIoCContainer.Current.Resolve<T>();

			return ResolveViewModel<T>(data, viewModel);
		}

        public static Page ResolveViewModel<T>(object data, BasePageModel viewModel)
			where T : BasePageModel
		{
			var name = typeof(T).Name.Replace ("Model", string.Empty);
            var pageType = Type.GetType ("QuoteApp.Pages." + name);
            var page = (Page)TinyIoC.TinyIoCContainer.Current.Resolve (pageType);

			page.BindingContext = viewModel;

			var initMethod = TinyIoC.TypeExtensions.GetMethod (typeof(T), "Init");
			if (initMethod != null) {
				if (initMethod.GetParameters ().Length > 0) 
				{
					initMethod.Invoke (viewModel, new object[] { data });
				}
				else 
					initMethod.Invoke (viewModel, null);
			}

			var vmProperty = TinyIoC.TypeExtensions.GetProperty(pageType, "ViewModel");
			if (vmProperty != null)
				vmProperty.SetValue (page, viewModel);

			var vmPageBindingContext = TinyIoC.TypeExtensions.GetProperty(pageType, "BindingContext");
			if (vmPageBindingContext != null)
				vmPageBindingContext.SetValue (page, viewModel);

			var initMethodPage = TinyIoC.TypeExtensions.GetMethod (pageType, "Init"); 
			if (initMethodPage != null)
				initMethodPage.Invoke (page, null);

			return page;
		}

        protected void PushViewModel<T> (object data) where T : BasePageModel
		{
            BasePageModel viewModel = TinyIoC.TinyIoCContainer.Current.Resolve<T>();;

			var page = ResolveViewModel<T> (data, viewModel);

			viewModel.PreviousViewModel = this;

            IRootNavigation rootNav = TinyIoC.TinyIoCContainer.Current.Resolve<IRootNavigation> ();
            rootNav.PushPage (page, viewModel);
		}

		protected void PopViewModel()
		{
            IRootNavigation rootNav = TinyIoC.TinyIoCContainer.Current.Resolve<IRootNavigation> ();
            rootNav.PopPage ();
		}

		protected void PopViewModel(object data)
		{
			if (PreviousViewModel != null && data != null) {
                var initMethod = TinyIoC.TypeExtensions.GetMethod (PreviousViewModel.GetType(), "ReverseInit"); 
				if (initMethod != null) {
					initMethod.Invoke (PreviousViewModel, new object[] { data });
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

