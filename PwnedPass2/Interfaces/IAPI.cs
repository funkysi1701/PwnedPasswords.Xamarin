﻿using System.Net.Http;
using System.Threading.Tasks;

namespace PwnedPass2.Interfaces
{
    public interface IApi
    {
        /// <summary>
        /// GetHIBP.
        /// </summary>
        /// <param name="url">url.</param>
        /// <returns>Task string.</returns>
        Task<string> GetHIBP(string url);

        /// <summary>
        /// GetAsyncAPI.
        /// </summary>
        /// <param name="url">url.</param>
        /// <returns>Task HttpResponseMessage.</returns>
        HttpResponseMessage GetAsyncAPI(string url, string version);
    }
}
