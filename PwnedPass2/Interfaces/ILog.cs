using System;

namespace PwnedPass2.Interfaces
{
    public interface ILog
    {
        /// <summary>
        /// SendTracking.
        /// </summary>
        /// <param name="message">message.</param>
        void SendTracking(string message);

        /// <summary>
        /// SendTracking.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="e">e.</param>
        void SendTracking(string message, Exception e);
    }
}