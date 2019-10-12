using PwnedPass2.Interfaces;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PwnedPass2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private bool saveFirst = false;

        public MainPage()
        {
            InitializeComponent();
            SaveData();
        }

        private void SaveData()
        {
            try
            {
                this.saveFirst = Cache.SaveData(this.saveFirst);
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(ex.Message, ex);
            }
        }
    }
}