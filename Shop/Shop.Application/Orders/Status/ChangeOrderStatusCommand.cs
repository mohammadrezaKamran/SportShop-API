﻿using Common.Application;
using Shop.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.Status
{
    public class ChangeOrderStatusCommand : IBaseCommand
    {
        public long OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }

}
