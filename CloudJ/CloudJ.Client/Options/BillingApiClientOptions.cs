﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Options
{
    public class BillingApiClientOptions
    {
        public string MakeOrderUrl { get; set; }
        public string GetOrdersByFilterUrl { get; set; }
    }
}
