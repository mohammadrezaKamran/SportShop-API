using Common.Query;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetProductForShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products.GetProductForCategory
{
    public class GetProductForCategoryQuery : QueryFilter<ProductCategoryResult, ProductCategoryFilterParam>
    {
        public GetProductForCategoryQuery(ProductCategoryFilterParam filterParams) : base(filterParams)
        {
            
        }
    }
}
