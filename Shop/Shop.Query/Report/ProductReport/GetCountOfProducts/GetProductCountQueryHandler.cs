using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.ProductReport.GetCountOfProducts
{
    internal class GetProductCountQueryHandler : IRequestHandler<GetProductCountQuery, long>
    {
        private readonly ShopContext _context;

        public GetProductCountQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(GetProductCountQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.CountAsync(cancellationToken);
        }
    }
}
