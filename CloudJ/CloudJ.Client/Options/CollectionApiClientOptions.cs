using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Options
{
    public class CollectionApiClientOptions
    {
        public string AddCollectionUrl { get; set; }
        public string RemoveCollectionUrl { get; set; }
        public string GetCollectionByFilterUrl { get; set; }
        public string UpdateCollectionUrl { get; set; }
    }
}
