using System;
using System.Collections.Generic;
using System.Text;

namespace PwnedPass2.Models
{
    public class HIBPResult
    {
        public IList<HIBPModel> HIBP { get; set; }
        public string Exception { get; set; }
    }
}