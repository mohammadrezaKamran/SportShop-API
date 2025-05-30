using Common.Query;
using MediatR;
using Shop.Query.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.Order.GetNewOrder
{
    public class GetNewOrdersQuery:IRequest<long>
    {
    }
}
