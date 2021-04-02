using PwnedPasswords.Core;
using System.Collections.Generic;

namespace PwnedPass2.Models
{
    public class HibpResult
    {
        public IList<HIBP> HIBP { get; set; }
        public string Exception { get; set; }
    }
}
