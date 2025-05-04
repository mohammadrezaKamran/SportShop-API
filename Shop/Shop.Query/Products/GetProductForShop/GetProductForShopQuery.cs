using Common.Query;
using Shop.Query.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products.GetProductForShop
{
    public class GetProductForShopQuery : QueryFilter<ProductShopResult, ProductShopFilterParam>
    {
        public GetProductForShopQuery(ProductShopFilterParam filterParams) : base(filterParams)
        {
        }
    }
}
