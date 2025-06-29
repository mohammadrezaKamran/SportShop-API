﻿using Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg.Repository
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<ProductVariant?> GetVariantById(long variantId);
		Task<bool> IsSequenceDuplicateAsync(long productId, int sequence);
	}
}
