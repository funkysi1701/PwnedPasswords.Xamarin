using System.Collections.Generic;

namespace PwnedPass2.Models
{
    public class HIBPResult
    {
        public IList<HIBPModel> HIBP { get; set; }
        public string Exception { get; set; }
    }
}