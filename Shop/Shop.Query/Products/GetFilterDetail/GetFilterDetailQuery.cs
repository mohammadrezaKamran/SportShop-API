using Common.Query;
using MediatR;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GetProductByIdQueryHandler;

namespace Shop.Query.Products.GetFilterDetail
{
	public class GetFilterDetailQuery : IQuery<FilterDetailsDto?>
	{

	}
}
