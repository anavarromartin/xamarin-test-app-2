using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinDemo2
{
    public interface INavigationService
    {
        Task NavigateTo<TPage>(params object[] args) where TPage : Page;
    }

    public class NavigationService : INavigationService
    {
        public async Task NavigateTo<TPage>(params object[] args) where TPage : Page
        {
            var page = (TPage)Activator.CreateInstance(typeof(TPage), args);
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}

