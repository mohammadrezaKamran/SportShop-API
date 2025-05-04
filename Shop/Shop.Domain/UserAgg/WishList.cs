using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg
{
    public class WishList:BaseEntity
    {
        public WishList(long productId)
        {
            ProductId = productId;
        }

        public long ProductId {  get;private set; }

    }
}
