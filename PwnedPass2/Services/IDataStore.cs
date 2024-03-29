﻿using PwnedPass2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PwnedPass2.Services
{
    public interface IDataStore<T>
    {
        Task<T> GetItemAsync(string id);

        Task<IEnumerable<T>> GetItemsAsync(string orderby, bool orderdir, string url, bool forceRefresh = false);

        Task<IEnumerable<T>> GetEmailsAsync(string email, string orderby, bool orderdir, bool forceRefresh = false);

        Task<Passwords> GetPasswordAsync(string hash);
    }
}
