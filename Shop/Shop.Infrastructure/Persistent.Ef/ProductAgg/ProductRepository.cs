using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(Product product)
    {     
        if (product != null)
        {
            Context.Remove(product);
        }
    }

    async Task<ProductInventory?> IProductRepository.GetInventoryById(long id)
    {
     return await Context.Products.SelectMany(p=>p.Inventories).FirstOrDefaultAsync(i=>i.Id==id);
    }
}