using DataAccessLayer.Billing.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Abstraction
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
