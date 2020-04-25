﻿using System.Net.Http;

namespace PwnedPass2.Interfaces
{
    public interface IAPI
    {
        /// <summary>
        /// GetHIBP.
        /// </summary>
        /// <param name="url">url.</param>
        /// <returns>Task string.</returns>
        string GetHIBP(string url);

        /// <summary>
        /// GetAsyncAPI.
        /// </summary>
        /// <param name="url">url.</param>
        /// <returns>Task HttpResponseMessage.</returns>
        HttpResponseMessage GetAsyncAPI(string url);
    }
}