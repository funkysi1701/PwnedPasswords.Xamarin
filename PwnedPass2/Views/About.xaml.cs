
using PwnedPass2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PwnedPass2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About : ContentPage
    {
        private AboutViewModel viewModel;
        public About()
        {
            InitializeComponent();
            BindingContext = viewModel = new AboutViewModel();
        }
    }
}