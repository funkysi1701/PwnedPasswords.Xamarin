﻿using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PwnedPass2.ViewModels
{
    public class EmailViewModel : BaseViewModel
    {
        public ObservableCollection<HIBPModel> Emails { get; set; }
        public Command LoadEmailsCommand { get; set; }

        public string EmailInput { get; set; }
        public string DateSort { get; set; }
        public string NameSort { get; set; }
        public string CountSort { get; set; }
        public string Breach { get; set; }
        public string Account { get; set; }

        public EmailViewModel(string EmailInp, bool order, string orderby)
        {
            Title = "';** pwned pass";
            EmailInput = EmailInp;
            Emails = new ObservableCollection<HIBPModel>();
            LoadEmailsCommand = new Command(async () => await ExecuteLoadItemsCommand(EmailInput, order, orderby));
            SetSort(order, orderby);
            SetSort(order, orderby);
            SetSort(order, orderby);
            Breach = SetBreach();
            Account = SetAccount();
        }

        private string SetAccount()
        {
            return Page.GetAccounts();
        }

        private string SetBreach()
        {
            return Page.GetBreach();
        }

        private void SetSort(bool order, string orderby)
        {
            if (orderby == "AddedDate")
            {
                if (order)
                {
                    DateSort = "^ Date";
                    NameSort = "Name";
                    CountSort = "Pwned Acc";
                }
                else
                {
                    DateSort = "v Date";
                    NameSort = "Name";
                    CountSort = "Pwned Acc";
                }
            }
            if (orderby == "Name")
            {
                if (order)
                {
                    DateSort = "Date";
                    NameSort = "^ Name";
                    CountSort = "Pwned Acc";
                }
                else
                {
                    DateSort = "Date";
                    NameSort = "v Name";
                    CountSort = "Pwned Acc";
                }
            }
            if (orderby == "PwnCount")
            {
                if (order)
                {
                    DateSort = "Date";
                    NameSort = "Name";
                    CountSort = "^ Pwned Acc";
                }
                else
                {
                    DateSort = "Date";
                    NameSort = "Name";
                    CountSort = "v Pwned Acc";
                }
            }
        }

        private async Task ExecuteLoadItemsCommand(string email, bool sortdir, string orderby)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Emails.Clear();
                var items = await DataStore.GetEmailsAsync(email, orderby, sortdir, true);
                SaveLastEmail(email);
                foreach (var item in items)
                {
                    Emails.Add(item);
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SaveLastEmail(string email)
        {
            try
            {
                var last = new LastEmail
                {
                    Email = email,
                    Id = 1
                };
                App.Database.SaveLastEmail(last);
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
            }
        }

        public LastEmail LoadLastEmail()
        {
            try
            {
                return App.Database.GetLastEmail();
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
                return new LastEmail();
            }
        }
    }
}