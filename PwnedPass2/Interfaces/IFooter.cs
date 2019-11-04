using PwnedPass2.Views;
using Xamarin.Forms;

namespace PwnedPass2.Interfaces
{
    public interface IFooter
    {
        void AddFooter(ItemsPage mainPage, StackLayout stack);

        void AddFooter(EmailCheck mainPage, StackLayout stack);

        void AddFooter(PasswordCheck mainPage, StackLayout stack);
    }
}