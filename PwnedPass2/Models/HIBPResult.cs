using PwnedPasswords.Core;
using System.Collections.Generic;

namespace PwnedPass2.Models
{
    public class HIBPResult
    {
        public IList<HIBP> HIBP { get; set; }
        public string Exception { get; set; }
    }
}
