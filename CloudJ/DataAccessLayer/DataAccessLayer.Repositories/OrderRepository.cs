using DataAccessLayer.Abstraction;
using DataAccessLayer.Billing.Models;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(CloudjContext context) : base(context)
        {
        }
    }
}
