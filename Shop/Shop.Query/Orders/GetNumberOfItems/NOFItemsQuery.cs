using Common.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.GetNumberOfItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Orders.GetNumberOfItems
{
	public record NOFItemsQuery(long UserId):IRequest<int>;
}
public class NOFItemsQueryHandler : IRequestHandler<NOFItemsQuery, int>
{
	private readonly ShopContext _shopContext;

	public NOFItemsQueryHandler(ShopContext shopContext)
	{
		_shopContext = shopContext;
	}	

	public async Task<int> Handle(NOFItemsQuery request, CancellationToken cancellationToken)
	{
		var order = await _shopContext.Orders.FirstOrDefaultAsync(d => d.UserId == request.UserId, cancellationToken);

		return order.ItemCount;
	}
}
