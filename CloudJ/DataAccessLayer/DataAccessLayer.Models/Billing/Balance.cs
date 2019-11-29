using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models.Billing
{
    public class Balance : BaseEntity
    {
        public string UserId { get; set; }
        public double BalanceValue { get; set; }

    }
}
